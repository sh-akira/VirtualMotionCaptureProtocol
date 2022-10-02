/*
 * SampleBonesReceive (VRM0and1)
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
using VRM;

[RequireComponent(typeof(uOSC.uOscServer))]
public class SampleBonesReceive : MonoBehaviour
{
    public GameObject Model;
    private GameObject OldModel = null;

    Animator animator = null;
    Vrm10Instance vrm10Root = null;
    VRMBlendShapeProxy blendShapeProxy = null;

    uOSC.uOscServer server;

    Dictionary<ExpressionKey, float> expressions = new Dictionary<ExpressionKey, float>();
    Dictionary<BlendShapeKey, float> blends = new Dictionary<BlendShapeKey, float>();

    void Start()
    {
        server = GetComponent<uOSC.uOscServer>();
        server.onDataReceived.AddListener(OnDataReceived);

        Application.targetFrameRate = 60; //60fps
    }

    void Update()
    {
        if (vrm10Root == null)
        {
            vrm10Root = Model.GetComponent<Vrm10Instance>();
        }
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
                vrm10Root = Model.GetComponent<Vrm10Instance>();
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

            if (blendShapeProxy)
            {
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

            if (vrm10Root)
            {
                string expressionName = BlendName;

                //VRM0 Preset -> VRM1 Preset 
                switch (expressionName)
                {
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
                foreach (var e in vrm10Root.Runtime.Expression.ExpressionKeys)
                {
                    if (e.Preset == ExpressionPreset.custom)
                    {
                        if (e.Name.ToLower() == expressionName)
                        {
                            expressions[e] = BlendValue;
                            found = true;
                            break;
                        }
                    }
                    else
                    {
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
        }

        else if (message.address == "/VMC/Ext/Blend/Apply")
        {
            if (blendShapeProxy)
            {
                blendShapeProxy.SetValues(blends);
                blends.Clear();
            }
            if (vrm10Root)
            {
                vrm10Root.Runtime.Expression.SetWeights(expressions);
                expressions.Clear();
            }
        }
    }
}