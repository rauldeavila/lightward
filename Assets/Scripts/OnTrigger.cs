using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class OnTrigger : MonoBehaviour {

    public bool DisplayMessageFromHandleInputOnEnter = false;

    public bool IgnoreWhenSitting = false;
    public bool ShowOnlyWhileSitting = false;
    public bool DestroyAfterTrigger = false;
    public bool DisableAfterTrigger = false;
    public float TimeBeforeReenabling = 0.5f;

    public UnityEvent WizEnteredTriggerEvent;
    public UnityEvent WizExitedTriggerEvent;

    private BoxCollider2D _boxCol;

    private bool _onceFlag = false;

    void Awake(){
        _boxCol = GetComponent<BoxCollider2D>();
        Invoke("EnableCol", 0.3f);
    }

    void EnableCol(){
        _boxCol.enabled = true;
    }


    void OnTriggerStay2D(Collider2D collider)
    {
        if(!Move.Instance.IsNoClipActive)
        {
            if(IgnoreWhenSitting)
            {
                if(PlayerController.Instance.AnimatorIsPlaying("sit") || PlayerController.Instance.AnimatorIsPlaying("sitting"))
                {
                    gameObject.SetActive(false);
                    return;
                }
            }

            if(_onceFlag == false)
            {
                if(ShowOnlyWhileSitting && (PlayerController.Instance.AnimatorIsPlaying("sit")))
                {
                    WizEnteredTriggerEvent.Invoke();
                    _onceFlag = true;
                    if(DisableAfterTrigger){
                        _boxCol.enabled = false;
                        Invoke("EnableCol", TimeBeforeReenabling);
                    }
                    if(DestroyAfterTrigger){
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(!Move.Instance.IsNoClipActive)
        {
            if(collider.CompareTag("WizHitBox") || collider.CompareTag("WizRoll"))
            {
                if(ShowOnlyWhileSitting && (!PlayerController.Instance.AnimatorIsPlaying("sit") || !PlayerController.Instance.AnimatorIsPlaying("sitting")))
                {
                    return;
                }
                if(DisplayMessageFromHandleInputOnEnter)
                {
                    DisplayButtonOnScreen.Instance.ShowButtonPrompt(GetComponent<HandleInput>().ActionName, GetComponent<HandleInput>().Message);
                }
                WizEnteredTriggerEvent?.Invoke();
                if(DisableAfterTrigger){
                    _boxCol.enabled = false;
                    Invoke("EnableCol", TimeBeforeReenabling);
                }
                if(DestroyAfterTrigger){
                    gameObject.SetActive(false);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collider) {
        if(!Move.Instance.IsNoClipActive)
        {
            if(collider.CompareTag("WizHitBox") || collider.CompareTag("WizRoll")){
                if(DisplayMessageFromHandleInputOnEnter)
                {
                    DisplayButtonOnScreen.Instance.HideButtonPrompt();
                }
                if(WizExitedTriggerEvent != null){
                    WizExitedTriggerEvent.Invoke();
                    _onceFlag = false;
                    if(DestroyAfterTrigger){
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }


}
