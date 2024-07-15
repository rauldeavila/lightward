using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour {

    public bool onGround = false;
    public bool updateAnimatorParameter = false;
    private Animator animator;

    private void Awake() {
        if (updateAnimatorParameter) {
            animator = GetComponentInParent<Animator>();
        }
    }
    
    private void OnTriggerStay2D(Collider2D collider){
        if(collider.CompareTag("Ground")){
            onGround = true;
            if (updateAnimatorParameter) {
                animator.SetBool("onGround", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider){
        if(collider.CompareTag("Ground")){
            onGround = false;
            if (updateAnimatorParameter) {
                animator.SetBool("onGround", false);
            }
        }
    }


}
