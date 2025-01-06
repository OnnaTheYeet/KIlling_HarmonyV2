using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    private HashSet<string> invisibleObjects = new HashSet<string>();

    private HashSet<string> interactedObjects = new HashSet<string>();

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


    public void MarkObjectAsInvisible(string objectID)
    {
        if (!invisibleObjects.Contains(objectID))
        {
            invisibleObjects.Add(objectID);
        }
    }

    public bool IsObjectInvisible(string objectID)
    {
        return invisibleObjects.Contains(objectID);
    }


    /// <summary>
    /// Prüft, ob mit einem bestimmten Objekt bereits interagiert wurde.
    /// </summary>
    public bool HasInteracted(string objectID)
    {
        return interactedObjects.Contains(objectID);
    }

    /// <summary>
    /// Registriert die Interaktion mit einem bestimmten Objekt.
    /// </summary>
    public void RegisterInteraction(string objectID)
    {
        if (!interactedObjects.Contains(objectID))
        {
            interactedObjects.Add(objectID);
        }
    }
}
