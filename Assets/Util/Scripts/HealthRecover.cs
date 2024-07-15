using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthRecover : MonoBehaviour {

    // public int cost_heartrecover = 25;

    // public GameObject gainingHeartPrefab;
    // private FMOD.Studio.EventInstance SFXhealthRecoverAmbienceInstance;
    // private FMOD.Studio.EventInstance SFXhealthRecoverHPUP;

    // public static HealthRecover Instance;

    // void Awake(){

    //     if (Instance != null && Instance != this){ 
    //         Destroy(this); 
    //     } else { 
    //         Instance = this; 
    //     } 

    //     SFXhealthRecoverAmbienceInstance = FMODUnity.RuntimeManager.CreateInstance("event:/char/wiz/health_recover_ambience");
    //     SFXhealthRecoverHPUP = FMODUnity.RuntimeManager.CreateInstance("event:/char/wiz/health_recover_hpup");
    // }

    // void FixedUpdate(){
    //     if(!GameState.Instance.Paused && !GameState.Instance.InventoryOpened && !GameState.Instance.MapOpened && Inputs.Instance.HoldingHealthRecover && PlayerState.Instance.CanUseHealthRecover(cost_heartrecover)){
    //         if(!PlayerController.Instance.AnimatorIsPlaying("health_recover") && PlayerController.Instance.State.Grounded){
    //             if(!((PlayerStats.Instance.GetCurrentHealth() + 1) > PlayerStats.Instance.GetMaxHealth())){
    //                 PlayerController.Instance.Animator.Play("health_recover");
    //                 SFXhealthRecoverAmbienceInstance.start();
    //             } else{
    //                 PlayerController.Instance.Animator.Play("idle");
    //                 SFXhealthRecoverAmbienceInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    //             }
    //         }
    //     } else{
    //         if(PlayerController.Instance.AnimatorIsPlaying("health_recover")){
    //             PlayerController.Instance.Animator.Play("idle");
    //             SFXhealthRecoverAmbienceInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    //         }
    //     }
    // }

    // // Recover calls on animator!
    // public void RecoverHealth(){
    //     PlayerStats.Instance.DecreaseMagic(cost_heartrecover);
    //     PlayerStats.Instance.IncreaseHealth(1);
    //     CameraController.Instance.SoftShake();
    //     SFXhealthRecoverHPUP.start();
    //     Instantiate(gainingHeartPrefab, this.transform.position , Quaternion.identity);
    //     CheckIfCanGainAnotherHeart();
    // }

    // private void CheckIfCanGainAnotherHeart(){
    //     if((PlayerStats.Instance.GetCurrentHealth() + 1) > PlayerStats.Instance.GetMaxHealth()){
    //         Inputs.Instance.HoldingHealthRecover = false;
    //         PlayerController.Instance.Animator.Play("idle");
    //         SFXhealthRecoverAmbienceInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    //     }
    // }

}
