/*
 * SampleBonesReceive (VRM1)
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
using UniVRM10;

[RequireComponent(typeof(uOSC.uOscServer))]
public class SampleBonesReceive : MonoBehaviour
{
    public GameObject Model;
    private GameObject OldModel = null;

    Animator animator = null;
    Vrm10Instance vrmRoot = null;

    uOSC.uOscServer server;

    Dictionary<ExpressionKey, float> expressions = new Dictionary<ExpressionKey, float>();

    void Start()
    {
        server = GetComponent<uOSC.uOscServer>();
        server.onDataReceived.AddListener(OnDataReceived);
    }

    void LateUpdate()
    {
        if (vrmRoot == null)
        {
            vrmRoot = Model.GetComponent<Vrm10Instance>();
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
                vrmRoot = Model.GetComponent<Vrm10Instance>();
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

            string expressionName = BlendName.ToLower();

            //VRM0 Preset -> VRM1 Preset 
            switch (expressionName) {
                case "joy": expressionName = "happy"; break;
                case "angry": expressionName = "angry"; break;
                case "sorrow": expressionName = "sad"; break;
                case "fun": expressionName = "relaxed"; break;
                case "a": expressionName = "aa"; break;
                case "i": expressionName = "ih"; break;
                case "u": expressionName = "ou"; break;
                case "e": expressionName = "ee"; break;
                case "o": expressionName = "oh"; break;
                case "blink_l": expressionName = "blinkleft"; break;
                case "blink_r": expressionName = "blinkright"; break;
                default: break;
            }

            //Search Expression
            bool found = false;
            foreach (var e in vrmRoot.Runtime.Expression.ExpressionKeys) {
                if (e.Preset == ExpressionPreset.custom)
                {
                    if (e.Name.ToLower() == expressionName)
                    {
                        expressions[e] = BlendValue;
                        found = true;
                        break;
                    }
                }
                else {
                    if (e.Preset.ToString().ToLower() == expressionName)
                    {
                        expressions[e] = BlendValue;
                        found = true;
                        break;
                    }
                }
            }

            if (!found)
            {
                Debug.Log("Not found!" + expressionName);
            }
        }

        else if (message.address == "/VMC/Ext/Blend/Apply")
        {
            vrmRoot.Runtime.Expression.SetWeights(expressions);
            expressions.Clear();
        }
    }
}