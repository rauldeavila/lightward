using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimatorWhenPlayerEntersTrigger : MonoBehaviour {

    public Animator animator;
    public bool triggerOnlyOneTime = false;
    private bool doOnlyOnce = false;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            if(triggerOnlyOneTime != true){
                animator.SetTrigger("start");
            } else{
                if(doOnlyOnce == false){
                    doOnlyOnce = true;
                    animator.SetTrigger("start");
                }
            }
        }
    }

}
