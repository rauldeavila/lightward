using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    public static Inputs Instance;
    public GameControls _gameControls;
    private InputDevice lastDevice;
    public bool Keyboard;
    public bool AnyKeyWasPressed;

    public bool HoldingRightArrow;
    public bool HoldingLeftArrow;
    public bool HoldingUpArrow;
    public bool HoldingDownArrow;
    public bool HoldingJump;
    public bool HoldingDash;
    public bool HoldingAttack;
    public bool HoldingItem;
    public bool HoldingFireball;
    public bool HoldingTimeControl;
    public bool HoldingDashingSoul;
    public bool HoldingDashingLight;
    public bool HoldingStart;
    public bool HoldingSelect;

    public string LastControlPath { get; private set; }
    public string LastControlDisplayName { get; private set; }

    public List<SignalSender> SignalSenders;

    private bool _inputsEnabled;

    private Dictionary<string, float> lastCallTime;
    private float callCooldown = 0.1f; // Adjust this value as needed

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        SignalSenders = Resources.LoadAll<SignalSender>("ScriptableObjects/SignalSenders").ToList();

        _gameControls = new GameControls();
        _gameControls.Enable();

        ResetAllButtonPress();

        lastCallTime = new Dictionary<string, float>();
    }

    void OnDisable()
    {
        _gameControls.Disable();
    }

    public void UpdateLastInputDevice(InputAction.CallbackContext ctx)
    {
        lastDevice = ctx.control?.device;
        LastControlPath = ctx.control.path;
        LastControlDisplayName = ctx.control.displayName;

        if (lastDevice != null)
        {
            Keyboard = lastDevice.displayName == "Keyboard";
            AnyKeyWasPressed = true;
        }
    }

    public void ResetAllButtonPress()
    {
        HoldingRightArrow = false;
        HoldingLeftArrow = false;
        HoldingUpArrow = false;
        HoldingDownArrow = false;
        HoldingJump = false;
        HoldingAttack = false;
        HoldingItem = false;
        HoldingFireball = false;
        HoldingTimeControl = false;
        HoldingDashingSoul = false;
        HoldingDashingLight = false;
        HoldingStart = false;
        HoldingSelect = false;
    }

    public void DisableInputs()
    {
        _inputsEnabled = false;
    }

    public void EnableInputs()
    {
        _inputsEnabled = true;
    }

    private int GetBindingIndexForControlScheme(InputAction action, string controlScheme)
    {
        for (int i = 0; i < action.bindings.Count; i++)
        {
            if (action.bindings[i].groups.Contains(controlScheme))
            {
                return i;
            }
        }
        return -1;
    }

    private bool CanCall(string actionName)
    {
        if (!lastCallTime.ContainsKey(actionName) || Time.unscaledTime - lastCallTime[actionName] > callCooldown)
        {
            lastCallTime[actionName] = Time.unscaledTime;
            return true;
        }
        return false;
    }

    public void RightArrowPerformed()
    {
        if (_inputsEnabled && CanCall(nameof(RightArrowPerformed)))
        {
            HoldingRightArrow = true;
            GetSignalSender("SignalHoldingRightArrow").Raise();
        }
    }

    public void RightArrowCanceled()
    {
        if (_inputsEnabled && CanCall(nameof(RightArrowCanceled)))
        {
            HoldingRightArrow = false;
            GetSignalSender("SignalReleasedRightArrow").Raise();
        }
    }

    public void LeftArrowPerformed()
    {
        if (_inputsEnabled && CanCall(nameof(LeftArrowPerformed)))
        {
            HoldingLeftArrow = true;
            GetSignalSender("SignalHoldingLeftArrow").Raise();
        }
    }

    public void LeftArrowCanceled()
    {
        if (_inputsEnabled && CanCall(nameof(LeftArrowCanceled)))
        {
            HoldingLeftArrow = false;
            GetSignalSender("SignalReleasedLeftArrow").Raise();
        }
    }

    public void UpArrowPerformed()
    {
        if (_inputsEnabled && CanCall(nameof(UpArrowPerformed)))
        {
            HoldingUpArrow = true;
            GetSignalSender("SignalHoldingUpArrow").Raise();
        }
    }

    public void UpArrowCanceled()
    {
        if (_inputsEnabled && CanCall(nameof(UpArrowCanceled)))
        {
            HoldingUpArrow = false;
            GetSignalSender("SignalReleasedUpArrow").Raise();
        }
    }

    public void DownArrowPerformed()
    {
        if (_inputsEnabled && CanCall(nameof(DownArrowPerformed)))
        {
            HoldingDownArrow = true;
            GetSignalSender("SignalHoldingDownArrow").Raise();
        }
    }

    public void DownArrowCanceled()
    {
        if (_inputsEnabled && CanCall(nameof(DownArrowCanceled)))
        {
            HoldingDownArrow = false;
            GetSignalSender("SignalReleasedDownArrow").Raise();
        }
    }

    public void JumpPerformed()
    {
        if (_inputsEnabled && CanCall(nameof(JumpPerformed)))
        {
            HoldingJump = true;
            GetSignalSender("SignalHoldingJump").Raise();
        }
    }

    public void JumpCanceled()
    {
        if (_inputsEnabled && CanCall(nameof(JumpCanceled)))
        {
            HoldingJump = false;
            GetSignalSender("SignalReleasedJump").Raise();
        }
    }

    public void DashPerformed()
    {
        if (_inputsEnabled && CanCall(nameof(DashPerformed)))
        {
            HoldingDash = true;
            GetSignalSender("SignalHoldingDash").Raise();
        }
    }

    public void DashCanceled()
    {
        if (_inputsEnabled && CanCall(nameof(DashCanceled)))
        {
            HoldingDash = false;
            GetSignalSender("SignalReleasedDash").Raise();
        }
    }

    public void AttackPerformed()
    {
        if (_inputsEnabled && CanCall(nameof(AttackPerformed)))
        {
            HoldingAttack = true;
            GetSignalSender("SignalHoldingAttack").Raise();
        }
    }

    public void AttackCanceled()
    {
        if (_inputsEnabled && CanCall(nameof(AttackCanceled)))
        {
            HoldingAttack = false;
            GetSignalSender("SignalReleasedAttack").Raise();
        }
    }

    public void ItemPerformed()
    {
        if (_inputsEnabled && CanCall(nameof(ItemPerformed)))
        {
            HoldingItem = true;
            GetSignalSender("SignalHoldingItem").Raise();
        }
    }

    public void ItemCanceled()
    {
        if (_inputsEnabled && CanCall(nameof(ItemCanceled)))
        {
            HoldingItem = false;
            GetSignalSender("SignalReleasedItem").Raise();
        }
    }

    public void PausePerformed()
    {
        if (_inputsEnabled && CanCall(nameof(PausePerformed)))
        {
            HoldingStart = true;
            GetSignalSender("SignalHoldingStart").Raise();
        }
    }

    public void PauseCanceled()
    {
        if (_inputsEnabled && CanCall(nameof(PauseCanceled)))
        {
            HoldingStart = false;
            GetSignalSender("SignalReleasedStart").Raise();
        }
    }

    public void SelectPerformed()
    {
        if (_inputsEnabled && CanCall(nameof(SelectPerformed)))
        {
            HoldingSelect = true;
            GetSignalSender("SignalHoldingSelect").Raise();
        }
    }

    public void SelectCanceled()
    {
        if (_inputsEnabled && CanCall(nameof(SelectCanceled)))
        {
            HoldingSelect = false;
            GetSignalSender("SignalReleasedSelect").Raise();
        }
    }

    public void FireballPerformed()
    {
        if (_inputsEnabled && CanCall(nameof(FireballPerformed)))
        {
            HoldingFireball = true;
            GetSignalSender("SignalHoldingFireball").Raise();
        }
    }

    public void FireballCanceled()
    {
        if (_inputsEnabled && CanCall(nameof(FireballCanceled)))
        {
            HoldingFireball = false;
            GetSignalSender("SignalReleasedFireball").Raise();
        }
    }

    public void DashingLightPerformed()
    {
        if (_inputsEnabled && CanCall(nameof(DashingLightPerformed)))
        {
            HoldingDashingLight = true;
            GetSignalSender("SignalHoldingDashingLight").Raise();
        }
    }

    public void DashingLightCanceled()
    {
        if (_inputsEnabled && CanCall(nameof(DashingLightCanceled)))
        {
            HoldingDashingLight = false;
            GetSignalSender("SignalReleasedDashingLight").Raise();
        }
    }

    public void DashingSoulPerformed()
    {
        if (_inputsEnabled && CanCall(nameof(DashingSoulPerformed)))
        {
            HoldingDashingSoul = true;
            GetSignalSender("SignalHoldingDashingSoul").Raise();
        }
    }

    public void DashingSoulCanceled()
    {
        if (_inputsEnabled && CanCall(nameof(DashingSoulCanceled)))
        {
            HoldingDashingSoul = false;
            GetSignalSender("SignalReleasedDashingSoul").Raise();
        }
    }

    public SignalSender GetSignalSender(string name)
    {
        return SignalSenders.Find(item => item.name == name);
    }
}
