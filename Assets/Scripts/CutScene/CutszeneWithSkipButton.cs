using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class CutszeneWithSkipButton : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    public Button skipButton;
    private bool buttonVisible = false;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("Kein VideoPlayer auf diesem GameObject gefunden!");
        }

        if (skipButton != null)
        {
            skipButton.gameObject.SetActive(false);
            skipButton.onClick.AddListener(SkipCutscene);
            Debug.Log("Skip-Button korrekt initialisiert");
        }
        else
        {
            Debug.LogError("Skip-Button nicht zugewiesen! Bitte im Inspector zuweisen.");
        }
    }

    void Update()
    {
        if (!buttonVisible && Input.GetMouseButtonDown(0))
        {
            ShowSkipButton();
        }
    }

    private void ShowSkipButton()
    {
        if (skipButton != null)
        {
            skipButton.gameObject.SetActive(true);
            Debug.Log("Skip-Button sichtbar gemacht");
            buttonVisible = true;
        }
        else
        {
            Debug.LogError("Skip-Button ist null! Kann nicht angezeigt werden.");
        }
    }

    private void SkipCutscene()
    {
        Debug.Log("Skip-Button gedrückt!");
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }
        LoadNextScene();
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        Debug.Log("Lade nächste Szene...");
        SceneManager.LoadScene(1);
    }

    void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
        if (skipButton != null)
        {
            skipButton.onClick.RemoveListener(SkipCutscene);
        }
    }
}
