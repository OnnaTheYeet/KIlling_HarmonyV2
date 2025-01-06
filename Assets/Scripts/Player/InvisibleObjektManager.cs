using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class InvisibleObjectManager : MonoBehaviour
{
    public static InvisibleObjectManager Instance;

    private Dictionary<string, GameObject> invisibleObjectsInScene = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterInvisibleObjectOnExit(GameObject obj, string sceneName)
    {
        string objectID = GetObjectID(obj, sceneName);
        if (!invisibleObjectsInScene.ContainsKey(objectID))
        {
            invisibleObjectsInScene.Add(objectID, obj);
        }
    }

    public void CheckInvisibleObjectStatus(GameObject obj, string sceneName)
    {
        string objectID = GetObjectID(obj, sceneName);
        if (invisibleObjectsInScene.ContainsKey(objectID))
        {
            DestroyObject(obj);
        }
        else
        {
            obj.SetActive(true);
        }
    }

    private void DestroyObject(GameObject obj)
    {
        Destroy(obj);
    }

    private string GetObjectID(GameObject obj, string sceneName)
    {
        return sceneName + "_" + obj.name;
    }

    public void HandleSceneChange()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        foreach (var kvp in invisibleObjectsInScene)
        {
            string objectID = kvp.Key;
            if (objectID.StartsWith(currentScene))
            {
                kvp.Value.SetActive(false);
                Destroy(kvp.Value);
            }
        }
    }
}
