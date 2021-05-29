/*
 * SampleBonesSendBundle
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
using VRM;
using uOSC;

[RequireComponent(typeof(uOSC.uOscClient))]
public class SampleBonesSendBundle : MonoBehaviour
{
    uOSC.uOscClient uClient = null;

    public GameObject Model = null;
    private GameObject OldModel = null;

    Animator animator = null;
    VRMBlendShapeProxy blendShapeProxy = null;

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
        //モデルが更新されたときのみ読み込み
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

            //ボーン位置を仮想トラッカーとして送信
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
