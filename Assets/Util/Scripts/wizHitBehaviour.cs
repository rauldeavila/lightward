using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizHitBehaviour : StateMachineBehaviour {

    private WizHit wizHitScript;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       wizHitScript = FindObjectOfType<WizHit>();
       wizHitScript.running = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       wizHitScript.Hit();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       wizHitScript.End();
    }

}
