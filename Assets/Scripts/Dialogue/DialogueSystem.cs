using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; private set; }
    public event Action OnDialogueEnd;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI nameTag;
    [SerializeField] Canvas dialogueCanvas;
    DialogueContainer currentDialogue;

    [SerializeField][Range(0f, 1f)] float visibleTextPercent;
    [SerializeField] float timePerLetter = 0.05f;
    float totalTimeToType, currentTime;
    [SerializeField] float skipTextWaitTime = 0.1f;
    [SerializeField] SpriteManager spriteManager;
    [SerializeField] SpriteManager backgroundManager;

    [SerializeField] TMP_FontAsset defaultFontAsset;

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

        if (defaultFontAsset == null)
        {
            Debug.LogError("Default Font Asset (LiberationSans SDF) is not assigned!");
        }
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

        if (DialogueManager.Instance != null && currentDialogue != null)
        {
            DialogueManager.Instance.MarkDialogueAsPlayed(currentDialogue);
        }

        Debug.Log("Dialogue finished");

        OnDialogueEnd?.Invoke();
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

        text.font = line.fontAsset != null ? line.fontAsset : currentDialogue.fontAsset ?? defaultFontAsset;
        text.fontSize = line.fontSize != 0 ? line.fontSize : currentDialogue.fontSize;
        text.color = line.fontColor != Color.clear ? line.fontColor : currentDialogue.fontColor;

        if (dialogueCanvas.TryGetComponent(out Image dialogBoxImage))
        {
            if (line.dialogBoxBackground != null)
            {
                dialogBoxImage.sprite = line.dialogBoxBackground;
            }
            else if (currentDialogue.dialogBoxBackground != null)
            {
                dialogBoxImage.sprite = currentDialogue.dialogBoxBackground;
            }
        }

        lineToShow = line.line;

        nameTag.text = line.actor != null ? line.actor.Name : "";

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

    void OnGUI()
    {
        if (Event.current.type != EventType.Layout && Event.current.type != EventType.Repaint)
        {
            Debug.Log($"Unexpected event: {Event.current.type}");
        }
    }

}
