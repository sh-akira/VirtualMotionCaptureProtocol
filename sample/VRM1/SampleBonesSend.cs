/*
 * SampleBonesSend (VRM1)
 * gpsnmeajp
 * https://sh-akira.github.io/VirtualMotionCaptureProtocol/
 *
 * These codes are licensed under CC0.
 * http://creativecommons.org/publicdomain/zero/1.0/deed.ja
 */
 
// このスクリプトの利用は推奨しません！！
// このスクリプトは、学習用であり、必要な処理を欠いているため、通信バッファの詰まりを引き起こして、動作に異常な遅れを生じる場合があります。
// SampleBonesSendBundle.csの方を利用することを強く推奨します。

// NOT RECOMMENDED USE THIS SCRIPT!!
// This script is for learning purposes only and lacks necessary processing, which can cause communication buffers to clog up and cause unusual delays in operation.
// It is strongly recommended to use SampleBonesSendBundle.cs.
 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniVRM10;

[RequireComponent(typeof(uOSC.uOscClient))]
public class SampleBonesSend : MonoBehaviour
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

    void LateUpdate()
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
            foreach (HumanBodyBones bone in Enum.GetValues(typeof(HumanBodyBones)))
            {
                if (bone != HumanBodyBones.LastBone)
                {
                    var Transform = animator.GetBoneTransform(bone);
                    if (Transform != null)
                    {
                        uClient.Send("/VMC/Ext/Bone/Pos",
                            bone.ToString(),
                            Transform.localPosition.x, Transform.localPosition.y, Transform.localPosition.z,
                            Transform.localRotation.x, Transform.localRotation.y, Transform.localRotation.z, Transform.localRotation.w);
                    }
                }
            }

            //Virtual Tracker send from bone position
            SendBoneTransformForTracker(HumanBodyBones.Head, "Head");
            SendBoneTransformForTracker(HumanBodyBones.Spine, "Spine");
            SendBoneTransformForTracker(HumanBodyBones.LeftHand, "LeftHand");
            SendBoneTransformForTracker(HumanBodyBones.RightHand, "RightHand");
            SendBoneTransformForTracker(HumanBodyBones.LeftFoot, "LeftFoot");
            SendBoneTransformForTracker(HumanBodyBones.RightFoot, "RightFoot");

            //Expression
            if (vrmRoot != null)
            {
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
                uClient.Send("/VMC/Ext/Blend/Apply");
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

    void SendBoneTransformForTracker(HumanBodyBones bone, string DeviceSerial)
    {
        var DeviceTransform = animator.GetBoneTransform(bone);
        if (DeviceTransform != null) {
            uClient.Send("/VMC/Ext/Tra/Pos",
        (string)DeviceSerial,
        (float)DeviceTransform.position.x,
        (float)DeviceTransform.position.y,
        (float)DeviceTransform.position.z,
        (float)DeviceTransform.rotation.x,
        (float)DeviceTransform.rotation.y,
        (float)DeviceTransform.rotation.z,
        (float)DeviceTransform.rotation.w);
        }
    }
}
