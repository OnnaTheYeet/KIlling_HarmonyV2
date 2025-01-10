using UnityEngine;
using UnityEngine.UI;

public class BsPDialogueTrigger : MonoBehaviour
{
    public bool isInitiallyVisible = true;

    private Button buttonComponent;
    private bool hasVisibilityBeenSet = false;

    private void Awake()
    {
        buttonComponent = GetComponent<Button>();
        if (buttonComponent == null)
        {
            Debug.LogError("Keine Button-Komponente auf diesem GameObject gefunden!");
        }

        if (!hasVisibilityBeenSet)
        {
            CheckInitialVisibility();
            hasVisibilityBeenSet = true;
        }
    }

    private void Start()
    {
        DialogueSystem.Instance.OnDialogueEnd += EnableButton;
    }

    private void Update()
    {
        if (DialogueSystem.IsDialogueActive)
        {
            DisableButton();
        }
    }

    private void EnableButton()
    {
        if (buttonComponent != null)
        {
            buttonComponent.interactable = true;
        }
    }

    private void DisableButton()
    {
        if (buttonComponent != null)
        {
            buttonComponent.interactable = false;
        }
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

    private void OnDestroy()
    {
        if (DialogueSystem.Instance != null)
        {
            DialogueSystem.Instance.OnDialogueEnd -= EnableButton;
        }
    }
}
