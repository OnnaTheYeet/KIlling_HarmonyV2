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
            DialogueManager.Instance.OnDialogueStarted += HandleDialogueStarted;
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
    }

    private void DisableDarkness()
    {
        if (darknessObject != null)
        {
            darknessObject.SetActive(false);
            Debug.Log($"Darkness object '{darknessObject.name}' has been disabled.");
        }
        else
        {
            Debug.LogWarning("Darkness object is not assigned in the inspector.");
        }
    }
}
