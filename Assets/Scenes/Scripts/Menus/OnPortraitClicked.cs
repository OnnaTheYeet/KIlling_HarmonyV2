using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPortraitClicked : MonoBehaviour
{
    public void OnPortraitClick()
    {
        // e.g. if this script is on the same GameObject that has CharacterUI
        CharacterUI myCharacterUI = GetComponent<CharacterUI>();
        CharacterManager.Instance.SelectCharacter(myCharacterUI);
    }

}
