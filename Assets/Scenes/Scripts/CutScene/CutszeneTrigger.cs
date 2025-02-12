using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class CutszeneTrigger : MonoBehaviour
{
    public DialogueContainer triggerDialogue;
    public GameObject cutsceneCanvas;
    public VideoPlayer videoPlayer;
    public Button skipButton;

    private bool isCutsceneActive = false;
    private bool dialoguePlayed = false;

    void Start()
    {
        if (DialogueSystem.Instance != null)
        {
            DialogueSystem.Instance.OnDialogueEnd += HandleDialogueEnd;
        }

        if (cutsceneCanvas != null)
        {
            cutsceneCanvas.SetActive(false);
        }

        if (skipButton != null)
        {
            skipButton.gameObject.SetActive(false);
            skipButton.onClick.AddListener(SkipCutscene);
        }
    }

    private void HandleDialogueEnd()
    {
        if (!dialoguePlayed)
        {
            dialoguePlayed = true;
            Debug.Log("Dialog beendet! Cutscene wird vorbereitet...");
            StartCoroutine(WaitForDialogueToEnd());
        }
    }

    private IEnumerator WaitForDialogueToEnd()
    {
        while (DialogueSystem.IsDialogueActive)
        {
            yield return null;
        }
        StartCutscene();
    }

    private void StartCutscene()
    {
        if (isCutsceneActive || videoPlayer == null || cutsceneCanvas == null)
        {
            Debug.LogError("Cutscene konnte nicht gestartet werden! Entweder isCutsceneActive, VideoPlayer oder CutsceneCanvas ist NULL.");
            return;
        }

        Debug.Log("Cutscene wird gestartet!");
        isCutsceneActive = true;

        ToggleInteractivity(false);

        cutsceneCanvas.SetActive(true);
        Debug.Log("CutsceneCanvas aktiviert!");

        videoPlayer.Play();
        Debug.Log("Video gestartet!");

        videoPlayer.loopPointReached += OnCutsceneEnd;

        if (skipButton != null)
        {
            skipButton.gameObject.SetActive(true);
        }
    }

    private void SkipCutscene()
    {
        Debug.Log("Cutscene übersprungen!");
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }
        EndCutscene();
    }

    private void OnCutsceneEnd(VideoPlayer vp)
    {
        EndCutscene();
    }

    private void EndCutscene()
    {
        Debug.Log("Cutscene beendet!");
        isCutsceneActive = false;

        cutsceneCanvas.SetActive(false);
        if (skipButton != null)
        {
            skipButton.gameObject.SetActive(false);
        }

        ToggleInteractivity(true);
    }

    private void ToggleInteractivity(bool state)
    {
        GameObject[] interactables = GameObject.FindGameObjectsWithTag("Interactable");
        foreach (GameObject obj in interactables)
        {
            Collider col = obj.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = state;
            }
        }
    }

    private void OnDestroy()
    {
        if (DialogueSystem.Instance != null)
        {
            DialogueSystem.Instance.OnDialogueEnd -= HandleDialogueEnd;
        }

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnCutsceneEnd;
        }

        if (skipButton != null)
        {
            skipButton.onClick.RemoveListener(SkipCutscene);
        }
    }
}
