using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

[System.Serializable]
public class Option
{
    public string text;
    public UnityEvent onEnter;
}

public class AdjustableButton : MonoBehaviour
{
    private TextMeshProUGUI buttonText;
    public List<Option> options = new List<Option>(); // List of options to populate in the Inspector
    private int currentIndex = 0;
    private RectTransform rectTransform;

    // Shake settings
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 10f;

    // Transition settings
    public float transitionDuration = 0.2f;

    void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        UpdateText();
    }

    public void AdjustValue(bool decrease)
    {
        if (decrease)
        {
            if (currentIndex > 0)
            {
                GameManager.Instance.PlaySound("event:/game/00_ui/ui_changeselection_hi");
                currentIndex--;
                StartCoroutine(TransitionEffect(-1));
                UpdateText();
                options[currentIndex].onEnter?.Invoke();
            }
            else
            {
                StartCoroutine(Shake());
            }
        }
        else
        {
            if (currentIndex < options.Count - 1)
            {
                GameManager.Instance.PlaySound("event:/game/00_ui/ui_changeselection_hi");
                currentIndex++;
                StartCoroutine(TransitionEffect(1));
                UpdateText();
                options[currentIndex].onEnter?.Invoke();
            }
            else
            {
                StartCoroutine(Shake());
            }
        }
    }

    private void UpdateText()
    {
        buttonText.text = ParseColors(options[currentIndex].text);
    }

    private string ParseColors(string input)
    {
        return input.Replace("(", "<").Replace(")", ">");
    }

    private IEnumerator Shake()
    {
        GameManager.Instance.PlaySound("event:/game/00_ui/ui_changeselection_not");
        Vector2 originalPosition = rectTransform.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float xOffset = Random.Range(-shakeMagnitude, shakeMagnitude);
            rectTransform.anchoredPosition = new Vector2(originalPosition.x + xOffset, originalPosition.y);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = originalPosition;
    }

    private IEnumerator TransitionEffect(int direction)
    {
        Vector2 originalPosition = rectTransform.anchoredPosition;
        Vector2 targetPosition = originalPosition + new Vector2(10f * direction, 0); // Move in the direction of the value change

        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(originalPosition, targetPosition, elapsedTime / transitionDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(targetPosition, originalPosition, elapsedTime / transitionDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = originalPosition;
    }
}
