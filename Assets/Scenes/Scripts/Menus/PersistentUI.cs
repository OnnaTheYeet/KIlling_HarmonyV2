using System.Collections.Generic;
using UnityEngine;

public class PersistentUI : MonoBehaviour
{
    public string category = "Default";

    private static Dictionary<string, PersistentUI> instances = new Dictionary<string, PersistentUI>();

    private void Awake()
    {
        if (instances.ContainsKey(category))
        {
            Destroy(gameObject);
            return;
        }

        instances[category] = this;
        DontDestroyOnLoad(gameObject);
    }
}
