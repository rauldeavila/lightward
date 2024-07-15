using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(DialogueTrigger))]
public class ShowAlert : MonoBehaviour {

    public UnityEvent SaveTriggerEvent; 
    private AlertsPanelController alertsUI;
    public BoxCollider2D boxcolliderLeft;
    public BoxCollider2D boxcolliderRight;
    private PlayerController wiz;
    private CurrentDialogueControllerForTextAnimator curDialogue;
    private PlayerCombat combat;
    private Move move;
    private GameState gameState;

    private bool flag = true;

    public GameObject alertObject;

    public float delayToShow = 0f;

    // Tutorial only
    public bool jumpAlert;
    public bool attackAlert;
    public bool wakeUpAlert;
    public bool climbAlert;

    public bool enterAlert;
    public bool talkAlert;
    public bool saveAlert;
    public bool interactAlert;
    public bool unlockAlert;
    public bool heartContainer;
    public bool spell;
    public GameObject SpellGameObject;
    public bool bottle;
    public bool key;
    private Animator animator;
    // ---


    public bool doSomethingWhenDialogueEnded = false;
    [ShowIf("doSomethingWhenDialogueEnded")]
    public bool startNewScene = false;
    // add any other useful 'do's here in the future =)


    private bool dialogueStarted = false;
    // ----

    public bool whiteOutline = false;
    [ShowIf("whiteOutline")]
    public GameObject objectToTurnOutlineOn;

    public bool insideTrigger = false;
    public bool insideTalkTrigger = false;
	public bool insideSaveTrigger = false;
    public bool insideInteractTrigger = false;
    public bool insideEnterTrigger = false;
    public bool insideUnlockTrigger = false;

    private PlayerStats playerStats;

    public bool TriggerEndCheck = false;
    private bool canControlSittingState = false;

    private void Awake() {
        alertsUI = FindObjectOfType<AlertsPanelController>();
        wiz = FindObjectOfType<PlayerController>();
        move = FindObjectOfType<Move>();
        playerStats = FindObjectOfType<PlayerStats>();
        curDialogue = FindObjectOfType<CurrentDialogueControllerForTextAnimator>();
        combat = FindObjectOfType<PlayerCombat>();
        gameState = FindObjectOfType<GameState>();
        animator = GetComponent<Animator>();
        Invoke("ToggleSittingStateController", 1f);
    }

    private void ToggleSittingStateController(){
        canControlSittingState = true;
    }

