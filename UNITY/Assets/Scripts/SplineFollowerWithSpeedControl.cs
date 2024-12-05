using UnityEngine;
using TMPro;
using UnityEngine.Splines;

public class SplineFollowerWithSpeedControl : MonoBehaviour
{
    public SplineContainer splineContainer;  
    public float speed = 5f;                 
    public float speedChangeRate = 2f;       
    private float t = 0f;                    
    public TextMeshProUGUI speedText;        
    public TextMeshProUGUI pressureText;     

    private float pressure = 0f;             
    private float pressureIncreaseRate = 1f;

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
            Debug.Log("Pressed Up Arrow: Speed increased to " + speed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            speed = Mathf.Max(0f, speed - speedChangeRate * Time.deltaTime);
            Debug.Log("Pressed Down Arrow: Speed decreased to " + speed);
        }

        
        if (splineContainer != null)
        {
            t += (speed * Time.deltaTime) / splineContainer.CalculateLength(); 
            if (t > 1f) t = 0f; 

            
            pressure += pressureIncreaseRate * Time.deltaTime;

            
            if (Input.GetKey(KeyCode.Space))
            {
                pressure *= 0.95f;
                Debug.Log("Space pressed: Pressure reduced to " + pressure);
            }

            
            transform.position = splineContainer.EvaluatePosition(t);
            Vector3 tangent = splineContainer.EvaluateTangent(t);
            transform.rotation = Quaternion.LookRotation(tangent);
        }

        
        if (speedText != null)
        {
            speedText.text = "Speed: " + speed.ToString("F2") + " km/h";  
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
}
