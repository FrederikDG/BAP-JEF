using UnityEngine;
using UnityEngine.Splines;

[ExecuteInEditMode]
public class SplineFollower : MonoBehaviour
{
    public SplineContainer splineContainer;
    private float t = 0f;
    public float startPercentage = 0f;
    public StatsController statsController; 

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
        if (statsController != null)
        {
            float speed = statsController.GetSpeed(); 
            
            t = Mathf.Repeat(t, 1f); 
            UpdatePositionAndRotation();
        }
        else
        {
            Debug.LogError("StatsController is not assigned to SplineFollower!");
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
}
