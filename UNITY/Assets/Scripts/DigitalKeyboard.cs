using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class DigitalKeyboard : MonoBehaviour
{
    public TMP_InputField inputField;  
    public GameObject keyboardPanel;   
    private Button[] keys;             
    private int currentKeyIndex = 0;   

    private int keysPerRow = 8;       

    void Start()
    {
        
        keys = keyboardPanel.GetComponentsInChildren<Button>();

        
        EventSystem.current.SetSelectedGameObject(keys[0].gameObject);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            NavigateKeys(-keysPerRow); 
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            NavigateKeys(keysPerRow); 
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            NavigateKeys(-1); 
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NavigateKeys(1); 
        }
        else if (Input.GetKeyDown(KeyCode.Return)) 
        {
            SelectKey();
        }
    }

    void NavigateKeys(int direction)
    {
        
        currentKeyIndex += direction;

        
        if (currentKeyIndex < 0)
            currentKeyIndex = keys.Length - 1; 
        else if (currentKeyIndex >= keys.Length)
            currentKeyIndex = 0; 

        
        EventSystem.current.SetSelectedGameObject(keys[currentKeyIndex].gameObject);
    }
public void SelectKey()
{
    
    string keyText = keys[currentKeyIndex].GetComponentInChildren<TextMeshProUGUI>().text;

    
    Debug.Log("Selected Key Text: " + keyText);

    
    inputField.text += keyText;
}

    
    public void ClearInput()
    {
        inputField.text = "";
    }

    
    public void Backspace()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }
}
