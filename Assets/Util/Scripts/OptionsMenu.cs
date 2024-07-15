using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

    public bool mainMenu = false;
    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus Master;
    FMOD.Studio.Bus UI_SFX;

    float MusicVolume = 1f;
    float SFXVolume = 1f;
    float UISFXVolume = 1f;
    float MasterVolume = 1f;

    public Slider musicSlider;
    public Slider SFXSlider;
    public Slider MasterSlider;

    public Image musicFill;
    public Image SFXFill;
    public Image masterFill;

    [FMODUnity.EventRef]
    private string ChangeSelection = "event:/game/00_ui/ui_changeselection";

    private GameState gameState;

    private void Awake(){
        UI_SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/sfxui");
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/sfx");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        UISFXVolume = PlayerPrefs.GetFloat("UISFXVolume", 1f);
        MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicSlider.value = MusicVolume;
        SFXSlider.value = SFXVolume;
        MasterSlider.value = MasterVolume;
        gameState = FindObjectOfType<GameState>();
    }

    private void Update(){
        if(mainMenu){
            SFX.setVolume(PlayerPrefs.GetFloat("SFXVolume", 1f));
            Music.setVolume(PlayerPrefs.GetFloat("MusicVolume", 1f));
            UI_SFX.setVolume(PlayerPrefs.GetFloat("UISFXVolume", 1f));
            Master.setVolume(PlayerPrefs.GetFloat("MasterVolume", 1f));            
        } else{
            if(!gameState.Paused){
                SFX.setVolume(PlayerPrefs.GetFloat("SFXVolume", 1f));
            }
            Music.setVolume(PlayerPrefs.GetFloat("MusicVolume", 1f));
            UI_SFX.setVolume(PlayerPrefs.GetFloat("UISFXVolume", 1f));
            Master.setVolume(PlayerPrefs.GetFloat("MasterVolume", 1f));
        }
    }

    public void MusicVolumeLevel(float newMusicVolume){
        PlayerPrefs.SetFloat("MusicVolume", newMusicVolume);
        FMODUnity.RuntimeManager.PlayOneShot(ChangeSelection, transform.position);
        if(newMusicVolume == 0f){
            musicFill.color = new Color(musicFill.color.r, musicFill.color.g, musicFill.color.b, 0f);
        } else{
            musicFill.color = new Color(musicFill.color.r, musicFill.color.g, musicFill.color.b, 1f);
        }
    }

    public void SFXVolumeLevel(float newSFXVolume){
        PlayerPrefs.SetFloat("SFXVolume", newSFXVolume);
        PlayerPrefs.SetFloat("UISFXVolume", newSFXVolume);
        FMODUnity.RuntimeManager.PlayOneShot(ChangeSelection, transform.position);
        if(newSFXVolume == 0f){
            SFXFill.color = new Color(SFXFill.color.r, SFXFill.color.g, SFXFill.color.b, 0f);
        } else{
            SFXFill.color = new Color(SFXFill.color.r, SFXFill.color.g, SFXFill.color.b, 1f);
        }
    }

    public void MasterVolumeLevel(float newMasterVolume){
        PlayerPrefs.SetFloat("MasterVolume", newMasterVolume);
        FMODUnity.RuntimeManager.PlayOneShot(ChangeSelection, transform.position);
        if(newMasterVolume == 0f){
            masterFill.color = new Color(masterFill.color.r, masterFill.color.g, masterFill.color.b, 0f);
        } else{
            masterFill.color = new Color(masterFill.color.r, masterFill.color.g, masterFill.color.b, 1f);
        }
    }

    public void SetFrameRateTo30(){
        PlayerPrefs.SetInt("FrameRate", 30);
    }

    public void SetFrameRateTo60(){
        PlayerPrefs.SetInt("FrameRate", 60);
    }

    public void ExitToMenu(){
        
    }

    public void QuitGame(){
        Application.Quit();
    }

    void OnDisable(){
        EventSystem.current.SetSelectedGameObject(null);
    }

 
}
