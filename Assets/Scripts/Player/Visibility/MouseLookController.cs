using UnityEngine;

public class MouseLookController : MonoBehaviour
{
    public GameObject targetGameObject;
    private MouseLook mouseLook;

    private void Awake()
    {
        if (targetGameObject == null)
        {
            Debug.LogError("Kein Ziel-GameObject zugewiesen! Bitte das Ziel-GameObject im Inspector zuweisen.");
            return;
        }

        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCamera != null)
        {
            mouseLook = mainCamera.GetComponent<MouseLook>();
            if (mouseLook == null)
            {
                Debug.LogError("Das MouseLook-Skript wurde auf der MainCamera nicht gefunden!");
            }
        }
        else
        {
            Debug.LogError("Kein Objekt mit dem Tag 'MainCamera' gefunden!");
        }
    }

    private void Update()
    {
        if (targetGameObject == null || mouseLook == null)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            if (mouseLook.enabled)
            {
                mouseLook.enabled = false;
            }
        }
        else
        {
            if (targetGameObject.activeSelf)
            {
                if (mouseLook.enabled)
                {
                    mouseLook.enabled = false;
                }
            }
            else
            {
                if (!mouseLook.enabled)
                {
                    mouseLook.enabled = true;
                }
            }
        }
    }
}
