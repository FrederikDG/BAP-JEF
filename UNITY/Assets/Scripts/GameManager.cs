using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float totalSpeed;
    public float totalResistance;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}