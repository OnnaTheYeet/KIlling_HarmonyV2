using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource;  
    public AudioClip buttonClickSound;  

    public void PlayGame()
    {
        PlaySound();
        SceneManager.LoadSceneAsync(1);
    }

    public void ExitGame()
    {
        PlaySound();
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    private void PlaySound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);  
        }
    }
}
