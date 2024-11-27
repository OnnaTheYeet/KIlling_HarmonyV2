using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnMouseDown()
    {
        Inventory.GetKey = true;
        Destroy(gameObject);
    }
}
