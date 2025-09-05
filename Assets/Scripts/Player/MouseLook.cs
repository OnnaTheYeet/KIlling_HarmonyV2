using UnityEngine;

public class MouseLook : MonoBehaviour
{
    float RotationX;
    float RotationY;
    public float MinXRotation = -110f;
    public float MaxXRotation = 90f;
    public float MinYRotation = -60f;
    public float MaxYRotation = 60f;
    public float sensivity = 15f;

    void Start()
    {
        RotationX = transform.localEulerAngles.x;
        RotationY = transform.localEulerAngles.y;

        transform.localEulerAngles = new Vector3(RotationX, RotationY, 0);
    }

    void Update()
    {
        if (DialogueSystem.IsDialogueActive || PauseMenu.IsGamePaused)
        {
            return;
        }

        RotationY += Input.GetAxis("Mouse X") * sensivity;
        RotationX += Input.GetAxis("Mouse Y") * -1 * sensivity; 

        RotationX = Mathf.Clamp(RotationX, MinXRotation, MaxXRotation);
        RotationY = Mathf.Clamp(RotationY, MinYRotation, MaxYRotation);

        transform.localEulerAngles = new Vector3(RotationX, RotationY, 0);
    }
}
