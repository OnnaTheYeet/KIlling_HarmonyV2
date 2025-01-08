using UnityEngine;

public class DestroyOnSceneExit : MonoBehaviour
{
    private void OnDestroy()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        Destroy(gameObject);
    }
}
