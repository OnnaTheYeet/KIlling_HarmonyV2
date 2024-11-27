using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; private set; }

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI nameTag;
    [SerializeField] Canvas dialogueCanvas;
    DialogueContainer currentDialogue;

    [SerializeField] [Range(0f, 1f)] float visibleTextPercent;
    [SerializeField] float timePerLetter = 0.05f;
    float totalTimeToType, currentTime;
    [SerializeField] float skipTextWaitTime = 0.1f;
    [SerializeField] SpriteManager spriteManager;
    [SerializeField] SpriteManager backgroundManager;

    string lineToShow;

    int index;
    Coroutine skipTextCoroutine;
    bool isDialogueActive = false;

    [SerializeField] DialogueContainer debugDialogueContainer;

    public static bool IsDialogueActive { get; private set; } = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        dialogueCanvas.gameObject.SetActive(false);
        if (debugDialogueContainer != null)
        {
            InitiateDialogue(debugDialogueContainer);
        }
    }

    private void Update()
    {
        if (PauseMenu.IsGamePaused)
            return;

        if (isDialogueActive && Input.GetMouseButtonDown(0))
        {
            PushText();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            skipTextCoroutine = StartCoroutine(SkipText());
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            if (skipTextCoroutine != null)
            {
                StopCoroutine(skipTextCoroutine);
            }
        }

        if (isDialogueActive)
        {
            TypeOutText();
        }
    }

    public void InitiateDialogue(DialogueContainer dialogueContainer)
    {
        if (dialogueContainer.playOnlyOnce && DialogueManager.Instance.HasDialoguePlayed(dialogueContainer))
        {
            Debug.Log("Dieser Dialog wurde bereits abgespielt.");
            return;
        }

        currentDialogue = dialogueContainer;
        index = 0;
        isDialogueActive = true;
        IsDialogueActive = true;

        text.text = "";
        nameTag.text = "";
        dialogueCanvas.gameObject.SetActive(true);

        CycleLine();

        if (dialogueContainer.playOnlyOnce)
        {
            DialogueManager.Instance.MarkDialogueAsPlayed(dialogueContainer);
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        IsDialogueActive = false;

        text.text = "";
        nameTag.text = "";

        dialogueCanvas.gameObject.SetActive(false);

        Debug.Log("Dialogue finished");
    }

    private void TypeOutText()
    {
        if (visibleTextPercent >= 1f) return;

        currentTime += Time.deltaTime;
        visibleTextPercent = currentTime / totalTimeToType;
        visibleTextPercent = Mathf.Clamp(visibleTextPercent, 0f, 1f);
        UpdateText();
    }

    void UpdateText()
    {
        int totalLetterToShow = (int)(lineToShow.Length * visibleTextPercent);
        text.text = lineToShow.Substring(0, totalLetterToShow);
    }

    private void PushText()
    {
        if (visibleTextPercent < 1f)
        {
            visibleTextPercent = 1f;
            UpdateText();
            return;
        }

        CycleLine();
    }

    private void CycleLine()
    {
        if (index >= currentDialogue.lines.Count)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = currentDialogue.lines[index];

        if (line.spriteChanges != null)
        {
            for (int i = 0; i < line.spriteChanges.Count; i++)
            {
                if (line.spriteChanges[i].actor == null)
                {
                    spriteManager.Hide(line.spriteChanges[i].onScreenImageID);
                    continue;
                }
                int expressionID = line.spriteChanges[i].expression;
                spriteManager.Set(line.spriteChanges[i].actor.sprites[expressionID], line.spriteChanges[i].onScreenImageID);
            }
        }

        if (line.backgroundChanges != null)
        {
            for (int i = 0; i < line.backgroundChanges.Count; i++)
            {
                if (line.backgroundChanges[i].sprite == null)
                {
                    backgroundManager.Hide(line.backgroundChanges[i].onScreenImageID);
                    continue;
                }
                backgroundManager.Set(line.backgroundChanges[i].sprite, line.backgroundChanges[i].onScreenImageID);
            }
        }

        lineToShow = line.line;

        if (currentDialogue.lines[index].actor != null)
        {
            nameTag.text = currentDialogue.lines[index].actor.Name;
        }
        else
        {
            nameTag.text = "";
        }

        totalTimeToType = lineToShow.Length * timePerLetter;
        currentTime = 0f;
        visibleTextPercent = 0f;
        text.text = "";

        index += 1;
    }

    IEnumerator SkipText()
    {
        while (true)
        {
            yield return new WaitForSeconds(skipTextWaitTime);
            PushText();
        }
    }

    public static void PauseAllCoroutines()
    {
        if (Instance != null)
        {
            Instance.StopAllCoroutines();
        }
    }

    public static void ResumeAllCoroutines()
    {
        if (Instance != null && Instance.isDialogueActive)
        {
            Instance.TypeOutText();
        }
    }
}
