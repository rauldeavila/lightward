using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAndStopParticles : MonoBehaviour {

    private ParticleSystem particles;
    public bool onOtherGameObject = false;
    public ParticleSystem extParticles;
    
    void Awake() {
        if (!onOtherGameObject) {
            particles = GetComponent<ParticleSystem>();
        } else {
            particles = extParticles;
        }
    }

    public void StartParticles() {
        particles.Play(true);
    }

    public void StopParticles() {
        particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

}
