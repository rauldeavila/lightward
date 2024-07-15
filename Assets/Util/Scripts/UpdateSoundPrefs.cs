using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSoundPrefs : MonoBehaviour {

    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus Master;

    float MusicVolume = 1f;
    float SFXVolume = 1f;
    float MasterVolume = 1f;


    private void Awake(){
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/sfx");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
    }

    private void Update(){
        Music.setVolume(PlayerPrefs.GetFloat("MusicVolume", 1f));
        SFX.setVolume(PlayerPrefs.GetFloat("SFXVolume", 1f));
        Master.setVolume(PlayerPrefs.GetFloat("MasterVolume", 1f));
    }

}
