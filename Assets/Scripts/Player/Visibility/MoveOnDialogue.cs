using UnityEngine;

public class MoveOnDialogue : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector3 targetPosition;
    public float moveDuration = 2f;

    [Header("Dialogue Settings")]
    public DialogueContainer triggerDialogue;

    private Vector3 startPosition;
    private float elapsedTime = 0f;
    private bool isMoving = false;

    private void Start()
    {
        startPosition = transform.position;

        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.OnDialogueFinished += HandleDialogueFinished;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / moveDuration;

            transform.position = Vector3.Lerp(startPosition, targetPosition, progress);

            if (progress >= 1f)
            {
                isMoving = false;
            }
        }
    }

    private void HandleDialogueFinished(DialogueContainer finishedDialogue)
    {
        Debug.Log("Dialogue finished detected: " + finishedDialogue.name);
        if (finishedDialogue == triggerDialogue)
        {
            Debug.Log("Matched dialogue. Starting movement.");
            StartMovement();
        }
        else
        {
            Debug.LogWarning("Dialogue did not match triggerDialogue.");
        }
    }


    private void StartMovement()
    {
        startPosition = transform.position;
        elapsedTime = 0f;
        isMoving = true;
    }
}
