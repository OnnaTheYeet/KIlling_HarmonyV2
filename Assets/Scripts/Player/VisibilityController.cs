using System.Collections.Generic;
using UnityEngine;

public class VisibilityController : MonoBehaviour
{
    [Header("Dialog Settings")]
    public DialogueContainer triggerDialogue;

    [Tooltip("Visibility change timing: True = At dialogue start, False = At dialogue end")]
    public bool changeVisibilityAtStart = false;

    [Header("Targets to Show")]
    public List<GameObject> objectsToShow;

    private void Start()
    {
        if (DialogueManager.Instance != null)
        {
            if (changeVisibilityAtStart)
            {
                DialogueManager.Instance.OnDialogueStarted += HandleDialogueStarted;
            }
            else
            {
                DialogueManager.Instance.OnDialogueFinished += HandleDialogueFinished;
            }
        }
    }

    private void HandleDialogueStarted(DialogueContainer startedDialogue)
    {
        if (startedDialogue == triggerDialogue)
        {
            ShowObjects();
        }
    }

    private void HandleDialogueFinished(DialogueContainer finishedDialogue)
    {
        if (finishedDialogue == triggerDialogue)
        {
            ShowObjects();
        }
    }

    private void ShowObjects()
    {
        foreach (GameObject obj in objectsToShow)
        {
            SetVisibility(obj, true);
        }
    }

    private void SetVisibility(GameObject obj, bool visible)
    {
        if (obj == null) return;

        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = visible;
        }

        BoxCollider boxCollider = obj.GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            boxCollider.enabled = visible;
        }
    }
}
