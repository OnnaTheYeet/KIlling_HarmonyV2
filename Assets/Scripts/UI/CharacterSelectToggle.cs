using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectToggle : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite selectSprite;      
    public Sprite unselectSprite;     
    
    private Image buttonImage;      
    private bool isSelected = false;   

    void Start()
    {
        
        buttonImage = GetComponent<Image>(); 
        UpdateButtonSprite();
    }

    
    public void ToggleSelect()
    {
        
        isSelected = !isSelected;
        
        
        UpdateButtonSprite();
    }

    private void UpdateButtonSprite()
    {
        if (isSelected)
        {
            buttonImage.sprite = selectSprite;
        }
        else
        {
            buttonImage.sprite = unselectSprite;
        }
    }
}