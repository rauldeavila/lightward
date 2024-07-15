using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerMovement : MonoBehaviour {

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            animator.SetTrigger("move");
        }    
    }

}
