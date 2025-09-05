using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private string objectID;

    private void Start()
    {
        objectID = gameObject.name;

        if (GameStateManager.Instance.HasInteracted(objectID))
        {
            HandleAlreadyInteractedState();
        }
    }

    public void Interact()
    {
        Debug.Log($"Interacted with {objectID}");
        GameStateManager.Instance.RegisterInteraction(objectID);

        HandleInteraction();
    }

    private void HandleInteraction()
    {
        gameObject.SetActive(false);
    }

    private void HandleAlreadyInteractedState()
    {
        gameObject.SetActive(false);
    }
}
