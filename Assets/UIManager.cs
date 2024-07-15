using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;  // Add this to use Keyboard and Gamepad

public class UIManager : MonoBehaviour
{
    public Button firstButton;
    public float FontImpactValue = 20f;
    public float transitionDuration = 0.1f; // Duration of the transition
    public Color[] animationColors = { new Color(1.0f, 0.613f, 0.212f, 1f), new Color(1f, 0.250f, 0.412f, 1f) };
    private Button[] buttons;
    private int currentIndex = 0;
    private Vector2[] originalPositions;
    private bool buttonMoved = false;
    private bool _waitForButtonReset = false;
    private bool _waitForHorizontalReset = false;

    public bool AllowBackKey = false;
    public UnityEvent onBackKeyPressed;

    void Start()
    {
        InitializeButtons();
        SelectButton(Array.IndexOf(buttons, firstButton));
    }

    void OnEnable()
    {
        if(Inputs.Instance != null)
        {
            if (Inputs.Instance.HoldingJump)
            {
                _waitForButtonReset = true;
            }
        }
        InitializeButtons();
        SelectButton(Array.IndexOf(buttons, firstButton));
    }

    void InitializeButtons()
    {
        buttons = GetComponentsInChildren<Button>();
        originalPositions = new Vector2[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            originalPositions[i] = buttons[i].GetComponent<RectTransform>().anchoredPosition;
        }
    }

    void Update()
    {
        HandleInput();

        // Update the selected button's blink animation
        for (int i = 0; i < buttons.Length; i++)
        {
            AnimatedButton animatedButton = buttons[i].GetComponent<AnimatedButton>();
            if (animatedButton != null)
            {
                animatedButton.SetSelected(i == currentIndex);
            }
        }

        // Handle back key input
        if (AllowBackKey)
        {
            HandleBackKey();
        }
    }

    void HandleInput()
    {
        if(!GameState.Instance.Rebinding)
        {
            if (!buttonMoved && Inputs.Instance.HoldingUpArrow)
            {
                int nextIndex = (currentIndex - 1 + buttons.Length) % buttons.Length;
                SelectButton(nextIndex);
                buttonMoved = true;
            }
            else if (!buttonMoved && Inputs.Instance.HoldingDownArrow)
            {
                int nextIndex = (currentIndex + 1) % buttons.Length;
                SelectButton(nextIndex);
                buttonMoved = true;
            }
            else if (!Inputs.Instance.HoldingUpArrow && !Inputs.Instance.HoldingDownArrow)
            {
                buttonMoved = false;
            }

            AnimatedButton selectedButton = buttons[currentIndex].GetComponent<AnimatedButton>();
            if (selectedButton != null)
            {
                if (selectedButton.isRebindingMenu)
                {
                    if (Keyboard.current.escapeKey.wasPressedThisFrame || (Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame))
                    {
                        if (_waitForButtonReset == false)
                        {
                            PressSelectedButton();
                        }
                    }
                    else
                    {
                        if (_waitForButtonReset)
                        {
                            _waitForButtonReset = false;
                        }
                    }
                }
                else
                {
                    if (Inputs.Instance.HoldingJump)
                    {
                        if (_waitForButtonReset == false)
                        {
                            PressSelectedButton();
                        }
                    }
                    else
                    {
                        if (_waitForButtonReset)
                        {
                            _waitForButtonReset = false;
                        }
                    }
                }
            }

            if (Inputs.Instance.HoldingLeftArrow || Inputs.Instance.HoldingRightArrow)
            {
                if (_waitForHorizontalReset == false)
                {
                    AdjustSelectedButtonValue(Inputs.Instance.HoldingLeftArrow);
                    _waitForHorizontalReset = true;
                }
            }
            else
            {
                if (_waitForHorizontalReset)
                {
                    _waitForHorizontalReset = false;
                }
            }
        }
    }

    void HandleBackKey()
    {
        if (!GameState.Instance.Rebinding)
        {
            if (Keyboard.current.backspaceKey.wasPressedThisFrame || (Gamepad.current != null && Gamepad.current.buttonNorth.wasPressedThisFrame))
            {
                // Fire the back key pressed event
                onBackKeyPressed.Invoke();
                print("RETURN!!!");
            }
        }
    }

    public void SelectButton(int index)
    {
        if (currentIndex != index)
        {
            int previousIndex = currentIndex;
            currentIndex = index;

            // Ensure all buttons are updated correctly
            for (int i = 0; i < buttons.Length; i++)
            {
                AnimatedButton animatedButton = buttons[i].GetComponent<AnimatedButton>();
                if (animatedButton != null)
                {
                    animatedButton.SetSelected(i == currentIndex);
                }
            }

            StartCoroutine(AnimateButtonImpact(previousIndex, currentIndex));
        }
    }

    private IEnumerator AnimateButtonImpact(int previousIndex, int currentIndex)
    {
        RectTransform currentRectTransform = buttons[currentIndex].GetComponent<RectTransform>();
        Vector2 originalPosition = originalPositions[currentIndex];
        Vector2 targetPosition = originalPosition;

        if (currentIndex < previousIndex)
        {
            targetPosition.y += FontImpactValue;
        }
        else
        {
            targetPosition.y -= FontImpactValue;
        }

        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            currentRectTransform.anchoredPosition = Vector2.Lerp(originalPosition, targetPosition, elapsedTime / transitionDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        // Ensure it reaches the final position
        currentRectTransform.anchoredPosition = targetPosition;

        // Return to original position
        elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            currentRectTransform.anchoredPosition = Vector2.Lerp(targetPosition, originalPosition, elapsedTime / transitionDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        currentRectTransform.anchoredPosition = originalPosition;
    }

    public void PressSelectedButton()
    {
        AnimatedButton selectedButton = buttons[currentIndex].GetComponent<AnimatedButton>();
        if (selectedButton != null)
        {
            selectedButton.PressButton();
        }
    }

    public void AdjustSelectedButtonValue(bool decrease)
    {
        AdjustableButton adjustableButton = buttons[currentIndex].GetComponent<AdjustableButton>();
        if (adjustableButton != null)
        {
            adjustableButton.AdjustValue(decrease);
        }
    }
}
