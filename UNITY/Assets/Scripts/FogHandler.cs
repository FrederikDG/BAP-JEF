using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class FogColorChanger : MonoBehaviour
{
    public Transform targetObject;
    public Gradient fogColorGradient;
    public float minHeight = 300f;  
    public float maxHeight = 2320f; 

    public float minFogDensity = 0.05f;  
    public float maxFogDensity = 0.3f;   

    public float minSkyboxIntensity = 0f;
    public float maxSkyboxIntensity = 1f;

    public Light directionalLight;
    public float minLightIntensity = 0.5f;
    public float maxLightIntensity = 2f;

    public Image[] colorImages;  
    public float minOpacity = 0f;
    public float maxOpacity = 1f;

    private Material skyboxMaterial;

    private void OnValidate()
    {
        if (targetObject != null)
        {
            float height = targetObject.position.y;

            
            
            float t = Mathf.InverseLerp(maxHeight, minHeight, height);

            
            Color newFogColor = fogColorGradient.Evaluate(t);
            RenderSettings.fogColor = newFogColor;

            
            float fogDensity = Mathf.Lerp(minFogDensity, maxFogDensity, t);
            RenderSettings.fogDensity = fogDensity;

            
            float skyboxIntensity = Mathf.Lerp(minSkyboxIntensity, maxSkyboxIntensity, t);
            RenderSettings.skybox.SetFloat("_Exposure", skyboxIntensity);

            
            if (directionalLight != null)
            {
                float lightIntensity = Mathf.Lerp(minLightIntensity, maxLightIntensity, t);
                directionalLight.intensity = lightIntensity;
            }

            
            if (colorImages != null)
            {
                foreach (var colorImage in colorImages)
                {
                    if (colorImage != null)
                    {
                        float opacity = Mathf.Lerp(minOpacity, maxOpacity, t);
                        Color imageColor = colorImage.color;
                        imageColor.a = opacity;
                        colorImage.color = imageColor;
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        OnValidate();
    }
}
