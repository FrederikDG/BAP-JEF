using UnityEngine;

public class WaterAnimation : MonoBehaviour
{
    public float waveSpeed = 0.5f; 
    public float waveHeight = 0.3f; 
    public float waveFrequency = 1.0f; 

    private Vector3[] originalVertices; 
    private Mesh mesh;

    void Start()
    {
        
        mesh = GetComponent<MeshFilter>().mesh;
        
        originalVertices = mesh.vertices;
    }

    void Update()
    {
        
        Vector3[] vertices = (Vector3[])originalVertices.Clone();

        
        for (int i = 0; i < vertices.Length; i++)
        {
            
            Vector3 vertex = vertices[i];
            
            vertex.y += Mathf.Sin((vertex.x + Time.time * waveSpeed) * waveFrequency) * waveHeight;
            
            vertices[i] = vertex;
        }

        
        mesh.vertices = vertices;
        mesh.RecalculateNormals(); 
    }
}
