using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionScreenNavigator : MonoBehaviour
{
    
    public GameObject selectionMenu;

    
    private bool isMenuActive = false;

    void Update()
    {
        
        if (selectionMenu.activeSelf && !isMenuActive)
        {
            
            SelectFirstButton();
            isMenuActive = true;
        }
        else if (!selectionMenu.activeSelf && isMenuActive)
        {
            
            isMenuActive = false;
        }
    }

    
    private void SelectFirstButton()
    {
        
        GameObject firstButton = GameObject.Find("btn_1");  
        if (firstButton != null)
        {
            EventSystem.current.SetSelectedGameObject(firstButton);
        }
    }
}
