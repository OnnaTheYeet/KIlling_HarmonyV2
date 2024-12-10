using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnMouseDown()
    {
        Inventory.GetKey = true;
        Destroy(gameObject);
    }
}
