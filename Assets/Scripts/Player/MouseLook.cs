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
    public float smoothTime = 0.1f;

    private float rotationXVelocity;
    private float rotationYVelocity;

    public float cursorLimitX = 0.8f;
    public float cursorLimitY = 0.8f;

    void Start()
    {
        RotationX = transform.localEulerAngles.x;
        RotationY = transform.localEulerAngles.y;

       // Cursor.lockState = CursorLockMode.Locked;
       // Cursor.visible = false;
    }

    void Update()
    {
        if (DialogueSystem.IsDialogueActive || PauseMenu.IsGamePaused)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * sensivity;
        float mouseY = Input.GetAxis("Mouse Y") * -1 * sensivity;
        RotationY += mouseX;
        RotationX += mouseY;

        RotationY = Mathf.SmoothDampAngle(RotationY, RotationY + mouseX, ref rotationYVelocity, smoothTime);
        RotationX = Mathf.SmoothDampAngle(RotationX, RotationX + mouseY, ref rotationXVelocity, smoothTime);

        RotationX = Mathf.Clamp(RotationX, MinXRotation, MaxXRotation);
        RotationY = Mathf.Clamp(RotationY, MinYRotation, MaxYRotation);

        transform.localEulerAngles = new Vector3(RotationX, RotationY, 0);

        LimitCursorPosition();
    }

    void LimitCursorPosition()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float xMin = screenWidth * (1 - cursorLimitX) / 2;
        float xMax = screenWidth - xMin;
        float yMin = screenHeight * (1 - cursorLimitY) / 2;
        float yMax = screenHeight - yMin;

        Vector3 cursorPos = Input.mousePosition;

        cursorPos.x = Mathf.Clamp(cursorPos.x, xMin, xMax);
        cursorPos.y = Mathf.Clamp(cursorPos.y, yMin, yMax);
    }
}
