using UnityEngine;
using UnityEngine.UI;

public class MuteToggle : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite muteSprite;      
    public Sprite unmuteSprite;     
    
    private Image buttonImage;      
    private bool isMuted = false;   

    void Start()
    {
        
        buttonImage = GetComponent<Image>(); 
        UpdateButtonSprite();
    }

    
    public void ToggleMute()
    {
        
        isMuted = !isMuted;
        
        
        UpdateButtonSprite();
        
        // Audio system goes hier
    }

    private void UpdateButtonSprite()
    {
        if (isMuted)
        {
            buttonImage.sprite = muteSprite;
        }
        else
        {
            buttonImage.sprite = unmuteSprite;
        }
    }
}