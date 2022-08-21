/*
 * SampleBonesReceive (VRM0.99)
 * gpsnmeajp
 * https://sh-akira.github.io/VirtualMotionCaptureProtocol/
 *
 * These codes are licensed under CC0.
 * http://creativecommons.org/publicdomain/zero/1.0/deed.ja
 */

// This is minimum sample of VMCProtocl.
// Recommend to use EVMC4U.

//これはVMCProtcolの最低限のサンプルです。
//特段の理由がない場合、EVMC4Uを利用することをおすすめします。

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

    Dictionary<BlendShapeKey, float> blends = new Dictionary<BlendShapeKey, float>();

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
            //Model updated
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
            string BlendName = ((string)message.values[0]).ToLower();
            float BlendValue = (float)message.values[1];

            //Search Expression
            bool found = false;
            foreach (var b in blendShapeProxy.GetValues())
            {
                if (b.Key.Preset == BlendShapePreset.Unknown)
                {
                    if (b.Key.Name.ToLower() == BlendName)
                    {
                        blends[b.Key] = BlendValue;
                        found = true;
                        break;
                    }
                }
                else
                {
                    if (b.Key.Preset.ToString().ToLower() == BlendName)
                    {
                        blends[b.Key] = BlendValue;
                        found = true;
                        break;
                    }
                }
            }

            if (!found)
            {
                Debug.Log("Not found!" + BlendName);
            }
        }
        else if (message.address == "/VMC/Ext/Blend/Apply")
        {
            blendShapeProxy.SetValues(blends);
            blends.Clear();
        }
    }
}