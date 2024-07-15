using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticlesHandler : MonoBehaviour {

    private ParticleSystem particleSystem;

    void Awake(){
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update(){
        // time.particleSystem.playbackSpeed = Time.timeScale;
    }

}
