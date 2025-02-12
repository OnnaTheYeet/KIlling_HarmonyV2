using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Cutszene : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    public int sceneToLoad = 0;  

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    public void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}
