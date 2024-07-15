using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    public string LastUsedInput;
    public string LastAction;
    
    private Inputs inputs;
    private List<SignalListener> listeners = new List<SignalListener>();

    // Add the public string variable to store the last used input

    private string previousDeviceName;

    public static InputListener Instance;

    // Define the UnityEvent
    public UnityEvent onLastUsedInputChanged = new UnityEvent();

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
        inputs = Inputs.Instance;

        // Load all SignalSenders and create SignalListeners for them
        foreach (var signalSender in Resources.LoadAll<SignalSender>("ScriptableObjects/SignalSenders"))
        {
            CreateSignalListener(signalSender);
        }

        // Log all connected gamepads
        var gamepads = Gamepad.all;
        foreach (var gamepad in gamepads)
        {
            bool isActive = gamepad.enabled;
            Debug.Log($"Gamepad: {gamepad.displayName}, Active: {isActive}");
        }
    }

    private void CreateSignalListener(SignalSender signalSender)
    {
        // Create a new GameObject for the SignalListener
        GameObject listenerObject = new GameObject(signalSender.name + "Listener");
        listenerObject.transform.SetParent(transform);

        // Add a SignalListener component to the new GameObject
        SignalListener signalListener = listenerObject.AddComponent<SignalListener>();

        // Disable the SignalListener component initially
        signalListener.enabled = false;

        // Assign the SignalSender property
        signalListener.SignalSender = signalSender;

        // Create a UnityEvent and add the OnSignalRaised method as a listener
        UnityEvent unityEvent = new UnityEvent();
        unityEvent.AddListener(() => OnSignalRaised(signalSender.name));

        // Assign the UnityEvent to the SignalListener
        signalListener.SignalEvent = unityEvent;

        // Enable the SignalListener component after setting the SignalSender
        signalListener.enabled = true;

        // Add the SignalListener to the list
        listeners.Add(signalListener);
    }

    private void OnSignalRaised(string signalName)
    {
        // Debug.Log($"Signal raised: {signalName}");

        if (inputs.HoldingLeftArrow && signalName == "SignalHoldingLeftArrow") PrintAction("MoveLeft");
        else if (inputs.HoldingRightArrow && signalName == "SignalHoldingRightArrow") PrintAction("MoveRight");
        else if (inputs.HoldingUpArrow && signalName == "SignalHoldingUpArrow") PrintAction("MoveUp");
        else if (inputs.HoldingDownArrow && signalName == "SignalHoldingDownArrow") PrintAction("MoveDown");
        else if (inputs.HoldingJump && signalName == "SignalHoldingJump") PrintAction("Jump");
        else if (inputs.HoldingDash && signalName == "SignalHoldingDash") PrintAction("Dash");
        else if (inputs.HoldingAttack && signalName == "SignalHoldingAttack") PrintAction("Attack");
        else if (inputs.HoldingItem && signalName == "SignalHoldingItem") PrintAction("Item");
        else if (inputs.HoldingFireball && signalName == "SignalHoldingFireball") PrintAction("Fireball");
        else if (inputs.HoldingDashingLight && signalName == "SignalHoldingDashingLight") PrintAction("DashingLight");
        else if (inputs.HoldingDashingSoul && signalName == "SignalHoldingDashingSoul") PrintAction("DashingSoul");
        else if (inputs.HoldingStart && signalName == "SignalHoldingStart") PrintAction("Start");
        else if (inputs.HoldingSelect && signalName == "SignalHoldingSelect") PrintAction("Select");
    }

    private void PrintAction(string actionName)
    {
        // Extract the device name from the control path
        string deviceName = ExtractDeviceName(inputs.LastControlPath);

        // Print action name to console only if the device name has changed
        if (deviceName != previousDeviceName || LastAction != actionName)
        {
            LastUsedInput = deviceName;
            LastAction = actionName;
            previousDeviceName = deviceName;

            // Invoke the UnityEvent
            onLastUsedInputChanged.Invoke();

            // Debug.Log($"Last input: {LastUsedInput}, Last action: {LastAction}");
        }
    }

    private string ExtractDeviceName(string controlPath)
    {
        // Extract the device name from the control path
        if (string.IsNullOrEmpty(controlPath))
            return "Unknown";

        string[] parts = controlPath.Split('/');
        if (parts.Length > 1)
        {
            string devicePart = parts[1];
            if (devicePart.Contains("Keyboard"))
                return "Keyboard";
            if (devicePart.Contains("Gamepad"))
                return "Gamepad";
            if (devicePart.Contains("DualShock"))
                return "DualShock";
            if (devicePart.Contains("Xbox"))
                return "Xbox";
            // Add other device name checks as needed
            return devicePart;
        }
        return "Unknown";
    }

    public string GetLastAction()
    {
        return LastAction;
    }
}
