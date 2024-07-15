using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizRollBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        StateController.Instance.CanJump = false;
        PlayerState.Instance.Jump = false;
        PlayerController.Instance.KnockWizBack(0f, 0f, true);
        if(PlayerState.Instance.FacingRight){
            PlayerController.Instance.KnockWizBack(3f, 6f);
        } else {
            PlayerController.Instance.KnockWizBack(-3f, 6f);
        }
        PlayerController.Instance.SetGravityToOneInSeconds(0.2f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerState.Instance.Roll = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
