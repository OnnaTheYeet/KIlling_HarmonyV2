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

    private void Awake()
    {
        CheckInitialVisibility();
    }

    private void OnEnable()
    {
        CheckInitialVisibility();
    }

    private void Start()
    {
        cam = Camera.main;
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
        if (!canInteract || !IsCharacterVisible())
        {
            return;
        }

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

        SetCharacterInteractivity(false);

        VisibilityStateManager.SetVisibilityState(gameObject, false);
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
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        Renderer mainRenderer = GetComponent<Renderer>();
        if (mainRenderer != null)
        {
            mainRenderer.enabled = true;
        }

        Collider mainCollider = GetComponent<Collider>();
        if (mainCollider != null)
        {
            mainCollider.enabled = true;
        }

        foreach (Transform child in transform)
        {
            Renderer childRenderer = child.GetComponent<Renderer>();
            if (childRenderer != null)
            {
                childRenderer.enabled = true;
            }

            Collider childCollider = child.GetComponent<Collider>();
            if (childCollider != null)
            {
                childCollider.enabled = true;
            }
        }

        SetCharacterInteractivity(true);

        VisibilityStateManager.SetVisibilityState(gameObject, true);
    }

    public void MakeCharacterInvisible()
    {
        gameObject.SetActive(false);

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

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        SetCharacterInteractivity(false);

        VisibilityStateManager.SetVisibilityState(gameObject, false);
    }

    private void CheckInitialVisibility()
    {
        bool savedState = VisibilityStateManager.GetVisibilityState(gameObject, isInitiallyVisible);
        if (savedState)
        {
            MakeCharacterVisible();
        }
        else
        {
            MakeCharacterInvisible();
        }
    }

    private bool IsCharacterVisible()
    {
        Renderer mainRenderer = GetComponent<Renderer>();
        return mainRenderer != null && mainRenderer.enabled;
    }
}
