using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneVisibilityController : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RestoreVisibilityState();
    }

    private void RestoreVisibilityState()
    {
        VisibilityStateManager.RestoreVisibilityState();
    }
}
