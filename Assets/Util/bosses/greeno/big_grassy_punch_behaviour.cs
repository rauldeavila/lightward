using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class big_grassy_punch_behaviour : StateMachineBehaviour {

    private EnemyController enemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       enemy = animator.GetComponent<EnemyController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       enemy.GetComponent<EnemyAttackWhenWizOnRadius>().Attack();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       enemy.State.Attacking = false;
    }
}
