using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizAttack1Behaviour : StateMachineBehaviour {
    
    private PlayerController wiz;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        wiz = FindObjectOfType<PlayerController>();
        wiz.Animator.SetBool("attack3", false);
        wiz.Animator.SetBool("attack2", false);
        wiz.Animator.SetBool("attack1", false);
    }

}
