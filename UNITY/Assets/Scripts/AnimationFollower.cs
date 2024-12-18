using UnityEngine;
using UnityEngine.Splines;

public class AnimationSplineFollower : MonoBehaviour
{
    public GameObject splineParent;  
    private SplineContainer[] splineContainers;  
    public Sprite[] fishTextures;  
    public float renderDistance = 20f;  

    public float minSpeed = 1f;  
    public float maxSpeed = 5f;  

    public bool shouldOrient = true;  
    public Vector3 additionalRotation = new Vector3(0, -90, 0);  

    private Camera mainCamera;

    [Range(0f, 1f)]
    public float splineUsagePercentage = 1f;  

    [Range(0.1f, 11f)]
    public float fishScale = 10f;  

    public RuntimeAnimatorController fishAnimatorController;  

    void Start()
    {
        mainCamera = Camera.main;  

        if (splineParent != null && fishTextures.Length > 0)
        {
            splineContainers = splineParent.GetComponentsInChildren<SplineContainer>();

            if (splineContainers.Length > 0)
            {
                
                int splineCountToUse = Mathf.CeilToInt(splineContainers.Length * splineUsagePercentage);

                for (int i = 0; i < splineCountToUse; i++)
                {
                    SplineContainer spline = splineContainers[i];

                    GameObject fish = new GameObject("Fish", typeof(SpriteRenderer), typeof(Animator));
                    fish.transform.SetParent(spline.transform);  
                    SpriteRenderer spriteRenderer = fish.GetComponent<SpriteRenderer>();

                    if (spriteRenderer != null && fishTextures.Length > 0)
                    {
                        spriteRenderer.sprite = fishTextures[Random.Range(0, fishTextures.Length)];
                    }

                    
                    fish.transform.localScale = Vector3.one * fishScale;

                    
                    Animator animator = fish.GetComponent<Animator>();
                    if (animator != null && fishAnimatorController != null)
                    {
                        animator.runtimeAnimatorController = fishAnimatorController;
                    }

                    AnimationFishController fishController = fish.AddComponent<AnimationFishController>();
                    fishController.currentSplineContainer = spline;
                    fishController.speed = Random.Range(minSpeed, maxSpeed);  
                    fishController.shouldOrient = shouldOrient;  
                    fishController.additionalRotation = additionalRotation;  

                    AnimationFishRendererVisibility fishRendererVisibility = fish.AddComponent<AnimationFishRendererVisibility>();
                    fishRendererVisibility.renderDistance = renderDistance;  
                    fishRendererVisibility.mainCamera = mainCamera;  
                }
            }
        }
    }
}

public class AnimationFishController : MonoBehaviour
{
    public SplineContainer currentSplineContainer;  
    public float speed = 1f;
    private float progress = 0f;
    public bool shouldOrient = true;  
    public Vector3 additionalRotation = new Vector3(0, -90, 0);  

    void Update()
    {
        if (currentSplineContainer == null) return;

        progress += (speed * Time.deltaTime) / currentSplineContainer.CalculateLength();

        if (progress > 1f)
            progress -= 1f;

        Vector3 position = currentSplineContainer.EvaluatePosition(progress);
        transform.position = position;

        if (shouldOrient)
        {
            Vector3 tangent = currentSplineContainer.EvaluateTangent(progress);
            if (tangent != Vector3.zero)
            {
                Quaternion splineRotation = Quaternion.LookRotation(tangent);
                transform.rotation = splineRotation * Quaternion.Euler(additionalRotation);
            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(additionalRotation);
        }
    }
}

public class AnimationFishRendererVisibility : MonoBehaviour
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

        if (distanceToCamera > renderDistance)
        {
            
            spriteRenderer.enabled = false;
        }
        else
        {
            
            spriteRenderer.enabled = true;
        }
    }
}
