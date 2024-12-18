using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestarter : MonoBehaviour
{
    
    public float restartDelay = 30f;

    private float idleTime = 0f;

    private void Update()
    {
        
        if (Input.anyKey || Input.mousePosition != new Vector3(Screen.width / 2, Screen.height / 2, 0))
        {
            idleTime = 0f; 
        }
        else
        {
            idleTime += Time.deltaTime; 
        }

        
        if (idleTime >= restartDelay)
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        
        SceneManager.LoadScene(0);
    }
}
