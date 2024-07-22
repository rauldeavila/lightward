using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SavingStar : MonoBehaviour
{
    public Animator animator;
    private bool _waitForButtonReset = false;
    void Update()
    {
        if(PlayerController.Instance.AnimatorIsPlaying("sit") && GameState.Instance.Overworld)
        {
            if(Inputs.Instance.HoldingJump)
            {
                if(_waitForButtonReset == false)
                {
                    animator.SetBool("holding_key", true);
                }
            }
            else
            {
                _waitForButtonReset = false;
                animator.SetBool("holding_key", false);
            }
        }
        else
        {
            if(_waitForButtonReset)
            {
                if(Inputs.Instance.HoldingJump)
                {
                    _waitForButtonReset = true;
                    print("waiting for button reset");
                }
                else
                {
                    print("button was reset");
                    _waitForButtonReset = false;
                }
            }
            animator.SetBool("holding_key", false);
        }
    }

    public void OpenSavePanel()
    {
        GameManager.Instance.OpenSavePanel();
    }
}
