/*
 * SampleBonesSendBundle (VRM1)
 * gpsnmeajp
 * https://sh-akira.github.io/VirtualMotionCaptureProtocol/
 *
 * These codes are licensed under CC0.
 * http://creativecommons.org/publicdomain/zero/1.0/deed.ja
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniVRM10;
using uOSC;

[RequireComponent(typeof(uOSC.uOscClient))]
public class SampleBonesSendBundle : MonoBehaviour
{
    uOSC.uOscClient uClient = null;

    public GameObject Model = null;
    private GameObject OldModel = null;

    Animator animator = null;
    Vrm10Instance vrmRoot = null;

    public bool vrm0styleExpression = true;

    public string filepath;

    public enum VirtualDevice
    {
        HMD = 0,
        Controller = 1,
        Tracker = 2,
    }

    void Start()
    {
        uClient = GetComponent<uOSC.uOscClient>();
    }

    void Update()
    {
        //Only model updated
        if (Model != null && OldModel != Model)
        {
            animator = Model.GetComponent<Animator>();
            vrmRoot = Model.GetComponent<Vrm10Instance>();
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

            //Expression
            if (vrmRoot != null)
            {
                var blendShapeBundle = new Bundle(Timestamp.Now);

                foreach (var b in vrmRoot.Runtime.Expression.GetWeights())
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

            //Available
            uClient.Send("/VMC/Ext/OK", 1);
        }
        else
        {
            uClient.Send("/VMC/Ext/OK", 0);
        }
        uClient.Send("/VMC/Ext/T", Time.time);

        //Load request
        uClient.Send("/VMC/Ext/VRM", filepath, vrmRoot.Vrm.Meta.Name);

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
}
