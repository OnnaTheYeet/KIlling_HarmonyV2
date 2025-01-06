using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    private bool isDialogueFinished = false;
    private string currentDialogueName;

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

    public void StartDialogue(string dialogueName)
    {
        currentDialogueName = dialogueName;
        isDialogueFinished = false;
    }

    public bool IsDialogueFinished(string dialogueName)
    {
        if (currentDialogueName == dialogueName)
        {
            return isDialogueFinished;
        }
        return false;
    }

    public void EndDialogue()
    {
        isDialogueFinished = true;
    }
}
