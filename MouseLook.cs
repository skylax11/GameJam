using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public static MouseLook Instance;

    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] Transform CharacterBody;
    [SerializeField] public Transform CameraParent;

    [Header("")]

    [SerializeField][Range(0f, 20f)] float sensitivity;
    float x, y;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        MouseControl();
    }
    void MouseControl()
    {
        x += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime * 20f;
        y += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime * 20f;

        y = Mathf.Clamp(y, -80f, 80f);

        CameraParent.localRotation = Quaternion.Euler(-y, 0f, 0f);

        CharacterBody.localRotation = Quaternion.Euler(0f, x, 0f);
    }
    public void recoil(float a, float b)
    {
        x += a;
        y += b;
    }
}
