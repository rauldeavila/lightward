using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    public bool EnableCombosOnlyOnEditor = false; // Only allow combos to run in the Unity editor


    public float startSelectTimeGap = 0.5f; // Allowed time gap between Start and Select key presses
    public float jumpAttackTimeGap = 0.5f; // Allowed time gap between Jump and Attack key presses in the combo
    public float arrowSelectTimeGap = 0.7f; // Allowed time gap between arrow keys and Select key presses in the combo
    public float upDownSelectTimeGap = 0.7f; // Allowed time gap between up, down, up, down, select keys in the combo
    public float comboCooldown = 1.0f; // Cooldown period after a combo is detected

    private float lastPressTime;
    private float lastComboTime;
    private int startSelectComboIndex = 0;
    private int jumpAttackComboIndex = 0;
    private int arrowSelectComboIndex = 0;
    private int upDownSelectComboIndex = 0;

    // Define enums for combo steps to make the code more readable
    private enum StartSelectComboStep
    {
        Start,
        Select
    }

    private enum JumpAttackComboStep
    {
        Jump1,
        Attack1,
        Jump2,
        Attack2
    }

    private enum ArrowSelectComboStep
    {
        LeftArrow,
        RightArrow,
        UpArrow,
        DownArrow,
        Select
    }

    private enum UpDownSelectComboStep
    {
        UpArrow1,
        DownArrow1,
        UpArrow2,
        DownArrow2,
        Select
    }

    // Define the combo sequences using the enums
    private StartSelectComboStep[] startSelectComboSequence = { StartSelectComboStep.Start, StartSelectComboStep.Select };
    private JumpAttackComboStep[] jumpAttackComboSequence = { JumpAttackComboStep.Jump1, JumpAttackComboStep.Attack1, JumpAttackComboStep.Jump2, JumpAttackComboStep.Attack2 };
    private ArrowSelectComboStep[] arrowSelectComboSequence = { ArrowSelectComboStep.LeftArrow, ArrowSelectComboStep.RightArrow, ArrowSelectComboStep.UpArrow, ArrowSelectComboStep.DownArrow, ArrowSelectComboStep.Select };
    private UpDownSelectComboStep[] upDownSelectComboSequence = { UpDownSelectComboStep.UpArrow1, UpDownSelectComboStep.DownArrow1, UpDownSelectComboStep.UpArrow2, UpDownSelectComboStep.DownArrow2, UpDownSelectComboStep.Select };

    private void Update()
    {
        if (EnableCombosOnlyOnEditor && !Application.isEditor)
        {
            return; // If combos are only enabled in the editor and we are not in the editor, do nothing
        }

        // Reset combo if too much time has passed between key presses
        if (Time.unscaledTime - lastPressTime > startSelectTimeGap)
        {
            startSelectComboIndex = 0;
        }

        if (Time.unscaledTime - lastPressTime > jumpAttackTimeGap)
        {
            jumpAttackComboIndex = 0;
        }

        if (Time.unscaledTime - lastPressTime > arrowSelectTimeGap)
        {
            arrowSelectComboIndex = 0;
        }

        if (Time.unscaledTime - lastPressTime > upDownSelectTimeGap)
        {
            upDownSelectComboIndex = 0;
        }

        CheckStartSelectCombo();
        CheckJumpAttackCombo();
        CheckArrowSelectCombo();
        CheckUpDownSelectCombo();
    }

    private void CheckStartSelectCombo()
    {
        if (Time.unscaledTime - lastComboTime < comboCooldown)
        {
            return; // Prevent the combo from being detected too quickly
        }

        switch (startSelectComboIndex)
        {
            case 0:
                if (Inputs.Instance.HoldingStart)
                {
                    lastPressTime = Time.unscaledTime;
                    startSelectComboIndex++;
                }
                break;
            case 1:
                if (Inputs.Instance.HoldingSelect)
                {
                    startSelectComboIndex++;
                    lastPressTime = Time.unscaledTime;
                    OnStartSelectComboDetected();
                }
                break;
        }
    }

    private void CheckJumpAttackCombo()
    {
        if (Time.unscaledTime - lastComboTime < comboCooldown)
        {
            return; // Prevent the combo from being detected too quickly
        }

        switch (jumpAttackComboIndex)
        {
            case 0:
                if (Inputs.Instance.HoldingJump)
                {
                    lastPressTime = Time.unscaledTime;
                    jumpAttackComboIndex++;
                }
                break;
            case 1:
                if (Inputs.Instance.HoldingAttack)
                {
                    lastPressTime = Time.unscaledTime;
                    jumpAttackComboIndex++;
                }
                break;
            case 2:
                if (Inputs.Instance.HoldingJump)
                {
                    lastPressTime = Time.unscaledTime;
                    jumpAttackComboIndex++;
                }
                break;
            case 3:
                if (Inputs.Instance.HoldingAttack)
                {
                    jumpAttackComboIndex++;
                    lastPressTime = Time.unscaledTime;
                    OnJumpAttackComboDetected();
                }
                break;
        }
    }

    private void CheckArrowSelectCombo()
    {
        if (Time.unscaledTime - lastComboTime < comboCooldown)
        {
            return; // Prevent the combo from being detected too quickly
        }

        switch (arrowSelectComboIndex)
        {
            case 0:
                if (Inputs.Instance.HoldingLeftArrow)
                {
                    lastPressTime = Time.unscaledTime;
                    arrowSelectComboIndex++;
                }
                break;
            case 1:
                if (Inputs.Instance.HoldingRightArrow)
                {
                    lastPressTime = Time.unscaledTime;
                    arrowSelectComboIndex++;
                }
                break;
            case 2:
                if (Inputs.Instance.HoldingUpArrow)
                {
                    lastPressTime = Time.unscaledTime;
                    arrowSelectComboIndex++;
                }
                break;
            case 3:
                if (Inputs.Instance.HoldingDownArrow)
                {
                    lastPressTime = Time.unscaledTime;
                    arrowSelectComboIndex++;
                }
                break;
            case 4:
                if (Inputs.Instance.HoldingSelect)
                {
                    arrowSelectComboIndex++;
                    lastPressTime = Time.unscaledTime;
                    OnArrowSelectComboDetected();
                }
                break;
        }
    }

    private void CheckUpDownSelectCombo()
    {
        if (Time.unscaledTime - lastComboTime < comboCooldown)
        {
            return; // Prevent the combo from being detected too quickly
        }

        switch (upDownSelectComboIndex)
        {
            case 0:
                if (Inputs.Instance.HoldingUpArrow)
                {
                    lastPressTime = Time.unscaledTime;
                    upDownSelectComboIndex++;
                }
                break;
            case 1:
                if (Inputs.Instance.HoldingDownArrow)
                {
                    lastPressTime = Time.unscaledTime;
                    upDownSelectComboIndex++;
                }
                break;
            case 2:
                if (Inputs.Instance.HoldingUpArrow)
                {
                    lastPressTime = Time.unscaledTime;
                    upDownSelectComboIndex++;
                }
                break;
            case 3:
                if (Inputs.Instance.HoldingDownArrow)
                {
                    lastPressTime = Time.unscaledTime;
                    upDownSelectComboIndex++;
                }
                break;
            case 4:
                if (Inputs.Instance.HoldingSelect)
                {
                    upDownSelectComboIndex++;
                    lastPressTime = Time.unscaledTime;
                    OnUpDownSelectComboDetected();
                }
                break;
        }
    }

    private void OnStartSelectComboDetected()
    {
        Debug.Log("Start + Select combo detected! - it does nothing.");
        lastComboTime = Time.unscaledTime; // Set the last combo time
        // Reset the combo index for the next detection
        startSelectComboIndex = 0;
    }

    private void OnJumpAttackComboDetected()
    {
        Debug.Log("Jump + Attack + Jump + Attack combo detected! - it does nothing.");
        lastComboTime = Time.unscaledTime; // Set the last combo time
        // Reset the combo index for the next detection
        jumpAttackComboIndex = 0;
    }

    private void OnArrowSelectComboDetected()
    {
        // Debug.Log("← + → + ↑ + ↓ + Select combo detected!");
        lastComboTime = Time.unscaledTime; // Set the last combo time
        // Reset the combo index for the next detection
        arrowSelectComboIndex = 0;
        Move.Instance.NoClip();
    }

    private void OnUpDownSelectComboDetected()
    {
        // Debug.Log("↑ + ↓ + ↑ + ↓ + Select combo detected!");
        lastComboTime = Time.unscaledTime; // Set the last combo time
        // Reset the combo index for the next detection
        upDownSelectComboIndex = 0;
        GameManager.Instance.InstantShadowWalk();
    }
}
