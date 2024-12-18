using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    
    public SplineFollower splineFollower;

    
    public float triggerPercentage = 0.5f;

    
    private bool triggered = false;

    void Update()
    {
        
        if (splineFollower != null)
        {
          float t = splineFollower.GetT(); 

            if (!triggered && t >= triggerPercentage)
            {
                TriggerEvent();
                triggered = true;  
            }
        }
    }

    
    private void TriggerEvent()
    {
        Debug.Log("Checkpoint reached at " + (triggerPercentage * 100f) + "%!");

        
        
    }

    
    public void ResetTrigger()
    {
        triggered = false;
    }
}
