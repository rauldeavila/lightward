using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePanelController : MonoBehaviour {

    public BoolValue wiz_has_magic;
    private Animator animator;

    private void Awake(){
        animator = GetComponent<Animator>();

        if(wiz_has_magic.initialValue == false){
            animator.Play("no_magic");
        } else if(wiz_has_magic.initialValue == true){
            animator.Play("has_magic");
        }
    }

    public void PlayMagicIntro(){
        animator.Play("has_magic_intro"); // exit time of 1 to has_magic
    }




}
