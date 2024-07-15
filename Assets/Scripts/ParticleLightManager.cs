using System.Collections.Generic;
using UnityEngine;


public class ParticleLightManager : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public UnityEngine.Rendering.Universal.Light2D lightPrefab;
    private ParticleSystem.Particle[] particles;
    private List<UnityEngine.Rendering.Universal.Light2D> lights = new List<UnityEngine.Rendering.Universal.Light2D>();

    void Start()
    {
        if (particleSystem == null)
        {
            particleSystem = GetComponent<ParticleSystem>();
        }

        // Initialize the particles array with a reasonable size
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
    }

    void LateUpdate()
    {
        int particleCount = particleSystem.GetParticles(particles);

        // Ensure we have enough lights for each particle
        while (lights.Count < particleCount)
        {
            UnityEngine.Rendering.Universal.Light2D light = Instantiate(lightPrefab);
            lights.Add(light);
        }

        // Update light positions and deactivate any extras
        for (int i = 0; i < lights.Count; i++)
        {
            if (i < particleCount)
            {
                lights[i].gameObject.SetActive(true);
                lights[i].transform.position = particleSystem.transform.TransformPoint(particles[i].position);
            }
            else
            {
                lights[i].gameObject.SetActive(false);
            }
        }
    }
}
