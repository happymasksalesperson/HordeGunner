using System.Collections;
using UnityEngine;

public class DecayOverTime : MonoBehaviour
{
    [SerializeField] private float fadeTime = 5.0f; // Duration of the fade

    public Renderer[] renderers;
    public Material[] materials;

    public void Decay()
    {
        StartCoroutine(FadeOutAndDestroy());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        // Get all the materials on the object
        renderers = GetComponentsInChildren<Renderer>();
        materials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            // Create a copy of the material
            materials[i] = new Material(renderers[i].material);
            // Assign the copied material to the renderer
            renderers[i].material = materials[i];
        }

        // Initial alpha value
        float startAlpha = 1.0f;

        // Time tracking
        float elapsedTime = 0.0f;

        // Fade loop
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0.0f, elapsedTime / fadeTime);

            // Apply the new alpha value to all materials
            foreach (Material material in materials)
            {
                Color color = material.color;
                color.a = alpha;
                material.color = color;
            }

            yield return null;
        }

        // Ensure alpha is set to 0
        foreach (Material material in materials)
        {
            Color color = material.color;
            color.a = 0.0f;
            material.color = color;
        }

        // Destroy the game object
        Destroy(gameObject);
    }
}