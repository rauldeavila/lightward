using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnParticleCollision : MonoBehaviour {

    // FMOD -------
    [FMODUnity.EventRef]
    public string AudioEvent = "";

    public void PlaySound(){
        FMODUnity.RuntimeManager.PlayOneShot(AudioEvent, transform.position);
    }

    private void OnParticleCollision(GameObject other) {
        PlaySound();
    }


}
