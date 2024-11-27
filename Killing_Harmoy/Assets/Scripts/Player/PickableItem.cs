using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public string ItemName;
    public void PickUp()
    {
        Debug.Log("Got item" + ItemName);
        Destroy(gameObject);
    }
}
