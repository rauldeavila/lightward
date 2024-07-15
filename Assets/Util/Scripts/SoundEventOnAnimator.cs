using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEventOnAnimator : MonoBehaviour {


    // FMOD -------
    [FMODUnity.EventRef]
    public string AudioEvent = "";

    public void PlaySound(){
        FMODUnity.RuntimeManager.PlayOneShot(AudioEvent, transform.position);
    }

}
