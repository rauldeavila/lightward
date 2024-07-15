using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TriggersWhenGrounded : MonoBehaviour {

    public bool hasAnimator;
    public bool triggered = false;
    [ShowIf("hasAnimator")]
    public Animator animator;
    private bool doOnce = false;


    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Ground")){
            triggered = true;
             if(hasAnimator && !doOnce){
                 doOnce = !doOnce;
                animator.SetTrigger("particles");
             }
        }



    }

}
