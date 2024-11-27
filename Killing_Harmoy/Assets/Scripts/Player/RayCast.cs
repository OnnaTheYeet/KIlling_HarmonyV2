using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RayCast : MonoBehaviour
{
    Camera cam;
    public int scene;
    public bool NeedKey = true;
    public DialogueSystem dialogueSystem;
    public DialogueContainer doorLockedDialogue;
    public DialogueContainer doorUnlockedDialogue;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {

        if (DialogueSystem.IsDialogueActive|| PauseMenu.IsGamePaused)
        {
            return;
        }


        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (NeedKey)
                    {
                        if (Inventory.GetKey)
                        {
                            OpenDoor();
                            if (!GameState.DoorDialoguePlayed)
                            {
                                PlayUnlockedDialogue();
                                GameState.DoorDialoguePlayed = true;
                            }
                        }
                        else
                        {
                            PlayLockedDialogue();
                        }
                    }
                    else
                    {
                        OpenDoor();
                        if (!GameState.DoorDialoguePlayed)
                        {
                            PlayUnlockedDialogue();
                            GameState.DoorDialoguePlayed = true;
                        }
                    }
                }
            }
        }
    }


    public void OpenDoor()
    {
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
