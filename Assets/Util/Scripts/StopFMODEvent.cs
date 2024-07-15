using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopFMODEvent : MonoBehaviour {

    public bool layer1 = false;
    public bool layer2 = false;
    public bool layer3 = false;
    public bool layer4 = false;
    public bool layer5 = false;

    public bool onTriggerEnter;
    public bool onTriggerExit;
    public bool onEnable;
    public bool onDisable;

    FMOD.Studio.Bus Layer1;
    FMOD.Studio.Bus Layer2;
    FMOD.Studio.Bus Layer3;
    FMOD.Studio.Bus Layer4;
    FMOD.Studio.Bus Layer5;
    FMOD.Studio.Bus Music;

    private void Awake(){
        Layer1 = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/music/layer1");
        Layer2 = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/music/layer2");
        Layer3 = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/music/layer3");
        Layer4 = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/music/layer4");
        Layer5 = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/music/layer5");
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(onTriggerEnter){
            if(collider.CompareTag("WizHitBox")){
                StopBusMusicEvents();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider){
        if(onTriggerExit){
            if(collider.CompareTag("WizHitBox")){
                StopBusMusicEvents();
            }
        }
    }

    void OnEnable(){
        if(onEnable){
            StopBusMusicEvents();
        }
    }

    void OnDisable(){
        if(onDisable){
            StopBusMusicEvents();
        }
    }

    void StopBusMusicEvents() {
        if(layer1){
            Layer1.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        if(layer2){
            Layer2.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        if(layer3){
            Layer3.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        if(layer4){
            Layer4.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        if(layer5){
            Layer5.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

    }

}
