using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DashingSoul : MonoBehaviour {

    public float speed = 30f;

    private bool pressedDashingSoul = false;
    private bool holdingDashingSoul = false;
    private float holdingDuration = 0.3f;
    private bool _dashingSoulPrepping = false;

    private FMOD.Studio.EventInstance preppingInstance;
    private FMOD.Studio.EventInstance flyingInstance;

    void Awake(){
        preppingInstance = FMODUnity.RuntimeManager.CreateInstance("event:/char/wiz/dashing_soul_prepping");
    }


    void FixedUpdate(){

        if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("hero_dashing_soul").runTimeValue == false){
            return;
        }
        // AQUI QUE OCORRE O CHECK DO BOTAO
        if(Inputs.Instance.HoldingDashingSoul && StateController.Instance.CanDashSoul){
            PlayerController.Instance.SetVelocityToZero();
            _dashingSoulPrepping = true;
            PlayerState.Instance.DashingSoul = true;
        }
        // se a animacao tiver rodando, seta o estado e fica checando se soltou o botao, se soltou, para a animacao
        if(_dashingSoulPrepping){
            PlayerController.Instance.StopWizWithoutChangingAnimation();
            PlayerController.Instance.Animator.ResetTrigger("stopDashingSoul");
            if(!PlayerController.Instance.AnimatorIsPlaying("dashingsoul_begin") && !PlayerController.Instance.AnimatorIsPlaying("dashingsoul_dashing")  && !PlayerController.Instance.AnimatorIsPlaying("dashingsoul_stopping")){
                PlayerController.Instance.Animator.Play("dashingsoul_begin");
                preppingInstance.start();
            }

            if(PlayerController.Instance.AnimatorIsPlaying("dashingsoul_begin")){
                if(!Inputs.Instance.HoldingDashingSoul){
                    _dashingSoulPrepping = false;
                    PlayerState.Instance.DashingSoul = false;
                    PlayerController.Instance.Animator.SetTrigger("releasedDashingSoul");
                    //stop prepping
                    preppingInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                }
            }

            if(PlayerController.Instance.AnimatorIsPlaying("dashingsoul_dashing")){
                PlayerState.Instance.DashingSoul = true;
                _dashingSoulPrepping = false;
            }
        }


        if(PlayerState.Instance.DashingSoul){ // seta no behavior
            if(PlayerController.Instance.AnimatorIsPlaying("dashingsoul_dashing")){
                UseDashingSoul();
            }
            if(Inputs.Instance.HoldingJump || GroundController.Instance.dashingSoulWall || PlayerController.Instance.AnimatorIsPlaying("hit")){
                StopDashingSoul();
            }
        }
        
    }

    public void UseDashingSoul(){ 
        PlayerController.Instance.DashingSoul(speed);
    }

    public void StopDashingSoul(){ // called on Jump.cs
        PlayerController.Instance.Animator.SetTrigger("stopDashingSoul");
        preppingInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        PlayerController.Instance.SetVelocityToZero();
    }

    // HOLDING CONTROLLERS ----------------

    void DashingSoulPressed(){
        if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("wiz_dashing_soul").runTimeValue == true){
            pressedDashingSoul = true;
            Invoke("ResetDashingSoulPressedState", 0.1f);
            Invoke("SetDashingSoulToHoldingState", holdingDuration);
        }
    }

    void ResetDashingSoulPressedState(){
        pressedDashingSoul = false;
    }

    void SetDashingSoulToHoldingState(){
        holdingDashingSoul = true;
    }

    void DashingSoulReleased(){
        CancelInvoke();
        holdingDashingSoul = false;
    }



}
