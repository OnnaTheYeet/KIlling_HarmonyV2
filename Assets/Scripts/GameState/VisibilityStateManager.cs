using System.Collections.Generic;
using UnityEngine;

public static class VisibilityStateManager
{
    private static Dictionary<string, bool> visibilityStates = new Dictionary<string, bool>();

    public static void SetVisibilityState(GameObject obj, bool visible)
    {
        if (obj == null) return;

        string key = obj.name;
        if (visibilityStates.ContainsKey(key))
        {
            visibilityStates[key] = visible;
        }
        else
        {
            visibilityStates.Add(key, visible);
        }
    }

    public static bool GetVisibilityState(GameObject obj, bool defaultValue)
    {
        if (obj == null) return defaultValue;

        string key = obj.name;
        if (visibilityStates.TryGetValue(key, out bool value))
        {
            return value;
        }

        return defaultValue;
    }
}
