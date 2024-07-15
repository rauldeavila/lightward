using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoreMountains.Feedbacks;

public class PlayerCombat : MonoBehaviour {


    public bool AttackCoolDown = false;

    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public int AttackPowerValue; 
    private int meleePowerValue = 1;
    

    [HideInInspector]
    public bool attackPressed = false;

    [FMODUnity.EventRef]
    public string AttackEvent = "";

    public static PlayerCombat Instance;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }

    private void FixedUpdate(){
        HandleMeleeAttack();
    }

    private void TurnCooldownOff(){
        AttackCoolDown = false;
    }

    public void AttackPressed(){
        if(StateController.Instance.CanMove){
            AttackPowerValue = meleePowerValue;
            attackPressed = true;
            return;
        }
    }

    private void Attack(){
        PlayerState.Instance.Attack = true;
        if(Inputs.Instance.HoldingUpArrow){
            PlayerController.Instance.Animator.Play("attack_up");
            Invoke("ResetAttackState", 0.3f);
            return;
        }
        if(!PlayerState.Instance.Grounded && Inputs.Instance.HoldingDownArrow) {
            PlayerController.Instance.Animator.Play("attack_down");
            Invoke("ResetAttackState", 0.3f);
            return;
        }
        int attack = Random.Range(1, 3);
        if(attack == 1){
            PlayerController.Instance.Animator.Play("attack");
        } else if(attack == 2){
            PlayerController.Instance.Animator.Play("attack2");
        }
        Invoke("ResetAttackState", 0.3f);
    }

    private void ResetAttackState(){
        PlayerState.Instance.Attack = false;
    }

    private void HandleMeleeAttack(){
        if(StateController.Instance.CanMove){
            if(timeBtwAttack <=0 && attackPressed){
                attackPressed = false;
                timeBtwAttack = startTimeBtwAttack;
                Attack();
            } else {
                attackPressed = false;
                timeBtwAttack -= Time.deltaTime;
            }

        } else{
            attackPressed = false;
        }        
    }

    public void PlayAttackSound(){
        FMODUnity.RuntimeManager.PlayOneShot(AttackEvent, transform.position);
    }

    public void EnterAttackCooldownForSeconds(float seconds){
        AttackCoolDown = true;
        Invoke("TurnCooldownOff", seconds);
    }

}
