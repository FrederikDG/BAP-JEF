using System.IO.Ports;
using UnityEngine;
using Newtonsoft.Json.Linq; 

public class ArduinoDebugLogger : MonoBehaviour
{
    private SerialPort serialPort;
    public string portName = "COM5";  
    public int baudRate = 9600;

    void Start()
    {
        try
        {
            serialPort = new SerialPort(portName, baudRate)
            {
                ReadTimeout = 10,
                DtrEnable = true,
            };
            serialPort.Open();
            Debug.Log($"Connected to {portName} at {baudRate} baud.");
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"Failed to open serial port {portName}: {e.Message}");
        }
    }

    void Update()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                if (serialPort.BytesToRead > 0)
                {
                    string message = serialPort.ReadLine();
                    Debug.Log($"Arduino: {message}");
                    HandleArduinoInput(message);
                }
            }
            catch (System.TimeoutException)
            {
                Debug.Log("Timeout: No data received this frame.");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Serial read error: {e.Message}");
            }
        }
    }

    
    private void HandleArduinoInput(string message)
    {
        try
        {
            
            JObject data = JObject.Parse(message);

            string buttonState = data["button"]?.ToString();

            
            if (buttonState == "pressed")
            {
                SimulateKeyPress(KeyCode.Return); 
            }

            
            JObject joystick = data["joystick"] as JObject;
            if (joystick != null)
            {
                string leftState = joystick["left"]?.ToString();
                string rightState = joystick["right"]?.ToString();
                string upState = joystick["up"]?.ToString();
                string downState = joystick["down"]?.ToString();

                
                if (leftState == "pressed")
                {
                    SimulateKeyPress(KeyCode.LeftArrow); 
                }
                if (rightState == "pressed")
                {
                    SimulateKeyPress(KeyCode.RightArrow); 
                }
                if (upState == "pressed")
                {
                    SimulateKeyPress(KeyCode.UpArrow); 
                }
                if (downState == "pressed")
                {
                    SimulateKeyPress(KeyCode.DownArrow); 
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to parse JSON: {e.Message}");
        }
    }

    
    private void SimulateKeyPress(KeyCode key)
    {
        Debug.Log($"Simulated key press: {key}");
        
    }

    void OnDestroy()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
