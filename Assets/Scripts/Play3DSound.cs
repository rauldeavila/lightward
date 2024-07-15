using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Play3DSound : MonoBehaviour {
    
    StudioEventEmitter _soundEmitter;

    void Start(){
        _soundEmitter = GetComponent<StudioEventEmitter>();
    }
    public void Play(){
        _soundEmitter.Play();
    }
}
