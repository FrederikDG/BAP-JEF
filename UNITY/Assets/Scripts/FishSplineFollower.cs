using UnityEngine;
using UnityEngine.Splines;
using System.Collections.Generic;

public class FishSplineFollower : MonoBehaviour
{
    public GameObject splineParent;  
    public GameObject fishParent;    
    public float renderDistance = 20f;  

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;  

        if (splineParent != null && fishParent != null)
        {
            SplineContainer[] splineContainers = splineParent.GetComponentsInChildren<SplineContainer>();

            if (splineContainers.Length > 0)
            {
                foreach (SplineContainer spline in splineContainers)
                {
                    
                    foreach (Transform fishTransform in fishParent.transform)
                    {
                        
                        GameObject fish = new GameObject(fishTransform.name, typeof(SpriteRenderer));
                        fish.transform.SetParent(spline.transform);

                        
                        SpriteRenderer spriteRenderer = fish.GetComponent<SpriteRenderer>();
                        spriteRenderer.sprite = fishTransform.GetComponent<SpriteRenderer>().sprite;

                        
                        FishController fishController = fish.AddComponent<FishController>();
                        fishController.currentSplineContainer = spline;
                        fishController.speed = Random.Range(1f, 5f);  

                        FishRendererVisibility fishRendererVisibility = fish.AddComponent<FishRendererVisibility>();
                        fishRendererVisibility.renderDistance = renderDistance;  
                        fishRendererVisibility.mainCamera = mainCamera;  
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("Spline parent or fish parent is missing.");
        }
    }
}


public class FishController : MonoBehaviour
{
    public SplineContainer currentSplineContainer;  
    public float speed = 1f;
    private float progress = 0f;
    public Vector3 additionalRotation = new Vector3(0, -90, 0); 

    void Update()
    {
        if (currentSplineContainer == null) return;

        progress += (speed * Time.deltaTime) / currentSplineContainer.CalculateLength();

        if (progress > 1f)
            progress -= 1f;

        Vector3 position = currentSplineContainer.EvaluatePosition(progress);
        transform.position = position;

        Vector3 tangent = currentSplineContainer.EvaluateTangent(progress);
        if (tangent != Vector3.zero)
        {
            Quaternion splineRotation = Quaternion.LookRotation(tangent);
            transform.rotation = splineRotation * Quaternion.Euler(additionalRotation);
        }
    }
}


public class FishRendererVisibility : MonoBehaviour
{
    public Camera mainCamera;
    public float renderDistance = 20f;  
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer not found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (mainCamera == null || spriteRenderer == null) return;

        float distanceToCamera = Vector3.Distance(mainCamera.transform.position, transform.position);

        if (distanceToCamera <= renderDistance)
        {
            if (!spriteRenderer.enabled)
                spriteRenderer.enabled = true;  
        }
        else
        {
            if (spriteRenderer.enabled)
                spriteRenderer.enabled = false;  
        }
    }
}
