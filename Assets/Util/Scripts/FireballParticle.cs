using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireballParticle : MonoBehaviour {

    private ParticleSystem particleSystem;

    void Awake(){
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void SetEmissionRateToZero(){
        var emission = particleSystem.emission;
        emission.rateOverTime = 0f;
    }



}
