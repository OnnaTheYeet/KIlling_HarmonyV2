using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class VisibilityStateManager
{
    private static Dictionary<string, bool> visibilityStates = new Dictionary<string, bool>();

    public static void SetVisibilityState(GameObject obj, bool isVisible)
    {
        if (obj == null)
            return;

        string key = obj.name + SceneManager.GetActiveScene().name;
        visibilityStates[key] = isVisible;

        obj.SetActive(isVisible);
    }

    public static bool GetVisibilityState(GameObject obj, bool defaultValue)
    {
        if (obj == null)
            return defaultValue;

        string key = obj.name + SceneManager.GetActiveScene().name;
        if (visibilityStates.ContainsKey(key))
        {
            return visibilityStates[key];
        }

        return defaultValue;
    }

    public static void RestoreVisibilityState()
    {
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            bool isVisible = GetVisibilityState(obj, true);
            SetVisibilityState(obj, isVisible);
        }
    }
}
