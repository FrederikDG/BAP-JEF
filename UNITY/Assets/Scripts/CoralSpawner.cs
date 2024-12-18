using UnityEngine;

public class CoralScatter : MonoBehaviour
{
    
    public GameObject coralPrefab; 
    public Terrain terrain; 
    public int numberOfCorals = 2000; 
    public float scatterRadius = 50f; 

    void Start()
    {
        ScatterCorals();
    }

    void ScatterCorals()
    {
        for (int i = 0; i < numberOfCorals; i++)
        {
            
            float xPos = transform.position.x + Random.Range(-scatterRadius, scatterRadius);
            float zPos = transform.position.z + Random.Range(-scatterRadius, scatterRadius);
            
            
            float yPos = terrain.SampleHeight(new Vector3(xPos, 0, zPos)) + terrain.transform.position.y;

            
            Vector3 coralPosition = new Vector3(xPos, yPos, zPos);
            Instantiate(coralPrefab, coralPosition, Quaternion.identity);
        }
    }
}
