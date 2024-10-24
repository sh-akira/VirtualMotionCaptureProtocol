/*
 * SampleBonesSendBundle (VRM0.99)
 * gpsnmeajp
 * https://sh-akira.github.io/VirtualMotionCaptureProtocol/
 *
 * These codes are licensed under CC0.
 * http://creativecommons.org/publicdomain/zero/1.0/deed.ja
 */
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;
using uOSC;
using UniGLTF;

[RequireComponent(typeof(uOSC.uOscClient))]
public class SampleBonesSendBundle : MonoBehaviour
{
    uOSC.uOscClient uClient = null;

    public GameObject Model = null;
    private GameObject OldModel = null;
    public string vrmfilepath;
    public bool RuntimeLoadGUI = true;

    Animator animator = null;
    VRMBlendShapeProxy blendShapeProxy = null;

    SynchronizationContext synchronizationContext;

    public enum VirtualDevice
    {
        HMD = 0,
        Controller = 1,
        Tracker = 2,
    }

    void Start()
    {
        uClient = GetComponent<uOSC.uOscClient>();
        synchronizationContext = SynchronizationContext.Current;
    }

    void LateUpdate()
    {
        //Only model updated
        if (Model != null && OldModel != Model)
        {
            animator = Model.GetComponent<Animator>();
            blendShapeProxy = Model.GetComponent<VRMBlendShapeProxy>();
            OldModel = Model;
        }

        if (Model != null && animator != null && uClient != null)
        {
            //Root
            var RootTransform = Model.transform;
            if (RootTransform != null)
            {
                uClient.Send("/VMC/Ext/Root/Pos",
                    "root",
                    RootTransform.position.x, RootTransform.position.y, RootTransform.position.z,
                    RootTransform.rotation.x, RootTransform.rotation.y, RootTransform.rotation.z, RootTransform.rotation.w);
            }

            //Bones
            var boneBundle = new Bundle(Timestamp.Now);
            foreach (HumanBodyBones bone in Enum.GetValues(typeof(HumanBodyBones)))
            {
                if (bone != HumanBodyBones.LastBone)
                {
                    var Transform = animator.GetBoneTransform(bone);
                    if (Transform != null)
                    {
                        boneBundle.Add(new Message("/VMC/Ext/Bone/Pos",
                            bone.ToString(),
                            Transform.localPosition.x, Transform.localPosition.y, Transform.localPosition.z,
                            Transform.localRotation.x, Transform.localRotation.y, Transform.localRotation.z, Transform.localRotation.w));
                    }
                }
            }
            uClient.Send(boneBundle);

            //Virtual Tracker send from bone position
            var trackerBundle = new Bundle(Timestamp.Now);
            SendBoneTransformForTracker(ref trackerBundle, HumanBodyBones.Head, "Head");
            SendBoneTransformForTracker(ref trackerBundle, HumanBodyBones.Spine, "Spine");
            SendBoneTransformForTracker(ref trackerBundle, HumanBodyBones.LeftHand, "LeftHand");
            SendBoneTransformForTracker(ref trackerBundle, HumanBodyBones.RightHand, "RightHand");
            SendBoneTransformForTracker(ref trackerBundle, HumanBodyBones.LeftFoot, "LeftFoot");
            SendBoneTransformForTracker(ref trackerBundle, HumanBodyBones.RightFoot, "RightFoot");
            uClient.Send(trackerBundle);

            //BlendShape
            if (blendShapeProxy != null)
            {
                var blendShapeBundle = new Bundle(Timestamp.Now);

                foreach (var b in blendShapeProxy.GetValues())
                {
                    blendShapeBundle.Add(new Message("/VMC/Ext/Blend/Val",
                        b.Key.ToString(),
                        (float)b.Value
                        ));
                }
                blendShapeBundle.Add(new Message("/VMC/Ext/Blend/Apply"));
                uClient.Send(blendShapeBundle);
            }

            //Available
            uClient.Send("/VMC/Ext/OK", 1);
        }
        else
        {
            uClient.Send("/VMC/Ext/OK", 0);
        }
        uClient.Send("/VMC/Ext/T", Time.time);

        //Load request
        uClient.Send("/VMC/Ext/VRM", vrmfilepath, "");

    }

    private void OnGUI()
    {
        if (RuntimeLoadGUI) {
            var ButtonStyle = new GUIStyle(GUI.skin.button);
            ButtonStyle.fontSize = 24;
            var TextFieldStyle = new GUIStyle(GUI.skin.textField);
            TextFieldStyle.fontSize = 24;
            var LabelStyle = new GUIStyle(GUI.skin.label);
            LabelStyle.fontSize = 24;

            var path = GUILayout.TextField("C:\\default.vrm", TextFieldStyle);
            if (GUILayout.Button("Load VRM", ButtonStyle)) {
                LoadVRM(path);
            }
            GUILayout.Label("Port:"+uClient.port, LabelStyle);
        }
    }

    void SendBoneTransformForTracker(ref Bundle bundle, HumanBodyBones bone, string DeviceSerial)
    {
        var DeviceTransform = animator.GetBoneTransform(bone);
        if (DeviceTransform != null) {
            bundle.Add(new Message("/VMC/Ext/Tra/Pos",
        (string)DeviceSerial,
        (float)DeviceTransform.position.x,
        (float)DeviceTransform.position.y,
        (float)DeviceTransform.position.z,
        (float)DeviceTransform.rotation.x,
        (float)DeviceTransform.rotation.y,
        (float)DeviceTransform.rotation.z,
        (float)DeviceTransform.rotation.w));
        }
    }

    //Load VRM file on runtime
    public void LoadVRM(string path) {
        if (Model != null)
        {
            Destroy(Model);
            Model = null;
        }

        if (File.Exists(path))
        {
            vrmfilepath = path;
            byte[] VRMdataRaw = File.ReadAllBytes(path);
            LoadVRMFromData(VRMdataRaw);
        }
        else {
            Debug.LogError("File not found: " + path);
        }
    }

    //Load VRM data on runtime
    //You can receive VRM over the network or file or other.
    public void LoadVRMFromData(byte[] VRMdataRaw)
    {
        GlbLowLevelParser glbLowLevelParser = new GlbLowLevelParser(null, VRMdataRaw);
        GltfData gltfData = glbLowLevelParser.Parse();
        VRMData vrm = new VRMData(gltfData);
        VRMImporterContext vrmImporter = new VRMImporterContext(vrm);

        synchronizationContext.Post(async (_) => {
            RuntimeGltfInstance gltfInstance = await vrmImporter.LoadAsync(new VRMShaders.ImmediateCaller());
            gltfData.Dispose();
            vrmImporter.Dispose();
            
            Model = gltfInstance.Root;
            Model.transform.parent = this.transform;

            gltfInstance.EnableUpdateWhenOffscreen();
            gltfInstance.ShowMeshes();
        }, null);
    }
}
