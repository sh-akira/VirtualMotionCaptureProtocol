/*
 * CameraPositionSend
 * gpsnmeajp
 * https://sh-akira.github.io/VirtualMotionCaptureProtocol/
 *
 * These codes are licensed under CC0.
 * http://creativecommons.org/publicdomain/zero/1.0/deed.ja
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(uOSC.uOscClient))]
public class CameraPositionSend : MonoBehaviour
{
    public float fov = 60;
    uOSC.uOscClient uClient = null;
    void Start()
    {
        uClient = GetComponent<uOSC.uOscClient>();
    }

    void Update()
    {
        uClient.Send("/VMC/Ext/Cam",
                    "camera",
                    transform.position.x, transform.position.y, transform.position.z,
                    transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w,fov);
    }
}
