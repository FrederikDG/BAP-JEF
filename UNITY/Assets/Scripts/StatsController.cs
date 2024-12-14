using UnityEngine;
using TMPro;

public class StatsController : MonoBehaviour
{
    public float speed = 5f;                  
    public float speedChangeRate = 2f;        
    private float pressure = 0f;              
    private float pressureIncreaseRate = 1f;  
    public TextMeshProUGUI speedText;         
    public TextMeshProUGUI pressureText;      

    void Start()
    {
        if (speedText == null)
        {
            Debug.LogError("Speed TextMeshPro is not assigned!");
        }
        if (pressureText == null)
        {
            Debug.LogError("Pressure TextMeshPro is not assigned!");
        }
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            speed += speedChangeRate * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            speed = Mathf.Max(0f, speed - speedChangeRate * Time.deltaTime);
        }

        
        pressure += pressureIncreaseRate * Time.deltaTime;

        
        if (Input.GetKey(KeyCode.Space))
        {
            pressure *= 0.95f;
            Debug.Log("Space pressed: Pressure reduced to " + pressure);
        }

        
        if (speedText != null)
        {
            speedText.text =speed.ToString("F0");  
        }

        if (pressureText != null)
        {
            pressureText.text = "Pressure: " + Mathf.FloorToInt(pressure).ToString() + "%";  
        }
        else
        {
            Debug.LogError("Pressure TextMeshPro is not assigned in the Inspector!");
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
}
