using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTrigger : MonoBehaviour {

    private bool onceFlag = false;

    private Animator animator;

    public bool enableGameObjectOnPickup;
    public GameObject objectToEnable;
    public bool disableGameObjectOnPickup;
    public GameObject objectToDisable;

    public bool isSpell;
    public bool isFireball;
    public bool isDashingSoul;
    public bool isDashingLight;


    public bool isHeartContainer;
    public BoolValue heart_container_scriptable;
    public GameObject thisGameObject;

    public bool isKey;
    public BoolValue key_scriptable_event;

    public bool isBottle;
    public BoolValue bottle_scriptable;  // 4 different scriptables

    public bool isLockedDoor;
    public BoolValue locked_door_scriptable_event;


    public bool isBoatRide;
    public GameObject boatMenu;

    private PlayerStats playerStats;
    private PlayerController controller;

    private bool _flagForSpellsTimeout = false;

    private void Awake(){
        controller = FindObjectOfType<PlayerController>();
        playerStats = FindObjectOfType<PlayerStats>();
        animator = GetComponent<Animator>();
        if(isHeartContainer){
            if(heart_container_scriptable.runTimeValue == true){
                gameObject.SetActive(false);
            }
        }
    }

    public void Update(){
        if(!_flagForSpellsTimeout){
            if(InteractionsManager.Instance.Dialogue == false && onceFlag){
                _flagForSpellsTimeout = true;
                Invoke("SetSpellToTrue", 1.5f);
            }
        }
    }

    private void SetSpellToTrue(){
        if(isFireball){
            ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_fireball", true);
        } else if(isDashingSoul){
            ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_dashing_soul", true);
        } else if(isDashingLight){
            ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_dashing_light", true);
        }
    }

    public void TriggerInteraction(){
        if(!onceFlag){

            onceFlag = true;

            if(enableGameObjectOnPickup){
                objectToEnable.SetActive(true);
            }

            if(disableGameObjectOnPickup){
                objectToDisable.SetActive(false);
            }

            if(isSpell){

                animator.SetTrigger("pickup");
                controller.Animator.Play("new_spell");
                // GetComponentInParent<EnableBasedOnLanguage>().en = null;
                // GetComponentInParent<EnableBasedOnLanguage>().ptbr = null;

            } else if(isHeartContainer){
                controller.Animator.Play("acquiring_health_container");
                PlayerState.Instance.SetAllStatesToFalse();
                PlayerState.Instance.Interacting = true;
                InteractionsManager.Instance.Animation = true;
                heart_container_scriptable.runTimeValue = true;
                animator.SetTrigger("pickup");
            } else if(isKey){
                    key_scriptable_event.initialValue = true;
                    playerStats.keys.initialValue += 1;
                    animator.SetTrigger("pickup");
            }  else if(isBottle){
                    bottle_scriptable.initialValue = true;
                    animator.SetTrigger("pickup");
            } else if(isLockedDoor){
                    // call on animator!
                    // locked_door_scriptable_event.initialValue = true;
                    playerStats.keys.runTimeValue -= 1;
                    animator.SetTrigger("unlock");
            } else if(isBoatRide){
                boatMenu.SetActive(true);
            }
        } 
    }

    public void SetLockedDoorScriptableToTrue(){
        locked_door_scriptable_event.initialValue = true;
    }


}
