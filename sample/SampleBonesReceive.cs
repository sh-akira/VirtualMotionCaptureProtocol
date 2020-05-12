/*
 * SampleBonesReceive
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

[RequireComponent(typeof(uOSC.uOscServer))]
public class SampleBonesReceive : MonoBehaviour
{
    public GameObject Model;
    private GameObject OldModel = null;

    Animator animator = null;
    VRMBlendShapeProxy blendShapeProxy = null;

    uOSC.uOscServer server;

    void Start()
    {
        server = GetComponent<uOSC.uOscServer>();
        server.onDataReceived.AddListener(OnDataReceived);
    }

    void Update()
    {
        if (blendShapeProxy == null)
        {
            blendShapeProxy = Model.GetComponent<VRMBlendShapeProxy>();
        }
    }

    void OnDataReceived(uOSC.Message message)
    {
        if (message.address == "/VMC/Ext/Root/Pos")
        {
            Vector3 pos = new Vector3((float)message.values[1], (float)message.values[2], (float)message.values[3]);
            Quaternion rot = new Quaternion((float)message.values[4], (float)message.values[5], (float)message.values[6], (float)message.values[7]);

            Model.transform.localPosition = pos;
            Model.transform.localRotation = rot;
        }

        else if (message.address == "/VMC/Ext/Bone/Pos")
        {
            //モデルが更新されたときのみ読み込み
            if (Model != null && OldModel != Model)
            {
                animator = Model.GetComponent<Animator>();
                blendShapeProxy = Model.GetComponent<VRMBlendShapeProxy>();
                OldModel = Model;
            }

            HumanBodyBones bone;
            if (Enum.TryParse<HumanBodyBones>((string)message.values[0], out bone))
            {
                if ((animator != null) && (bone != HumanBodyBones.LastBone))
                {
                    Vector3 pos = new Vector3((float)message.values[1], (float)message.values[2], (float)message.values[3]);
                    Quaternion rot = new Quaternion((float)message.values[4], (float)message.values[5], (float)message.values[6], (float)message.values[7]);

                    var t = animator.GetBoneTransform(bone);
                    if (t != null)
                    {
                        t.localPosition = pos;
                        t.localRotation = rot;
                    }
                }
            }
        }
        else if (message.address == "/VMC/Ext/Blend/Val")
        {
            string BlendName = (string)message.values[0];
            float BlendValue = (float)message.values[1];

            blendShapeProxy.AccumulateValue(BlendName, BlendValue);
        }
        else if (message.address == "/VMC/Ext/Blend/Apply")
        {
            blendShapeProxy.Apply();
        }
    }
}