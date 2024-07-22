using System.Collections;
using UnityEngine;

public class BedtimeBehaviour : StateMachineBehaviour
{
    private Animator animator;
    private static readonly int HoldingUp = Animator.StringToHash("HoldingUp");
    private static readonly int WakeyWakey = Animator.StringToHash("wakeywakey");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.animator = animator;
        WizCallsOnAnimator.Instance.StartBedtimeCoroutine();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Inputs.Instance.HoldingUpArrow)
        {
            animator.Play(WakeyWakey);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        DisplayButtonOnScreen.Instance.HideButtonPrompt();
    }
}
