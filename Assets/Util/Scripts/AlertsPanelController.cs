using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertsPanelController : MonoBehaviour {

    private Animator animator;
    public GameObject jumpMessage;
    public GameObject attackMessage;
    public GameObject talkMessage;
    public GameObject saveMessage;
    public GameObject wakeUpMessage;
    public GameObject interactMessage;
    public GameObject enterMessage;

    [HideInInspector]
    public bool showingJumpAlert;
    [HideInInspector]
    public bool showingAttackAlert;
    [HideInInspector]
    public bool showingTalkAlert;
    [HideInInspector]
    public bool showingSaveAlert;
    [HideInInspector]
    public bool showingWakeUpAlert;
    [HideInInspector]
    public bool showingInteractAlert;
    [HideInInspector]
    public bool showingEnterAlert;

    private void Awake() {
        animator = GetComponent<Animator>();
        showingAttackAlert = false;
        showingJumpAlert = false;
        showingTalkAlert = false;
        showingSaveAlert = false;
        showingWakeUpAlert = false;
        showingInteractAlert = false;
        showingEnterAlert = false;
        jumpMessage.SetActive(false);
        attackMessage.SetActive(false);
        talkMessage.SetActive(false);
        saveMessage.SetActive(false);
        wakeUpMessage.SetActive(false);
        interactMessage.SetActive(false);
        enterMessage.SetActive(false);
    }

    public void ShowJumpAlert(){
        jumpMessage.SetActive(true);
        showingJumpAlert = true;
        animator.SetTrigger("showMessage");
    }

    public void ShowAttackAlert(){
        attackMessage.SetActive(true);
        showingAttackAlert = true;
        animator.SetTrigger("showMessage");
    }

    public void ShowTalkAlert(){
        talkMessage.SetActive(true);
        showingTalkAlert = true;
        animator.SetTrigger("showMessage");
    }

    public void ShowSaveAlert(){
        saveMessage.SetActive(true);
        showingSaveAlert = true;
        animator.SetTrigger("showMessage");
    }

    public void ShowWakeUpAlert(){
        wakeUpMessage.SetActive(true);
        showingWakeUpAlert = true;
        animator.SetTrigger("showMessage");
    }

    public void ShowInteractAlert(){
        interactMessage.SetActive(true);
        showingInteractAlert = true;
        animator.SetTrigger("showMessage");
    }

    public void ShowEnterAlert(){
        enterMessage.SetActive(true);
        showingEnterAlert = true;
        animator.SetTrigger("showMessage");
    }

    public void HideAlertWindow(){
        animator.ResetTrigger("showMessage");
        showingAttackAlert = false;
        showingJumpAlert = false;
        showingTalkAlert = false;
        showingSaveAlert = false;
        showingWakeUpAlert = false;
        showingInteractAlert = false;
        showingEnterAlert = false;
        animator.SetTrigger("hideMessage");
        attackMessage.SetActive(false);
        jumpMessage.SetActive(false);
        talkMessage.SetActive(false);
        saveMessage.SetActive(false);
        wakeUpMessage.SetActive(false);
        interactMessage.SetActive(false);
        enterMessage.SetActive(false);
    }







}
