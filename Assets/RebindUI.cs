using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class RebindUI : MonoBehaviour
{
    public string thisPanelControlScheme = "Keyboard"; // or Gamepad
    public TextMeshProUGUI RebindAlertText;
    public Image RebindAlertBackgroundImage;
    public float delayBeforeListening = 0.5f; // Adjust the delay as needed

    private HashSet<Key> forbiddenKeys = new HashSet<Key>
    {
        Key.UpArrow, Key.DownArrow, Key.LeftArrow, Key.RightArrow,
        Key.CapsLock, Key.LeftCtrl, Key.RightCtrl, Key.LeftAlt, Key.RightAlt,
        Key.Delete, Key.LeftWindows, Key.RightWindows, Key.Escape,
        Key.F1, Key.F2, Key.F3, Key.F4, Key.F5, Key.F6,
        Key.F7, Key.F8, Key.F9, Key.F10, Key.F11, Key.F12
    };

    private HashSet<string> forbiddenGamepadButtons = new HashSet<string>
    {
        "start", "select", "dpadup", "dpaddown", "dpadleft", "dpadright",
        "leftstickup", "leftstickdown", "leftstickleft", "leftstickright", "leftstickpress"
    };

    void OnEnable()
    {
        GameState.Instance.OnRebindPanel = true;
    }

    void OnDisable()
    {
        GameState.Instance.OnRebindPanel = false;
    }

    [Button()]
    public void StartListeningInput(string actionToRebind, string controlScheme)
    {
        SetAlpha(RebindAlertBackgroundImage, 1f); // Assuming alpha is between 0 and 1
        RebindAlertText.text = "rebinding " + actionToRebind.ToLower() + ". listening...";

        StartCoroutine(DelayedListenForInput(actionToRebind, controlScheme));
    }

    private IEnumerator DelayedListenForInput(string actionToRebind, string controlScheme)
    {
        // Wait for the specified delay
        yield return new WaitForSecondsRealtime(delayBeforeListening);

        // Start listening for input
        yield return ListenForInput(actionToRebind, controlScheme);
    }

    private IEnumerator ListenForInput(string actionToRebind, string controlScheme)
    {
        while (true)
        {
            if (controlScheme == "Keyboard" && Keyboard.current != null)
            {
                foreach (var key in Keyboard.current.allKeys)
                {
                    if (key.wasPressedThisFrame && !forbiddenKeys.Contains(key.keyCode))
                    {
                        string newBinding = $"<{controlScheme}>/{key.name}";
                        HandleRebinding(actionToRebind, controlScheme, newBinding);
                        EndRebinding();
                        yield break;
                    }
                }
            }

            if (controlScheme == "Gamepad" && Gamepad.current != null)
            {
                foreach (var button in Gamepad.current.allControls)
                {
                    if (button is ButtonControl buttonControl && buttonControl.wasPressedThisFrame && !forbiddenGamepadButtons.Contains(buttonControl.name.ToLower()))
                    {
                        string newBinding = $"<{controlScheme}>/{button.name}";
                        HandleRebinding(actionToRebind, controlScheme, newBinding);
                        EndRebinding();
                        yield break;
                    }
                }
            }

            yield return null;
        }
    }

    private void HandleRebinding(string actionToRebind, string controlScheme, string newBinding)
    {
        // Check if attempted binding exists - if so, swap bindings!
        string actionWithTheWantedBind = Rebind.Instance.FindActionWithBinding(controlScheme, newBinding);
        if (string.IsNullOrEmpty(actionWithTheWantedBind))
        {
            Debug.Log("Simple bind. Just assigning it!");
            // No action has the binding, just bind it!
            Rebind.Instance.RebindAction(controlScheme, actionToRebind, newBinding);
        }
        else
        {
            Debug.Log("SWAPPING BINDS!!!");
            // Find our current binding
            string ourBinding = Rebind.Instance.GetBindingForActionAndScheme(actionToRebind, controlScheme);
            // Assign our current binding to the action with the wanted binding - removing its binding
            Rebind.Instance.RebindAction(controlScheme, actionWithTheWantedBind, ourBinding);
            // Finally, assign the new binding to ourselves
            Rebind.Instance.RebindAction(controlScheme, actionToRebind, newBinding);
        }
    }

    private void EndRebinding()
    {
        SetAlpha(RebindAlertBackgroundImage, 0f);
        RebindAlertText.text = "";
        GameState.Instance.Rebinding = false;
    }

    private void SetAlpha(Graphic graphic, float alpha)
    {
        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
}
