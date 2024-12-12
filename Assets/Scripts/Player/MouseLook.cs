using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    float RotationX;
    float RotationY;
    public float MinXRotation = -90f;
    public float MaxXRotation = 90f; 
    public float MinYRotation = -60f;
    public float MaxYRotation = 60f;
    public float sensivity = 15f;
    void Start()
    {
        Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        RotationY += Input.GetAxis("Mouse X") * sensivity;
        RotationX += Input.GetAxis("Mouse Y") * -1 * sensivity;
        RotationX = Mathf.Clamp(RotationX, MinXRotation, MaxXRotation);
        RotationY = Mathf.Clamp(RotationY, MinYRotation, MaxYRotation);
        transform.localEulerAngles = new Vector3(RotationX, RotationY, 0);

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            //Debug.Log(mousePos.x);
            //Debug.Log(mousePos.y);
        }
    }

}
