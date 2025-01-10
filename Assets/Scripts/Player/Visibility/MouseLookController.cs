using UnityEngine;

public class MouseLookController : MonoBehaviour
{
    public GameObject targetGameObject; // Referenz zum spezifischen GameObject
    private MouseLook mouseLook; // Referenz zum MouseLook-Skript

    private void Awake()
    {
        // Überprüfe, ob eine Referenz zum Ziel-GameObject vorhanden ist
        if (targetGameObject == null)
        {
            Debug.LogError("Kein Ziel-GameObject zugewiesen! Bitte das Ziel-GameObject im Inspector zuweisen.");
            return;
        }

        // Suche nach der MainCamera und dem MouseLook-Skript
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
            return; // Abbrechen, wenn keine Referenz vorhanden ist
        }

        // Wenn das Ziel-GameObject aktiv ist, deaktiviere MouseLook
        if (targetGameObject.activeSelf)
        {
            if (mouseLook.enabled)
            {
                mouseLook.enabled = false;
            }
        }
        // Wenn das Ziel-GameObject nicht aktiv ist, aktiviere MouseLook
        else
        {
            if (!mouseLook.enabled)
            {
                mouseLook.enabled = true;
            }
        }
    }
}
