/*
 * SampleBonesSendBundle (VRM0and1)
 * gpsnmeajp
 * https://sh-akira.github.io/VirtualMotionCaptureProtocol/
 *
 * Use UniVRM-0.120.0_be13.unitypackage, VRM-0.120.0_be13.unitypackage
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
using UniVRM10;
using VRM;
using uOSC;
using UniGLTF;

[RequireComponent(typeof(uOSC.uOscClient))]
public class SampleBonesSendBundle : MonoBehaviour
{
    uOSC.uOscClient uClient = null;

    public GameObject Model = null;
    private GameObject OldModel = null;
    public bool vrm0styleExpression = true;
    public string vrmfilepath;
    public bool RuntimeLoadGUI = true;

    Animator animator = null;
    Vrm10Instance vrm10Root = null;
    VRMBlendShapeProxy blendShapeProxy = null;

    SynchronizationContext synchronizationContext;

    string path = "C:\\default.vrm";
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

        Application.targetFrameRate = 60; //60fps
    }

    void Update()
    {
        //Only model updated
        if (Model != null && OldModel != Model)
        {
            animator = Model.GetComponent<Animator>();
            vrm10Root = Model.GetComponent<Vrm10Instance>();
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
                    Transform Transform;
                    if (vrm10Root)
                    {
                        Transform = vrm10Root.Humanoid.GetBoneTransform(bone);//生のBone
                    }
                    else
                    {
                        Transform = animator.GetBoneTransform(bone);//正規化されたBone
                    }

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

            //Expression
            if (vrm10Root != null)
            {
                var blendShapeBundle = new Bundle(Timestamp.Now);

                foreach (var b in vrm10Root.Runtime.Expression.GetWeights())
                {
                    string expressionName = b.Key.ToString();

                    if (vrm0styleExpression)
                    {
                        //VRM1 Preset -> VRM0 Preset 
                        switch (expressionName.ToLower())
                        {
                            case "happy": expressionName = "Joy"; break;
                            case "angry": expressionName = "Angry"; break;
                            case "sad": expressionName = "Sorrow"; break;
                            case "relaxed": expressionName = "Fun"; break;
                            case "aa": expressionName = "A"; break;
                            case "ih": expressionName = "I"; break;
                            case "ou": expressionName = "U"; break;
                            case "ee": expressionName = "E"; break;
                            case "oh": expressionName = "O"; break;
                            case "blinkleft": expressionName = "Blink_L"; break;
                            case "blinkright": expressionName = "Blink_R"; break;
                            default: break;
                        }
                    }

                    uClient.Send("/VMC/Ext/Blend/Val",
                        expressionName,
                        (float)b.Value
                        );
                }
                blendShapeBundle.Add(new Message("/VMC/Ext/Blend/Apply"));
                uClient.Send(blendShapeBundle);
            }

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
        if (RuntimeLoadGUI)
        {
            var ButtonStyle = new GUIStyle(GUI.skin.button);
            ButtonStyle.fontSize = 24;
            var TextFieldStyle = new GUIStyle(GUI.skin.textField);
            TextFieldStyle.fontSize = 24;
            var LabelStyle = new GUIStyle(GUI.skin.label);
            LabelStyle.fontSize = 24;

            path = GUILayout.TextField(path, TextFieldStyle);
            if (GUILayout.Button("Load VRM", ButtonStyle))
            {
                LoadVRM(path);
            }
            GUILayout.Label("Port:" + uClient.port, LabelStyle);

            if (GUILayout.Button("Happy(Joy) ON", ButtonStyle))
            {
                blendShapeProxy?.SetValue(BlendShapePreset.Joy, 1);
                vrm10Root?.Runtime.Expression.SetWeight(ExpressionKey.Happy, 1);
            }
            if (GUILayout.Button("Happy(Joy) OFF", ButtonStyle))
            {
                blendShapeProxy?.SetValue(BlendShapePreset.Joy, 0);
                vrm10Root?.Runtime.Expression.SetWeight(ExpressionKey.Happy, 0);
            }
        }
    }

    void SendBoneTransformForTracker(ref Bundle bundle, HumanBodyBones bone, string DeviceSerial)
    {
        Transform DeviceTransform;
        if (vrm10Root)
        {
            DeviceTransform = vrm10Root.Humanoid.GetBoneTransform(bone);//生のBone
        }
        else
        {
            DeviceTransform = animator.GetBoneTransform(bone);//正規化されたBone
        }

        if (DeviceTransform != null)
        {
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
    public void LoadVRM(string path)
    {
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
        else
        {
            Debug.LogError("File not found: " + path);
        }
    }

    //Load VRM data on runtime
    //You can receive VRM over the network or file or other.
    public void LoadVRMFromData(byte[] VRMdataRaw)
    {
        GlbLowLevelParser glbLowLevelParser = new GlbLowLevelParser(null, VRMdataRaw);
        GltfData gltfData = glbLowLevelParser.Parse();
        try
        {
            VRMData vrm = new VRMData(gltfData);
            VRMImporterContext vrmImporter = new VRMImporterContext(vrm);

            synchronizationContext.Post(async (_) =>
            {
                RuntimeGltfInstance gltfInstance = await vrmImporter.LoadAsync(new VRMShaders.ImmediateCaller());
                gltfData.Dispose();
                vrmImporter.Dispose();

                Model = gltfInstance.Root;
                Model.transform.parent = this.transform;

                gltfInstance.EnableUpdateWhenOffscreen();
                gltfInstance.ShowMeshes();
            }, null);

            return; //VRM0 Loaded
        }
        catch (NotVrm0Exception)
        {
            //continue loading
        }

        synchronizationContext.Post(async (_) =>
        {
            var instance = await Vrm10.LoadBytesAsync(VRMdataRaw, canLoadVrm0X: false, showMeshes: true, controlRigGenerationOption: ControlRigGenerationOption.None);
            await Task.Delay(100); //VRM1不具合対策
            Model = instance.gameObject;
            Model.transform.parent = this.transform;
            animator = Model.GetComponent<Animator>();

            var meshes = Model.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var m in meshes)
            {
                m.updateWhenOffscreen = true;
            }

            //開放
            gltfData?.Dispose();
        }, null);
    }
}
