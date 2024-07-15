using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class AnimatedButton : MonoBehaviour
{
    private TextMeshProUGUI buttonText;
    private Color[] animationColors;
    private Color originalColor;
    private float blinkSpeed = 0.25f;
    private bool isSelected = false;
    private float timer = 0f;
    public UnityEvent OnPressedButton; // UnityEvent for button press
    public UnityEvent OnValueChanged; // UnityEvent for value change
    public UnityEvent OnEnter; // UnityEvent for entering the button

    public bool isRebindingMenu = false; // this changes the key for OnPressed event that triggers the rebinding

    void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        animationColors = GetComponentInParent<UIManager>().animationColors;
        originalColor = buttonText.color;
    }

    void Update()
    {
        if (isSelected)
        {
            timer += Time.unscaledDeltaTime;
            float t = Mathf.PingPong(timer / blinkSpeed, 1f);
            buttonText.color = Color.Lerp(animationColors[0], animationColors[1], t);

            // Explicitly handle input for rebinding menu
            if (isRebindingMenu)
            {
                if (Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    // Debug.Log("Pressed button with ESC");
                    PressButton();
                }
                else if (Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame)
                {
                    // Debug.Log("Pressed button with START");
                    PressButton();
                }
            }
            else
            {
                var jumpBinding = Rebind.Instance.GetCurrentBindingControl("Jump");
                if (jumpBinding is ButtonControl buttonControl && buttonControl.wasPressedThisFrame)
                {
                    // Debug.Log("Pressed button with Jump");
                    PressButton();
                }
            }
        }
        else
        {
            buttonText.color = originalColor; // Reset to original color when deselected
        }
    }

    public void SetSelected(bool selected)
    {
        if (isSelected != selected)
        {
            GameManager.Instance.PlaySound("event:/game/00_ui/ui_changeselection");
            isSelected = selected;
            timer = 0f; // Reset timer when button is selected

            if (isSelected)
            {
                OnEnter?.Invoke(); // Invoke the OnEnter event when the button is selected
            }
        }
    }

    public void PressButton()
    {
        // Debug.Log("Button pressed");
        OnPressedButton?.Invoke(); // Invoke the event when the button is pressed
    }

    public void AdjustValue(bool decrease)
    {
        AdjustableButton adjustableButton = GetComponent<AdjustableButton>();
        if (adjustableButton != null)
        {
            adjustableButton.AdjustValue(decrease);
        }
        else
        {
            ToggleButton toggleButton = GetComponent<ToggleButton>();
            if (toggleButton != null)
            {
                toggleButton.Toggle();
            }
        }
        OnValueChanged?.Invoke(); // Invoke the event when the value is changed
    }
}
