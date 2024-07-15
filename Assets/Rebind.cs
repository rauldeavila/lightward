using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using Sirenix.OdinInspector;
using System.Text;
using UnityEngine.Events;

public class Rebind : MonoBehaviour
{
    public InputActionAsset inputActionAsset;
    private const string rebindsKey = "rebinds";
    private const string actionMapName = "Game";

    public UnityEvent onKeyRebinded = new UnityEvent();

    public static Rebind Instance;

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
    }

    private void Start()
    {
        LoadBindings();
        BindDynamicInputs();
    }

    [Button]
    public void RebindAction(string controlScheme, string actionToRebind, string newBinding)
    {
        print("Attempting to Rebind Action -> " + controlScheme + " " + actionToRebind + " " + newBinding);
        if (inputActionAsset == null)
        {
            Debug.LogError("InputActionAsset not loaded. Cannot rebind.");
            return;
        }

        var actionMap = inputActionAsset.FindActionMap(actionMapName, true);
        if (actionMap == null)
        {
            Debug.LogError($"Action map '{actionMapName}' not found.");
            return;
        }

        var action = actionMap.FindAction(actionToRebind, true);
        if (action == null)
        {
            Debug.LogError($"Action '{actionToRebind}' not found in action map '{actionMapName}'.");
            return;
        }

        bool bindingFound = false;
        for (int i = 0; i < action.bindings.Count; i++)
        {
            if (action.bindings[i].groups.Contains(controlScheme))
            {
                action.ApplyBindingOverride(i, newBinding);
                bindingFound = true;
                break;
            }
        }

        if (!bindingFound)
        {
            Debug.LogError($"Binding for control scheme '{controlScheme}' not found in action '{actionToRebind}'.");
            return;
        }

        SaveBindings();
        Debug.Log($"Rebound '{actionToRebind}' for control scheme '{controlScheme}' to '{newBinding}'");

        // Rebind dynamic inputs to ensure changes are applied
        BindDynamicInputs();
        onKeyRebinded.Invoke();
    }

    [Button]
    public void SaveBindings()
    {
        if (inputActionAsset == null)
        {
            Debug.LogError("InputActionAsset not loaded. Cannot save bindings.");
            return;
        }

        var rebinds = inputActionAsset.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(rebindsKey, rebinds);
        Debug.Log("Bindings saved.");
    }

    [Button]
    public void LoadBindings()
    {
        if (inputActionAsset == null)
        {
            Debug.LogError("InputActionAsset not loaded. Cannot load bindings.");
            return;
        }

        if (PlayerPrefs.HasKey(rebindsKey))
        {
            var rebinds = PlayerPrefs.GetString(rebindsKey);
            inputActionAsset.LoadBindingOverridesFromJson(rebinds);
        }

        // Debug.Log("Bindings loaded.");
    }



    [Button]
    public void PrintAllBindingsForAllActions()
    {
        if (inputActionAsset == null)
        {
            Debug.LogError("InputActionAsset not loaded. Cannot print bindings.");
            return;
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("All Control Bindings for All Actions:");

        foreach (var actionMap in inputActionAsset.actionMaps)
        {
            sb.AppendLine($"Action Map: {actionMap.name}");

            foreach (var action in actionMap.actions)
            {
                sb.AppendLine($"  Action: {action.name}");
                sb.AppendLine("  Bindings:");

                foreach (var binding in action.bindings)
                {
                    sb.AppendLine($"    {binding.groups}: {binding.effectivePath}");
                }
            }
        }

        Debug.Log(sb.ToString());
    }

    [Button()]
    public string FindActionWithBinding(string controlScheme, string binding)
    {
        if (inputActionAsset == null)
        {
            Debug.LogError("InputActionAsset not loaded. Cannot find action.");
            return string.Empty;
        }

        foreach (var actionMap in inputActionAsset.actionMaps)
        {
            foreach (var action in actionMap.actions)
            {
                foreach (var actionBinding in action.bindings)
                {
                    if (actionBinding.groups.Contains(controlScheme) && actionBinding.effectivePath == binding)
                    {
                        return action.name;
                    }
                }
            }
        }

        Debug.LogError($"No action found with binding '{binding}' for control scheme '{controlScheme}'.");
        return string.Empty;
    }


    [Button()]
    public string GetBindingForActionAndScheme(string actionName, string controlScheme)
    {
        if (inputActionAsset == null)
        {
            Debug.LogError("InputActionAsset not loaded. Cannot get actions.");
            return string.Empty;
        }

        foreach (var actionMap in inputActionAsset.actionMaps)
        {
            var action = actionMap.FindAction(actionName, true);
            if (action != null)
            {
                foreach (var binding in action.bindings)
                {
                    if (binding.groups.Contains(controlScheme))
                    {
                        return binding.effectivePath;
                    }
                }
            }
        }

        Debug.LogError($"No binding found for action '{actionName}' with control scheme '{controlScheme}'.");
        return string.Empty;
    }


    [Button]
    public void ResetAllBindingsToDefault()
    {
        if (inputActionAsset == null)
        {
            Debug.LogError("InputActionAsset not loaded. Cannot reset bindings.");
            return;
        }

        // Define the default bindings for your game here
        var defaultBindings = new[]
        {
            new { Action = "MoveLeft", ControlScheme = "Keyboard", Binding = "<Keyboard>/leftArrow" },
            new { Action = "MoveRight", ControlScheme = "Keyboard", Binding = "<Keyboard>/rightArrow" },
            new { Action = "MoveUp", ControlScheme = "Keyboard", Binding = "<Keyboard>/upArrow" },
            new { Action = "MoveDown", ControlScheme = "Keyboard", Binding = "<Keyboard>/downArrow" },
            new { Action = "Jump", ControlScheme = "Keyboard", Binding = "<Keyboard>/space" },
            new { Action = "Attack", ControlScheme = "Keyboard", Binding = "<Keyboard>/x" },
            new { Action = "Fireball", ControlScheme = "Keyboard", Binding = "<Keyboard>/c" },
            new { Action = "DashingLight", ControlScheme = "Keyboard", Binding = "<Keyboard>/z" },
            new { Action = "Dash", ControlScheme = "Keyboard", Binding = "<Keyboard>/shift" },
            new { Action = "Item", ControlScheme = "Keyboard", Binding = "<Keyboard>/tab" },
            new { Action = "DashingSoul", ControlScheme = "Keyboard", Binding = "<Keyboard>/f" },
            new { Action = "Start", ControlScheme = "Keyboard", Binding = "<Keyboard>/enter" },
            new { Action = "Select", ControlScheme = "Keyboard", Binding = "<Keyboard>/backspace" },
            new { Action = "Console", ControlScheme = "Keyboard", Binding = "<Keyboard>/backquote" },
            new { Action = "MoveLeft", ControlScheme = "Gamepad", Binding = "<Gamepad>/dpad/left" },
            new { Action = "MoveRight", ControlScheme = "Gamepad", Binding = "<Gamepad>/dpad/right" },
            new { Action = "MoveUp", ControlScheme = "Gamepad", Binding = "<Gamepad>/dpad/up" },
            new { Action = "MoveDown", ControlScheme = "Gamepad", Binding = "<Gamepad>/dpad/down" },
            new { Action = "MoveLeft", ControlScheme = "Gamepad", Binding = "<Gamepad>/leftStick/left" },
            new { Action = "MoveRight", ControlScheme = "Gamepad", Binding = "<Gamepad>/leftStick/right" },
            new { Action = "MoveUp", ControlScheme = "Gamepad", Binding = "<Gamepad>/leftStick/up" },
            new { Action = "MoveDown", ControlScheme = "Gamepad", Binding = "<Gamepad>/leftStick/down" },
            new { Action = "Jump", ControlScheme = "Gamepad", Binding = "<Gamepad>/buttonSouth" },
            new { Action = "Attack", ControlScheme = "Gamepad", Binding = "<Gamepad>/buttonWest" },
            new { Action = "Fireball", ControlScheme = "Gamepad", Binding = "<Gamepad>/buttonEast" },
            new { Action = "DashingLight", ControlScheme = "Gamepad", Binding = "<Gamepad>/buttonNorth" },
            new { Action = "Dash", ControlScheme = "Gamepad", Binding = "<Gamepad>/rightShoulder" },
            new { Action = "Item", ControlScheme = "Gamepad", Binding = "<Gamepad>/leftShoulder" },
            new { Action = "DashingSoul", ControlScheme = "Gamepad", Binding = "<Gamepad>/rightTrigger" },
            new { Action = "Start", ControlScheme = "Gamepad", Binding = "<Gamepad>/start" },
            new { Action = "Select", ControlScheme = "Gamepad", Binding = "<Gamepad>/select" },
            // Add other default bindings here
        };

        // Remove all current binding overrides
        inputActionAsset.RemoveAllBindingOverrides();

        // Apply default bindings
        foreach (var binding in defaultBindings)
        {
            RebindAction(binding.ControlScheme, binding.Action, binding.Binding);
        }

        Debug.Log("All bindings have been reset to default.");

        // Rebind dynamic inputs to ensure changes are applied
        BindDynamicInputs();
    }

    private void BindDynamicInputs()
    {
        if (inputActionAsset == null)
        {
            Debug.LogError("InputActionAsset not loaded. Cannot bind dynamic inputs.");
            return;
        }

        var actionMap = inputActionAsset.FindActionMap(actionMapName, true);
        if (actionMap == null)
        {
            Debug.LogError($"Action map '{actionMapName}' not found.");
            return;
        }

        foreach (var action in actionMap.actions)
        {
            action.Disable();  // Disable action to ensure clean subscription
            action.performed += OnActionPerformed;
            action.canceled += OnActionCanceled;
            action.Enable();  // Enable action after subscription
            // Debug.Log($"Subscribed to action '{action.name}'");
        }

        // Debug.Log("Dynamic inputs bound successfully.");
    }

    private void OnActionPerformed(InputAction.CallbackContext ctx)
    {
        // Debug.Log("ON ACTION PERFORMED!!!!");
        Inputs.Instance.UpdateLastInputDevice(ctx);
        var actionName = ctx.action.name;
        // Debug.Log($"Action '{actionName}' was performed.");
        switch (actionName)
        {
            case "MoveRight":
                Inputs.Instance.RightArrowPerformed();
                break;
            case "MoveLeft":
                Inputs.Instance.LeftArrowPerformed();
                break;
            case "MoveUp":
                Inputs.Instance.UpArrowPerformed();
                break;
            case "MoveDown":
                Inputs.Instance.DownArrowPerformed();
                break;
            case "Jump":
                Inputs.Instance.JumpPerformed();
                break;
            case "Dash":
                Inputs.Instance.DashPerformed();
                break;
            case "Attack":
                Inputs.Instance.AttackPerformed();
                break;
            case "Item":
                Inputs.Instance.ItemPerformed();
                break;
            case "Start":
                Inputs.Instance.PausePerformed();
                break;
            case "Select":
                Inputs.Instance.SelectPerformed();
                break;
            case "Fireball":
                Inputs.Instance.FireballPerformed();
                break;
            case "DashingLight":
                Inputs.Instance.DashingLightPerformed();
                break;
            case "DashingSoul":
                Inputs.Instance.DashingSoulPerformed();
                break;
            default:
                Debug.LogWarning($"Action '{actionName}' not mapped.");
                break;
        }
    }

    private void OnActionCanceled(InputAction.CallbackContext ctx)
    {
        var actionName = ctx.action.name;
        // Debug.Log($"Action '{actionName}' was canceled.");
        switch (actionName)
        {
            case "MoveRight":
                Inputs.Instance.RightArrowCanceled();
                break;
            case "MoveLeft":
                Inputs.Instance.LeftArrowCanceled();
                break;
            case "MoveUp":
                Inputs.Instance.UpArrowCanceled();
                break;
            case "MoveDown":
                Inputs.Instance.DownArrowCanceled();
                break;
            case "Jump":
                Inputs.Instance.JumpCanceled();
                break;
            case "Dash":
                Inputs.Instance.DashCanceled();
                break;
            case "Attack":
                Inputs.Instance.AttackCanceled();
                break;
            case "Item":
                Inputs.Instance.ItemCanceled();
                break;
            case "Start":
                Inputs.Instance.PauseCanceled();
                break;
            case "Select":
                Inputs.Instance.SelectCanceled();
                break;
            case "Fireball":
                Inputs.Instance.FireballCanceled();
                break;
            case "DashingLight":
                Inputs.Instance.DashingLightCanceled();
                break;
            case "DashingSoul":
                Inputs.Instance.DashingSoulCanceled();
                break;
            default:
                Debug.LogWarning($"Action '{actionName}' not mapped.");
                break;
        }
    }

        public InputControl GetCurrentBindingControl(string actionName)
    {
        var action = inputActionAsset.FindAction(actionName);
        if (action != null && action.bindings.Count > 0)
        {
            var controlPath = action.bindings[0].effectivePath;
            return InputSystem.FindControl(controlPath);
        }
        return null;
    }
}
