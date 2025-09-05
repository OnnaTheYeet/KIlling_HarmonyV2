using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    public HashSet<string> interactedObjects = new HashSet<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RegisterInteraction(string objectID)
    {
        if (!interactedObjects.Contains(objectID))
        {
            interactedObjects.Add(objectID);
        }
    }

    public bool HasInteracted(string objectID)
    {
        return interactedObjects.Contains(objectID);
    }
}
