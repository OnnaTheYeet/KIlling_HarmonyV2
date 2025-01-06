using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(9);
    }
    public void ExitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
