using System.Collections;
using UnityEngine;

public class CharacterDialogueTrigger : MonoBehaviour
{
    public DialogueContainer characterDialogue;
    public bool disableCharacterAfterDialogue = false;
    public bool requireItemForInteraction = false;
    public GameObject itemToRequire;
    public bool isInitiallyVisible = true;

    private Camera cam;
    private bool canInteract = true;

    private void Start()
    {
        cam = Camera.main;

        if (isInitiallyVisible)
        {
            MakeCharacterVisible();
        }
        else
        {
            MakeCharacterInvisible();
        }
    }

    private void Update()
    {
        if (DialogueSystem.IsDialogueActive || PauseMenu.IsGamePaused || !canInteract)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    TriggerDialogue();
                }
            }
        }
    }

    private void TriggerDialogue()
    {
        if (characterDialogue != null)
        {
            Debug.Log("Triggering dialogue: " + characterDialogue.name);
            DialogueSystem.Instance.InitiateDialogue(characterDialogue);

            if (disableCharacterAfterDialogue)
            {
                StartCoroutine(DisableCharacterAfterDialogue());
            }
        }
        else
        {
            Debug.LogError("Character dialogue is null!");
        }
    }

    private IEnumerator DisableCharacterAfterDialogue()
    {
        while (DialogueSystem.IsDialogueActive)
        {
            yield return null;
        }

        DisableCharacterAndChildren();
    }

    private void DisableCharacterAndChildren()
    {
        gameObject.SetActive(false);

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);

            Collider childCollider = child.GetComponent<Collider>();
            if (childCollider != null)
            {
                childCollider.enabled = false;
            }

            Renderer childRenderer = child.GetComponent<Renderer>();
            if (childRenderer != null)
            {
                childRenderer.enabled = false;
            }
        }
    }

    public void SetCharacterInteractivity(bool hasItem)
    {
        canInteract = requireItemForInteraction ? hasItem : true;
    }

    public void UpdateCharacterInteractivity()
    {
        bool hasItem = Inventory.HasRequiredItem;
        SetCharacterInteractivity(hasItem);
    }

    public void MakeCharacterVisible()
    {
        gameObject.SetActive(true);

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }

        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
        }

        SetCharacterInteractivity(true);
    }

    public void MakeCharacterInvisible()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }

        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        SetCharacterInteractivity(false);
    }
}
