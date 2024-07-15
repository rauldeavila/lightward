using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GateTrigger : MonoBehaviour {


    public bool oneGate;
    public bool twoGates;
    public bool threeGates;
    public bool fourGates;
    public bool triggersOnlyScriptableObjectIsFalse;
    public bool stopMusicBusWhenClosingGate;
    public BoolValue scriptableObjectToCheck;

    public Animator gate1Animator;
    [ShowIf("twoGates")]
    public Animator gate2Animator;
    [ShowIf("threeGates")]
    public Animator gate3Animator;
    [ShowIf("fourGates")]
    public Animator gate4Animator;

    public bool gatesLocked = false;
    FMOD.Studio.Bus Music;

    private void Awake(){
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/music");
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            if(triggersOnlyScriptableObjectIsFalse && scriptableObjectToCheck.initialValue == false){ // false = not killed enemy, so triggers it
                if(gatesLocked == false){
                    gatesLocked = true;
                    LockGates();
                }
            } else if(triggersOnlyScriptableObjectIsFalse == false){
                if(gatesLocked == false){
                    gatesLocked = true;
                    LockGates();
                }
            }
        }
    }

    public void LockGates(){
        if(stopMusicBusWhenClosingGate){
            StopMusicBus();
        }
        if(gate1Animator != null){
            gate1Animator.SetTrigger("lock");
        }
        if(gate2Animator != null){
            gate2Animator.SetTrigger("lock");
        }
        if(gate3Animator != null){
            gate3Animator.SetTrigger("lock");
        }
        if(gate4Animator != null){
            gate4Animator.SetTrigger("lock");
        }
    }

    public void UnlockGates(){
        if(gate1Animator != null){
            gate1Animator.SetTrigger("unlock");
        }
        if(gate2Animator != null){
            gate2Animator.SetTrigger("unlock");
        }
        if(gate3Animator != null){
            gate3Animator.SetTrigger("unlock");
        }
        if(gate4Animator != null){
            gate4Animator.SetTrigger("unlock");
        }
    }

    private void StopMusicBus(){
        Music.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }


}
