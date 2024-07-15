using UnityEngine;

public class ColorMask : MonoBehaviour
{
    public Shader colorMaskShader; // Reference to the custom shader
    public Color maskColor = Color.red; // Color to use for the mask
    public float radius = 1.0f; // Radius of the circle collider

    private Material material; // Material using the custom shader

    private void Start()
    {
        // Create a new material with the custom shader
        material = new Material(colorMaskShader);

        // Set the mask color property of the shader
        material.SetColor("_MaskColor", maskColor);

        // Set the circle position and radius properties of the shader
        material.SetVector("_CirclePosition", transform.position);
        material.SetFloat("_CircleRadius", radius);

        // Apply the material to the renderer
        GetComponent<Renderer>().material = material;
    }

    private void Update()
    {
        // Update the circle position property of the shader in case the object moves
        material.SetVector("_CirclePosition", transform.position);
    }

    private void OnDestroy()
    {
        // Clean up the material when the object is destroyed
        if (material != null)
        {
            Destroy(material);
        }
    }
}
