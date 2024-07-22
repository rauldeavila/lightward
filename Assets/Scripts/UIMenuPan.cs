using System.Collections;
using UnityEngine;

public class UIMenuPan : MonoBehaviour
{
    public float panDuration = 1f; // Time in seconds for the pan animation

    private RectTransform rectTransform;
    private Coroutine panCoroutine;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void PanTo(float targetValue)
    {
        if (panCoroutine != null)
        {
            StopCoroutine(panCoroutine);
        }
        panCoroutine = StartCoroutine(PanCoroutine(targetValue));
    }

    private IEnumerator PanCoroutine(float targetValue)
    {
        Vector2 startPosition = rectTransform.anchoredPosition;
        Vector2 endPosition = new Vector2(startPosition.x, targetValue);
        float elapsedTime = 0f;

        while (elapsedTime < panDuration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, elapsedTime / panDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = endPosition;
        panCoroutine = null;
    }
}
