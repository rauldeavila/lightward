using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEventsOnAnimator : MonoBehaviour {
    // FMOD -------
    [FMODUnity.EventRef]
    public string AudioEvent1 = "";

    [FMODUnity.EventRef]
    public string AudioEvent2 = "";

    [FMODUnity.EventRef]
    public string AudioEvent3 = "";

    [FMODUnity.EventRef]
    public string AudioEvent4 = "";

    [FMODUnity.EventRef]
    public string AudioEvent5 = "";

    public void PlaySound1(){
        FMODUnity.RuntimeManager.PlayOneShot(AudioEvent1, transform.position);
    }

    public void PlaySound2(){
        FMODUnity.RuntimeManager.PlayOneShot(AudioEvent2, transform.position);
    }

    public void PlaySound3(){
        FMODUnity.RuntimeManager.PlayOneShot(AudioEvent3, transform.position);
    }

    public void PlaySound4(){
        FMODUnity.RuntimeManager.PlayOneShot(AudioEvent4, transform.position);
    }

    public void PlaySound5(){
        FMODUnity.RuntimeManager.PlayOneShot(AudioEvent5, transform.position);
    }

}
