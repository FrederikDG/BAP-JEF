using UnityEngine;
using UnityEngine.EventSystems;

public class GlobalButtonNavigator : MonoBehaviour
{
    [System.Serializable]
    public class ButtonGroup
    {
        public GameObject grp;  
        public GameObject button;  
    }

    public ButtonGroup[] buttonGroups;  

    private GameObject currentSelectedButton = null; 

    private void Update()
    {
        bool buttonSelected = false;

        foreach (var buttonGroup in buttonGroups)
        {
            if (buttonGroup.grp.activeSelf) 
            {
                
                if (currentSelectedButton == null || currentSelectedButton != buttonGroup.button)
                {
                    SelectButton(buttonGroup.button);  
                    buttonSelected = true;
                    break;  
                }
            }
        }

        
        if (!buttonSelected)
        {
            SelectFirstActiveGroupButton();
        }
    }

    
    private void SelectButton(GameObject button)
    {
        if (button != null)
        {
            
            EventSystem.current.SetSelectedGameObject(null);
            
            
            EventSystem.current.SetSelectedGameObject(button);
            currentSelectedButton = button;
        }
    }

    
    private void SelectFirstActiveGroupButton()
    {
        foreach (var buttonGroup in buttonGroups)
        {
            if (buttonGroup.grp.activeSelf)  
            {
                
                if (currentSelectedButton == null)
                {
                    SelectButton(buttonGroup.button);  
                }
                break;  
            }
        }
    }
}
