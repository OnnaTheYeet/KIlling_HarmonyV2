using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool GetKey = false;
    public static bool HasRequiredItem = false;
    public GameObject requiredItem;

    public void AcquireItem()
    {
        HasRequiredItem = true;
        if (requiredItem != null)
        {
            Destroy(requiredItem);
        }
    }
}
