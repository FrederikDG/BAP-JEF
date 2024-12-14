using UnityEngine;
using UnityEngine.Splines;

public class FishSplineFollower : MonoBehaviour
{
    public SplineContainer splineContainer; 
    public float speed = 1f; 
    private float progress = 0f; 
    public Vector3 additionalRotation = new Vector3(0, 0, 0); 

    void Update()
    {
        if (splineContainer == null) return; 

        
        progress += (speed * Time.deltaTime) / splineContainer.CalculateLength();

        
        if (progress > 1f)
            progress -= 1f;

        
        Vector3 position = splineContainer.EvaluatePosition(progress);
        transform.position = position;

        
        Vector3 tangent = splineContainer.EvaluateTangent(progress);
        if (tangent != Vector3.zero) 
        {
            Quaternion splineRotation = Quaternion.LookRotation(tangent); 
            transform.rotation = splineRotation * Quaternion.Euler(additionalRotation); 
        }
    }
} 