using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausemenu;
    public bool isPaused;
    public static bool IsGamePaused;

    void Start()
    {
        pausemenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        IsGamePaused = true;
        DialogueSystem.PauseAllCoroutines();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        IsGamePaused = false;
        DialogueSystem.ResumeAllCoroutines();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
