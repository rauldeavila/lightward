using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDyingBehaviour : StateMachineBehaviour {

    private EnemyController enemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       enemy = animator.GetComponent<EnemyController>();
       enemy.GetComponent<EnemyState>().Dead = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       enemy.GetComponent<EnemyDead>().KillEnemy();
    }

}
