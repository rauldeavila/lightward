using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimatorHandler : MonoBehaviour {

    private Animator animator;

    void Awake(){
        animator = GetComponent<Animator>();
    }
 
    void Update(){
        if(animator != null){
            animator.speed = Time.timeScale;
        }
    }
}
