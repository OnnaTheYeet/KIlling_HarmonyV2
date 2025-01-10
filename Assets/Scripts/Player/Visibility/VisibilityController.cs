using System.Collections.Generic;
using UnityEngine;

public class VisibilityController : MonoBehaviour
{
    [Header("Dialog Settings")]
    public DialogueContainer triggerDialogue;

    [Tooltip("Visibility change timing: True = At dialogue start, False = At dialogue end")]
    public bool changeVisibilityAtStart = true;

    public bool changeVisibilityAtEnd = false;

    [Header("Targets to Show")]
    public List<GameObject> objectsToShow;

    [Header("Targets to Hide")]
    public List<GameObject> objectsToHide;

    private void Start()
    {
        RestoreVisibilityState();

        if (DialogueManager.Instance != null)
        {
            if (changeVisibilityAtStart)
            {
                DialogueManager.Instance.OnDialogueStarted += HandleDialogueStarted;
            }

            if (changeVisibilityAtEnd)
            {
                DialogueManager.Instance.OnDialogueFinished += HandleDialogueFinished;
            }
        }
    }

    private void OnDestroy()
    {
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.OnDialogueStarted -= HandleDialogueStarted;
            DialogueManager.Instance.OnDialogueFinished -= HandleDialogueFinished;
        }
    }

    private void HandleDialogueStarted(DialogueContainer startedDialogue)
    {
        if (startedDialogue == triggerDialogue && changeVisibilityAtStart)
        {
            UpdateVisibility();
        }
    }

    private void HandleDialogueFinished(DialogueContainer finishedDialogue)
    {
        if (finishedDialogue == triggerDialogue && changeVisibilityAtEnd)
        {
            UpdateVisibility();
        }
    }

    private void UpdateVisibility()
    {
        foreach (GameObject obj in objectsToShow)
        {
            if (obj != null)
            {
                bool isPersistent = obj.GetComponent<PersistentUI>() != null;
                VisibilityStateManager.SetVisibilityState(obj, true, isPersistent);
            }
        }

        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
            {
                bool isPersistent = obj.GetComponent<PersistentUI>() != null;
                VisibilityStateManager.SetVisibilityState(obj, false, isPersistent);
            }
        }

        SaveVisibilityState();
    }

    private void RestoreVisibilityState()
    {
        foreach (GameObject obj in objectsToShow)
        {
            if (obj != null)
            {
                bool isPersistent = obj.GetComponent<PersistentUI>() != null;
                bool visible = VisibilityStateManager.GetVisibilityState(obj, true, isPersistent);
                SetVisibility(obj, visible);
            }
        }

        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
            {
                bool isPersistent = obj.GetComponent<PersistentUI>() != null;
                bool visible = VisibilityStateManager.GetVisibilityState(obj, true, isPersistent);
                SetVisibility(obj, visible);
            }
        }
    }

    private void SaveVisibilityState()
    {
        foreach (GameObject obj in objectsToShow)
        {
            if (obj != null)
            {
                bool isActive = obj.activeSelf;
                VisibilityStateManager.SetVisibilityState(obj, isActive);
            }
        }

        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
            {
                bool isActive = obj.activeSelf;
                VisibilityStateManager.SetVisibilityState(obj, isActive);
            }
        }
    }

    private void SetVisibility(GameObject obj, bool visible)
    {
        if (obj == null) return;

        obj.SetActive(visible);

        VisibilityStateManager.SetVisibilityState(obj, visible);
    }
}
