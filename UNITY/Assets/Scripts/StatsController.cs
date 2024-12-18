using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsController : MonoBehaviour
{
    public float speed = 5f;
    private float pressure = 0f;
    private float pressureIncreaseRate = 1f;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI pressureText;
    public TextMeshProUGUI yPositionText;

    public Transform trackedObject;      
    public Transform loweredObject;     
    public Transform raisedObject;      
    public RectTransform pressureBar;   

    public float Ycorrection;           

    public float maxDepth = 2000f;      
    public float maxLowering = 500f;    
    public float maxRaising = 500f;     
    public float maxPressure = 100f;    
    public float maxPressureBarWidth = 1065f; 

    public Gradient pressureColorRamp;  

    private float currentDepth = 0f;   
    void Update()
    {
        
        currentDepth += speed * Time.deltaTime;
        currentDepth = Mathf.Clamp(currentDepth, 0f, maxDepth);

        
        pressure += pressureIncreaseRate * Time.deltaTime;
        pressure = Mathf.Clamp(pressure, 0f, maxPressure);

        
        if (speedText != null)
        {
            speedText.text = speed.ToString("F0");
        }

        
        if (pressureText != null)
        {
            pressureText.text = Mathf.FloorToInt(pressure).ToString() + "%";
        }

        
        if (yPositionText != null && trackedObject != null)
        {
            float yPos = trackedObject.position.y + Ycorrection;
            yPositionText.text = yPos.ToString("F0"); 
        }

        
        if (loweredObject != null)
        {
            float lowering = MapRange(currentDepth + Ycorrection, 0f, maxDepth, 0f, maxLowering);
            Vector3 newPosition = loweredObject.position;
            newPosition.y = -lowering; 
            loweredObject.position = newPosition;
        }

        
        if (raisedObject != null)
        {
            float raising = MapRange(pressure, 0f, maxPressure, 0f, maxRaising);
            Vector3 newPosition = raisedObject.position;
            newPosition.y = raising; 
            raisedObject.position = newPosition;
        }

        
        if (pressureBar != null)
        {
            float barWidth = MapRange(pressure, 0f, maxPressure, 0f, maxPressureBarWidth);
            Vector2 newSize = pressureBar.sizeDelta;
            newSize.x = barWidth; 
            pressureBar.sizeDelta = newSize;

            
            RawImage pressureBarImage = pressureBar.GetComponent<RawImage>();
            if (pressureBarImage != null)
            {
                Color pressureColor = pressureColorRamp.Evaluate(pressure / maxPressure);
                pressureBarImage.color = pressureColor;  
            }
            else
            {
                Debug.LogError("Pressure bar does not have a RawImage component.");
            }
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetPressure()
    {
        return pressure;
    }

    
    private float MapRange(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) / (inMax - inMin) * (outMax - outMin) + outMin;
    }
}
