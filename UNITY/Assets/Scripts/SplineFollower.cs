using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.SceneManagement;  

[ExecuteInEditMode]
public class SplineFollower : MonoBehaviour
{
    public SplineContainer splineContainer;
    private float t = 0f;
    public float startPercentage = 0f;
    public StatsController statsController;
    public float speedFactor = 1f;

    public float GetT()
    {
        return t;
    }

    void Start()
    {
        InitializeT();
    }

    void Update()
    {
        if (Application.isPlaying)
        {
            if (statsController != null)
            {
                float speed = statsController.GetSpeed();

                t += speed * Time.deltaTime * speedFactor;
                t = Mathf.Repeat(t, 1f);  

                UpdatePositionAndRotation();

                
                if (t >= 0.99f)
                {
                    LoadNextScene();
                }
            }
            else
            {
                Debug.LogError("StatsController is not assigned to SplineFollower!");
            }
        }
        else
        {
            InitializeT();
            UpdatePositionAndRotation();
        }
    }

    void OnValidate()
    {
        InitializeT();
        UpdatePositionAndRotation();
    }

    private void InitializeT()
    {
        t = Mathf.Clamp01(startPercentage / 100f);
    }

    private void UpdatePositionAndRotation()
    {
        if (splineContainer != null)
        {
            transform.position = splineContainer.EvaluatePosition(t);
            Vector3 tangent = splineContainer.EvaluateTangent(t);
            transform.rotation = Quaternion.LookRotation(tangent);
        }
        else
        {
            Debug.LogError("SplineContainer is not assigned!");
        }
    }

private void LoadNextScene()
{
    
    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    Debug.Log("Loading scene at index: " + nextSceneIndex);
    SceneManager.LoadScene(nextSceneIndex);
}}
