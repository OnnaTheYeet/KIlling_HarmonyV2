using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    public void PlaySound()
    {
        audioSource.Play();
    }
}
