using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class VisibilityStateManager
{
    private static Dictionary<string, bool> visibilityStates = new Dictionary<string, bool>();

    public static void SetVisibilityState(GameObject obj, bool isVisible, bool isPersistent = false)
    {
        if (obj == null)
            return;

        string key = isPersistent ? obj.name : obj.name + SceneManager.GetActiveScene().name;
        visibilityStates[key] = isVisible;

        obj.SetActive(isVisible);
    }

    public static bool GetVisibilityState(GameObject obj, bool defaultValue, bool isPersistent = false)
    {
        if (obj == null)
            return defaultValue;

        string key = isPersistent ? obj.name : obj.name + SceneManager.GetActiveScene().name;
        if (visibilityStates.ContainsKey(key))
        {
            return visibilityStates[key];
        }

        return defaultValue;
    }

    public static void RestoreVisibilityState(bool isPersistent = false)
    {
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            bool isVisible = GetVisibilityState(obj, true, isPersistent);
            SetVisibilityState(obj, isVisible, isPersistent);
        }
    }
}
