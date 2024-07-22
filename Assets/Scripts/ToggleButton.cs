using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ToggleButton : MonoBehaviour
{
    private TextMeshProUGUI buttonText;
    private string[] toggleOptions = { "OFF", "ON" };
    private int currentIndex = 0;
    public UnityEvent OnToggled; // Add UnityEvent

    void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        UpdateText();
    }

    public void Toggle()
    {
        currentIndex = (currentIndex + 1) % toggleOptions.Length;
        UpdateText();
        OnToggled?.Invoke();
    }

    private void UpdateText()
    {
        buttonText.text = toggleOptions[currentIndex];
    }
}