using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    private void Awake()
    {
        instance = this;
    }
    [Header("Camera")]
    public Transform Camera;
    public Transform CameraParent;
}
