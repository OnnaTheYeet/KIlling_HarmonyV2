using UnityEngine;

public class MouseLookController : MonoBehaviour
{
    public GameObject targetGameObject;
    private MouseLook mouseLook;

    private void Awake()
    {
        FindMouseLookComponent();
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
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

    private void FindMouseLookComponent()
    {
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

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        FindMouseLookComponent();

        if (targetGameObject == null)
        {
            Debug.LogWarning("Kein Ziel-GameObject gesetzt. Bitte sicherstellen, dass es persistiert oder in der neuen Szene gesetzt wird.");
        }
    }
}
