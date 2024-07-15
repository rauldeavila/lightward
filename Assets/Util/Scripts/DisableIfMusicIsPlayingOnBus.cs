using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfMusicIsPlayingOnBus : MonoBehaviour {
    
    public GameObject thisGameObject;

    [SerializeField]
    private FMODUnity.StudioEventEmitter emitter;   

    private void Awake() {
        if (emitter.IsPlaying()) {
            thisGameObject.SetActive(false);
        } 
    }

}
