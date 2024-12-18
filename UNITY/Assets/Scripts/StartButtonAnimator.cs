using UnityEngine;
using UnityEngine.UI;  
using System.Collections;  

public class StartButtonAnimator : MonoBehaviour
{
    public Button startButton; 
    private Image startButtonImage; 

    private Color selectedColor = new Color(255f / 255f, 255f / 255f, 255f / 255f, 1f); 
    private Color unselectedColor = new Color(91f / 255f, 91f / 255f, 91f / 255f, 0.7f); 

    public float blinkDuration = 3f; 

    public GameObject grpSelectionMenu; 

    void Start()
    {
        if (startButton == null)
        {
            Debug.LogError("Start button is not assigned in the Inspector!");
            return; 
        }

        
        startButtonImage = startButton.GetComponent<Image>();

        
        StartCoroutine(BlinkButtonOpacity(startButtonImage, selectedColor, unselectedColor, blinkDuration));

        
        HideGrpSelectionMenu();
    }

    
    private IEnumerator BlinkButtonOpacity(Image buttonImage, Color color1, Color color2, float duration)
    {
        while (true) 
        {
            
            yield return StartCoroutine(ChangeButtonOpacitySine(buttonImage, color1, color2, duration));

            
            yield return StartCoroutine(ChangeButtonOpacitySine(buttonImage, color2, color1, duration));
        }
    }

    
    private IEnumerator ChangeButtonOpacitySine(Image buttonImage, Color startColor, Color endColor, float duration)
    {
        float timeElapsed = 0f;

        
        buttonImage.color = startColor;

        while (timeElapsed < duration)
        {
            
            float sineFactor = (Mathf.Sin((timeElapsed / duration) * Mathf.PI - Mathf.PI / 2) + 1f) / 2f;
            buttonImage.color = Color.Lerp(startColor, endColor, sineFactor);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        
        buttonImage.color = endColor;
    }

    
    private void HideGrpSelectionMenu()
    {
        if (grpSelectionMenu != null && grpSelectionMenu.activeSelf)
        {
            grpSelectionMenu.SetActive(false); 
        }
    }
}
