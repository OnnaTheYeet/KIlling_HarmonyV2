using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableByTagOnSceneLoad : MonoBehaviour
{
    public string tagToCheck;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        TryDisableTaggedObject();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TryDisableTaggedObject();
    }

    private void TryDisableTaggedObject()
    {
        GameObject taggedObject = GameObject.FindWithTag(tagToCheck);

        if (taggedObject != null)
        {
            DisableObjectAndChildren(taggedObject);
        }
    }

    private void DisableObjectAndChildren(GameObject obj)
    {
        obj.SetActive(false);

        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
