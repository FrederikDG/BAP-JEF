using System.IO.Ports;
using UnityEngine;

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
            Debug.Log("Checking for data...");
            if (serialPort.BytesToRead > 0)
            {
                string message = serialPort.ReadLine();
                Debug.Log($"Arduino: {message}");
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



    void OnDestroy()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
