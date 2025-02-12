using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SceneDialogueManager : MonoBehaviour
{
    [SerializeField] private List<DialogueContainer> dialogueContainers;
    private static HashSet<string> playedScenes = new HashSet<string>();

    private int currentDialogueIndex = 0;
    private bool isPlayingDialogue = false;

    private void Start()
    {
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (!playedScenes.Contains(currentSceneName) && dialogueContainers.Count > 0)
        {
            playedScenes.Add(currentSceneName);
            StartDialogueSequence();
        }
    }

    private void StartDialogueSequence()
    {
        if (currentDialogueIndex < dialogueContainers.Count && DialogueSystem.Instance != null)
        {
            isPlayingDialogue = true;

            DialogueSystem.Instance.InitiateDialogue(dialogueContainers[currentDialogueIndex]);

            DialogueSystem.Instance.OnDialogueEnd += HandleDialogueEnd;
        }
        else
        {
            Debug.Log("Alle Dialoge wurden abgespielt.");
            isPlayingDialogue = false;
        }
    }

    private void HandleDialogueEnd()
    {
        DialogueSystem.Instance.OnDialogueEnd -= HandleDialogueEnd;

        currentDialogueIndex++;
        StartDialogueSequence();
    }
}