using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepImageActionUpToDate : MonoBehaviour
{
    public string ActionName = "";
    public string ControlScheme = "";
    private Image my_image;

    void Start()
    {
        my_image = GetComponent<Image>();
        UpdateImage();
    }

    void Update()
    {
        UpdateImage();
    }

    private void UpdateImage()
    {
        my_image.sprite = GetSpriteForAction(ActionName, ControlScheme);
    }

    private Sprite GetSpriteForAction(string actionName, string controlScheme, bool forceKeyboard = false)
    {
        string binding;
        if (forceKeyboard || controlScheme == "Keyboard")
        {
            binding = Rebind.Instance.GetBindingForActionAndScheme(actionName, "Keyboard");
        }
        else
        {
            binding = Rebind.Instance.GetBindingForActionAndScheme(actionName, "Gamepad");
        }

        // Determine the correct resource name based on the control scheme for displaying icons
        string resourceName;
        if (forceKeyboard || controlScheme == "Keyboard")
        {
            resourceName = binding.Replace("<Keyboard>/", "keyboard_");
        }
        else
        {
            // Assuming you have a method to get the last used gamepad type
            string gamepadType = InputListener.Instance.LastUsedInput;
            resourceName = binding.Replace("<Gamepad>/", GetDevicePrefix(gamepadType));
        }

        resourceName = resourceName.Replace("/", "_");

        Sprite sprite = Resources.Load<Sprite>($"Inputs/{resourceName}_0");
        if (sprite == null)
        {
            // Fallback to a generic gamepad icon if specific icon not found
            sprite = Resources.Load<Sprite>($"Inputs/gamepad_{actionName}_0");
        }

        if (sprite == null)
        {
            Debug.LogError($"No sprite found for the given resource name: {resourceName}_0");
        }

        return sprite;
    }

    private string GetDevicePrefix(string deviceName)
    {
        switch (deviceName)
        {
            case "DualShock":
                return "dualshock_";
            case "Xbox":
                return "xbox_";
            case "Gamepad":
                return "gamepad_";
            default:
                return "gamepad_"; // Default to generic gamepad if no match
        }
    }
}
