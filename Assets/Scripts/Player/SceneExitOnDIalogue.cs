using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExitOnDialogue : MonoBehaviour
{
    public string targetDialogueName;
    private bool canExitScene = false;

    private void Update()
    {
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueFinished(targetDialogueName))
        {
            canExitScene = true;
        }

        if (canExitScene && Input.GetKeyDown(KeyCode.E))
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        int nextSceneIndex = SceneManager.GetSceneByName(currentScene).buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
