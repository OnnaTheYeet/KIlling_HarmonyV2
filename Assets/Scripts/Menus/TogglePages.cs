using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePages : MonoBehaviour
{

    public void TogglePage()
    {
        // Which character is selected?
        CharacterUI current = CharacterManager.Instance.currentCharacter;

        // If no character is selected, do nothing
        if (current == null) return;

        // Toggle their pages
        bool onFirstPage = current.firstPage.activeSelf;

        current.firstPage.SetActive(!onFirstPage);
        current.secondPage.SetActive(onFirstPage);
    }
}

