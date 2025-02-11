using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class RayCast : MonoBehaviour
{
    private Camera cam;
    public int firstScene;  // Intro-Cutscene Szene (Szene 1)
    public int secondScene;  // Level 1 (Szene 2)
    public int thirdScene;   // Cutscene Szene (Szene 3)
    public int fourthScene;  // Level 2 (Szene 4)

    private VideoPlayer videoPlayer;

    void Start()
    {
        cam = Camera.main;
        videoPlayer = FindObjectOfType<VideoPlayer>();  // Findet den VideoPlayer in der Szene
    }

    void Update()
    {
        if (DialogueSystem.IsDialogueActive || PauseMenu.IsGamePaused)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000000))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    HandleInteraction();
                }
            }
        }
    }

    private void HandleInteraction()
    {
        if (PlayerPrefs.GetInt("HasCutscenePlayed", 0) == 0)  // Überprüft, ob die Cutscene in Szene 3 abgespielt wurde
        {
            SceneManager.LoadScene(thirdScene);  // Szene 3 mit Cutscene
            PlayCutscene();
        }
        else
        {
            SceneManager.LoadScene(fourthScene);  // Wenn die Cutscene schon abgespielt wurde, gehe direkt zu Szene 4
        }
    }

    public void PlayCutscene()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();  // Video für die Cutscene starten
        }

        // Nachdem die Cutscene abgespielt wurde, speichere, dass sie gesehen wurde
        PlayerPrefs.SetInt("HasCutscenePlayed", 1);  // Setzt das Flag, dass die Cutscene abgespielt wurde
        PlayerPrefs.Save();  // Speichert den Wert
    }

    public void GoToNextLevel()
    {
        if (PlayerPrefs.GetInt("HasCutscenePlayed", 0) == 1)
        {
            // Wenn die Cutscene in Szene 3 bereits abgespielt wurde, gehe direkt zu Szene 4
            SceneManager.LoadScene(fourthScene);
        }
    }

    public void ReturnToLevel2()
    {
        // Wenn du von Szene 4 zurück zu Szene 2 gehst, wird keine Cutscene in Szene 2 abgespielt
        SceneManager.LoadScene(secondScene);
    }

    public void ReturnToScene4()
    {
        // Wenn du von Szene 2 auf Szene 4 gehst, nach der Cutscene in Szene 3, gehe direkt zu Szene 4
        SceneManager.LoadScene(fourthScene);
    }
}
