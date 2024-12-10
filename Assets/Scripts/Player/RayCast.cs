using UnityEngine;
using UnityEngine.SceneManagement;

public class RayCast : MonoBehaviour
{
    private Camera cam;
    public int scene;
    public bool NeedKey = true;
    public DialogueSystem dialogueSystem;
    public DialogueContainer doorLockedDialogue;
    public DialogueContainer doorUnlockedDialogue;

    public CharacterDialogueTrigger targetDialogueTrigger;

    void Start()
    {
        cam = Camera.main;
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
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
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
        if (NeedKey)
        {
            if (Inventory.GetKey)
            {
                OpenDoor();
                PlayUnlockedDialogue();
                if (targetDialogueTrigger != null)
                {
                    targetDialogueTrigger.MakeCharacterVisible();
                }
            }
            else
            {
                PlayLockedDialogue();
                if (targetDialogueTrigger != null)
                {
                    targetDialogueTrigger.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            OpenDoor();
            PlayUnlockedDialogue();
        }
    }

    public void OpenDoor()
    {
        Debug.Log("Tür wird geöffnet...");
        SceneManager.LoadScene(scene);
    }

    private void PlayLockedDialogue()
    {
        if (doorLockedDialogue != null && dialogueSystem != null)
        {
            dialogueSystem.InitiateDialogue(doorLockedDialogue);
        }
    }

    private void PlayUnlockedDialogue()
    {
        if (doorUnlockedDialogue != null && dialogueSystem != null)
        {
            dialogueSystem.InitiateDialogue(doorUnlockedDialogue);
        }
    }
}
