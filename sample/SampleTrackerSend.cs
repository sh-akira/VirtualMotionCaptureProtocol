/*
 * SampleTrackerSend
 * gpsnmeajp
 * https://sh-akira.github.io/VirtualMotionCaptureProtocol/
 *
 * These codes are licensed under CC0.
 * http://creativecommons.org/publicdomain/zero/1.0/deed.ja
 */
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Profiling;

[RequireComponent(typeof(uOSC.uOscClient))]
public class SampleTrackerSend : MonoBehaviour
{
    public enum VirtualDevice
    {
        HMD = 0,
        Controller = 1,
        Tracker = 2,
    }

    [Header("Virtual Device")]
    public VirtualDevice DeviceMode = VirtualDevice.Tracker;
    public Transform DeviceTransform = null;
    public String DeviceSerial = "VIRTUAL_DEVICE";

    [Header("BlendShapeProxy")]
    public string BlendShapeName = "";
    public float BlendShapeValue = 0f;

    uOSC.uOscClient client = null;

    void Start()
    {
        client = GetComponent<uOSC.uOscClient>();
    }
    void LateUpdate()
    {
        if (client == null)
        {
            return;
        }

        {
            string name = null;
            switch (DeviceMode)
            {
                case VirtualDevice.HMD:
                    name = "/VMC/Ext/Hmd/Pos";
                    break;
                case VirtualDevice.Controller:
                    name = "/VMC/Ext/Con/Pos";
                    break;
                case VirtualDevice.Tracker:
                    name = "/VMC/Ext/Tra/Pos";
                    break;
                default:
                    name = null;
                    break;
            }
            if (name != null && DeviceTransform != null && DeviceSerial != null)
            {
                client.Send(name,
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

        {
            client.Send("/VMC/Ext/Blend/Val", BlendShapeName, BlendShapeValue);
            client.Send("/VMC/Ext/Blend/Apply");
        }
    }
}
