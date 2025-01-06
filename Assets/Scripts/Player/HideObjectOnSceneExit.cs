using UnityEngine;
using UnityEngine.SceneManagement;

public class HideObjectOnSceneExit : MonoBehaviour
{
    public string targetSceneName;
    private InvisibleObjectManager invisibleObjectManager;

    private void Start()
    {
        invisibleObjectManager = InvisibleObjectManager.Instance;

        if (invisibleObjectManager != null)
        {
            invisibleObjectManager.RegisterInvisibleObjectOnExit(gameObject, targetSceneName);
        }
    }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == targetSceneName)
        {
            if (invisibleObjectManager != null)
            {
                invisibleObjectManager.CheckInvisibleObjectStatus(gameObject, targetSceneName);
            }
        }
    }

    public void DestroyObject(GameObject obj)
    {
        Destroy(obj);
    }
}