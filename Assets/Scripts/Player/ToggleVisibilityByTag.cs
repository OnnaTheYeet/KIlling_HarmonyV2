using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleVisibilityByTag : MonoBehaviour
{
    public string tagToCheck;

    public GameObject objectToHide;

    void Update()
    {
        GameObject taggedObject = GameObject.FindWithTag(tagToCheck);

        if (taggedObject != null && objectToHide != null)
        {
            SetObjectVisibility(objectToHide, false);
        }
        else if (objectToHide != null)
        {
            SetObjectVisibility(objectToHide, true);
        }
    }

    private void SetObjectVisibility(GameObject obj, bool isVisible)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = isVisible;
        }

        foreach (Renderer childRenderer in obj.GetComponentsInChildren<Renderer>())
        {
            childRenderer.enabled = isVisible;
        }
    }
}
