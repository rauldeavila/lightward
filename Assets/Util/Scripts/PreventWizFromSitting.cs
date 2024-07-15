using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventWizFromSitting : MonoBehaviour {

    private PlayerController wiz;

    private void Awake() {
        wiz = FindObjectOfType<PlayerController>();
    }


   private void OnTriggerEnter2D(Collider2D collider) {
       if(collider.CompareTag("WizHitBox")){
           if(wiz.AnimatorIsPlaying("sit") || wiz.AnimatorIsPlaying("sitting")){
               print("here no enter");
               wiz.Animator.Play("idle");
           }
       }   
   }

   private void OnTriggerStay2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
           if(wiz.AnimatorIsPlaying("sit") || wiz.AnimatorIsPlaying("sitting")){
               print("here no stay");
               wiz.Animator.Play("idle");
           }
       }
   }
}
