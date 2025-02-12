using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;

    public CharacterUI currentCharacter;  // which character is selected?

    private void Awake()
    {
        Instance = this;
    }

    // Optionally, you can have a helper method to switch characters:
    public void SelectCharacter(CharacterUI newCharacter)
    {
        currentCharacter = newCharacter;
        // If you want to reset pages, etc., do it here
    }
}
