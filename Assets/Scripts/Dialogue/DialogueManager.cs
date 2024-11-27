using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    private HashSet<DialogueContainer> playedDialogues = new HashSet<DialogueContainer>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public bool HasDialoguePlayed(DialogueContainer dialogue)
    {
        return playedDialogues.Contains(dialogue);
    }

    public void MarkDialogueAsPlayed(DialogueContainer dialogue)
    {
        if (!playedDialogues.Contains(dialogue))
        {
            playedDialogues.Add(dialogue);
        }
    }
}
