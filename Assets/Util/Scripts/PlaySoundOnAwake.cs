using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnAwake : MonoBehaviour {
    
    [FMODUnity.EventRef]
    public string SoundEvent = "";

    public void OnEnable(){
        FMODUnity.RuntimeManager.PlayOneShot(SoundEvent, transform.position);      
    }
}