    private void FixedUpdate() {


        if(!gameState.Paused && canControlSittingState){
            
            if(wiz.State.Sit && !wiz.AnimatorIsPlaying("sitting") &&  !wiz.AnimatorIsPlaying("lay") && !wiz.AnimatorIsPlaying("wake_from_lay_and_sword")){
                if(!wiz.AnimatorIsPlaying("sit")){
                    if(!PlayerController.Instance.AnimatorIsPlaying("waking_from_lay_and_sword")){
                        if(!PlayerController.Instance.AnimatorIsPlaying("lay")){
                            wiz.Animator.Play("sitting");
                            print("HERE!");
                        }
                    }
                }
            }

            if(insideTrigger && talkAlert){
                if(Inputs.Instance.HoldingUpArrow && wiz.AnimatorIsPlaying("idle")){
                    Inputs.Instance.HoldingUpArrow = false;
                    wiz.Move.StopPlayer();
                    HideAlert();
                    GetComponent<DialogueTrigger>().TriggerDialogue();
                    dialogueStarted = true;
                }
            }


            // ACABOU O DIALOGO!
            if(dialogueStarted){
                if(PlayerState.Instance.Interacting == false){
                    dialogueStarted = false;
                    if(spell){
                        animator.SetTrigger("end");
                    }   
                }
            }


            if(insideSaveTrigger){

                if(Inputs.Instance.HoldingDownArrow && (!wiz.AnimatorIsPlaying("sit") || !wiz.AnimatorIsPlaying("sitting"))){
                    if(wiz.AnimatorIsPlaying("idle")){
                        Inputs.Instance.HoldingDownArrow = false;
                        wiz.Move.StopPlayer();
                        HideAlert();
                        // flips wiz to the firepit
                        if(wiz.transform.position.x < this.transform.position.x){
                            if(wiz.State.FacingRight == false){
                                wiz.Move.Flip();
                            }
                        } else if(wiz.transform.position.x > this.transform.position.x){
                            if(wiz.State.FacingRight == true){
                                wiz.Move.Flip();
                            }
                        }
                        if(SaveTriggerEvent != null){
                            SaveTriggerEvent.Invoke();
                        }
                        wiz.Animator.Play("sitting");
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("wiz_health_yellow", 0);
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("wiz_health", ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_health").maxValue);
                        SaveSystem.Instance.SaveGame();
                    } else{
                        return;
                    }
                }

            } 

            if(saveAlert){
                if(wiz.State.Sit){
                    DisableTriggers();
                } else {
                    Invoke("EnableTriggers", 0.5f);
                }
            }

            if(insideInteractTrigger){
                if(Inputs.Instance.HoldingUpArrow && wiz.AnimatorIsPlaying("idle")){
                    Inputs.Instance.HoldingUpArrow = false;
                    wiz.State.Interacting = true;
                    wiz.Move.StopPlayer();
                    HideAlert();
                    GetComponent<InteractTrigger>().TriggerInteraction();
                }
            }

            if(insideUnlockTrigger){
                if(Inputs.Instance.HoldingUpArrow && wiz.AnimatorIsPlaying("idle")){
                    Inputs.Instance.HoldingUpArrow = false;
                    wiz.Move.StopPlayer();
                    HideAlert();
                    GetComponent<InteractTrigger>().TriggerInteraction();
                }
            }

            if(insideEnterTrigger){
                if(Inputs.Instance.HoldingUpArrow && wiz.AnimatorIsPlaying("idle")){
                    Inputs.Instance.HoldingUpArrow = false;
                    wiz.Move.StopPlayer();
                    HideAlert();
                    // GetComponent<SceneSwitcher>().WizPositionSetter();
                }
            }

            if(insideSaveTrigger){
                wiz.State.OnSaveTrigger = true;
            } else{
                wiz.State.OnSaveTrigger = false;
            }
        }       


    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            insideTrigger = true;
            if(spell || bottle){

                if(boxcolliderLeft)
                    boxcolliderLeft.enabled = false;

                if(boxcolliderRight)
                     boxcolliderRight.enabled = false;

                GetComponent<SoundsOnAnimator>().PlaySound1();
                GetComponent<DialogueTrigger>().TriggerDialogue();
                this.transform.position = PlayerController.Instance.transform.position;
                dialogueStarted = true;
                GetComponent<InteractTrigger>().TriggerInteraction();
            } else if(heartContainer || key){
                GetComponent<SoundsOnAnimator>().PlaySound1();
                GetComponent<InteractTrigger>().TriggerInteraction();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collider){
        if(collider.CompareTag("WizHitBox")){
            if(heartContainer || spell || key || bottle){
                //GetComponent<DialogueTrigger>().TriggerDialogue();
                //dialogueStarted = true;
                //GetComponent<InteractTrigger>().TriggerInteraction();
            } else{
                Invoke("ShowMessage", delayToShow);
            }
        }
            
    }

    private void ShowMessage(){
        if(insideTrigger){
            if(saveAlert && !wiz.State.DashingLight){                                                    // SAVE
                
	            if(alertObject != null){
		            alertObject.SetActive(true);
	            }
                insideSaveTrigger = true;
                insideTalkTrigger = false;
                insideEnterTrigger = false;

            } else if(talkAlert){                                                                                           // TALK
                insideTalkTrigger = true;
                insideUnlockTrigger = false;
                insideSaveTrigger = false;
                insideEnterTrigger = false;
                insideInteractTrigger = false;
                alertObject.SetActive(true);
            } else if(unlockAlert && playerStats.keys.initialValue >= 1){                   // UNLOCK
                insideUnlockTrigger = true;
                insideSaveTrigger = false;
                insideEnterTrigger = false;
                insideTalkTrigger = false;
                insideInteractTrigger = false;
                alertObject.SetActive(true);


            } else if(enterAlert){                                                                                      // ENTER
                insideEnterTrigger = true;
                insideSaveTrigger = false;
                insideTalkTrigger = false;
                insideUnlockTrigger = false;
                insideInteractTrigger = false;
                alertObject.SetActive(true);                 
            
            }else if(!saveAlert && !unlockAlert && !talkAlert){                                      //  ALL OTHER CASES <INTERACT>
                insideSaveTrigger = false;
                insideEnterTrigger = false;
                insideTalkTrigger = false;
                insideUnlockTrigger = false;
                if(!jumpAlert){
                    if(!attackAlert){
                        insideInteractTrigger = true;
                    }
                }
                alertObject.SetActive(true);        
            }

            if(whiteOutline){
                objectToTurnOutlineOn.GetComponent<ToggleWhiteOutline>().TurnOutlineOn();
            }           

        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            HideAlert();
        }  
    }

    private void HideAlert(){
        CancelInvoke();
        insideTrigger = false;
        insideTalkTrigger = false;
        insideSaveTrigger = false;
        insideInteractTrigger = false;
        insideEnterTrigger = false;
        insideUnlockTrigger = false;
        if(whiteOutline){
             objectToTurnOutlineOn.GetComponent<ToggleWhiteOutline>().TurnOutlineOff();
        }
        if(alertObject){
            alertObject.SetActive(false);
        }

    }

    private void DisableTriggers(){
        if(boxcolliderLeft != null){
            boxcolliderLeft.enabled = false;
        }
        if(boxcolliderRight != null){
            boxcolliderRight.enabled = false;
        }
    }

    private void EnableTriggers(){
        if(boxcolliderLeft != null){
            if(boxcolliderLeft.enabled == false){
                boxcolliderLeft.enabled = true;
                // move.pressedDownArrow = false;
            }
        }
        if(boxcolliderRight != null){
            if(boxcolliderRight.enabled == false){
                boxcolliderRight.enabled = true;
                // move.pressedDownArrow = false;
            }
        }
    }
}
