using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI;

public class CurrentDialogueControllerForTextAnimator : MonoBehaviour {

    public bool beingTyped = false;
    public bool finishedTyping = false;
    private TextAnimatorPlayer taScript;
    [FMODUnity.EventRef]
    private string TypeWriterAudioEvent = "event:/game/00_ui/ui_typewriter";

    private void Awake(){
        taScript = GetComponent<TextAnimatorPlayer>();
    }

    void Update(){
        if(InteractionsManager.Instance.Dialogue){
            // if(DialogueManager.Instance.GetSpeakerName() == "not_speaking"){
            //     SkipTypeWriterNow();
            // }
            if(beingTyped){
                DialogueManager.Instance.SetIconToTypewriter();
            } else {
                if(DialogueManager.Instance.FinalDialogue){
                    // DialogueManager.Instance.SetIconToFinalDialogue();
                } else {
                    DialogueManager.Instance.SetIconToNextDialogue();
                }
            }
        }
    }


    public void OnTextShowedTest(){
        beingTyped = false;
        finishedTyping = true;
    }

    public void OnTypeWriterStart(){
        beingTyped = true;
        finishedTyping = false;
    }

    public void SkipTypeWriterNow(){
        taScript.SkipTypewriter();
        beingTyped = false;
        finishedTyping = true;
    }

    public void PlayTypewriterSound(){
        FMODUnity.RuntimeManager.PlayOneShot("event:/game/00_ui/ui_typewriter", transform.position);
    }




}
