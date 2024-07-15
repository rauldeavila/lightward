using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizFallBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // CameraSystem.Instance.LookDownFalling(); called on wizcallsonanimator
       animator.SetBool("hardFall", false);
       PlayerController.Instance.Animator.SetBool("facingWall", false);
        // AnimatorStateInfo previousState = animator.GetCurrentAnimatorStateInfo(layerIndex - 1); // Get the previous state info
        // if (previousState.IsName("dodge")) // Only stop dashing if previous state was "Dodge"
        // {
        //     DashController.Instance.StopDashing();
        // }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {

    // }

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
