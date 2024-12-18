using UnityEngine;
using UnityEngine.UI;

public class ButtonInteractionHandler : MonoBehaviour
{
    
    public AudioSource audioSource;
    public AudioClip buttonClickSound;

    
    void Start()
    {
        
        Button[] buttons = FindObjectsOfType<Button>();

        
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClicked(button));
        }
    }

    
    void OnButtonClicked(Button clickedButton)
    {
        
        
        
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
