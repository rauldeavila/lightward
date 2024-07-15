using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

    public static SFXManager Instance;

    private FMOD.Studio.EventInstance warning01;
    private bool warning01Flag;

    // for not loopable events:
    //private string ElevatorClank = "event:/game/00_game/elevator_clank";
    //FMODUnity.RuntimeManager.PlayOneShot(ElevatorClank, transform.position);

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
        warning01 = FMODUnity.RuntimeManager.CreateInstance("event:/game/00_game/warning01");
    }

    // void Update(){
    //     if(PlayerState.Instance.Freezing && !PlayerStats.Instance.cloak3.runTimeValue && !warning01Flag){
    //         PlayWarning01();
    //     } else if(!PlayerState.Instance.Freezing && warning01Flag){
    //         StopWarning01();
    //     } else if(PlayerStats.Instance.cloak3.runTimeValue && warning01Flag){
    //         StopWarning01();
    //     }
    // }






    public void PlayWarning01(){
        warning01Flag = true;
        warning01.start();
    }

    public void StopWarning01(){
        warning01.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        warning01Flag = false;
    }



}
