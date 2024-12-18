using UnityEngine;

public class DarknessController : MonoBehaviour
{
    [Header("Darkness Object")]
    [Tooltip("The darkness object to disable.")]
    public GameObject darknessObject;

    [Header("Trigger Dialogue")]
    [Tooltip("The dialogue that triggers disabling the darkness.")]
    public DialogueContainer triggerDialogue;

    private void Start()
    {
        if (DialogueManager.Instance != null)
        {
            Debug.Log("DialogueManager.Instance gefunden.");
            DialogueManager.Instance.OnDialogueStarted += HandleDialogueStarted;
        }
        else
        {
            Debug.LogError("DialogueManager.Instance ist null! Stelle sicher, dass der DialogueManager in der Szene vorhanden ist.");
        }
    }


    private void OnDestroy()
    {
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.OnDialogueStarted -= HandleDialogueStarted;
        }
    }

    private void HandleDialogueStarted(DialogueContainer startedDialogue)
    {
        if (startedDialogue == triggerDialogue)
        {
            DisableDarkness();
        }
        else
        {
            Debug.Log($"Dialog '{startedDialogue.name}' is not the trigger dialogue.");
        }
    }


    private void DisableDarkness()
    {
        if (darknessObject != null)
        {
            darknessObject.SetActive(false);
            Debug.Log($"Darkness object '{darknessObject.name}' and its children have been disabled.");
        }
        else
        {
            Debug.LogWarning("Darkness object is not assigned in the inspector.");
        }
    }

}
