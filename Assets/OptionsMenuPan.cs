using System.Collections;
using UnityEngine;

public class OptionsMenuPan : MonoBehaviour
{
    public float initialValue = 0f;
    public float finalValue = 526f;
    public float panDuration = 1f; // Time in seconds for the pan animation

    private RectTransform rectTransform;
    private Coroutine panCoroutine;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void PanToInitial()
    {
        if (panCoroutine != null)
        {
            StopCoroutine(panCoroutine);
        }
        panCoroutine = StartCoroutine(PanCoroutine(initialValue));
    }

    public void PanToFinal()
    {
        if (panCoroutine != null)
        {
            StopCoroutine(panCoroutine);
        }
        panCoroutine = StartCoroutine(PanCoroutine(finalValue));
    }

    private IEnumerator PanCoroutine(float targetValue)
    {
        Vector2 startPosition = rectTransform.anchoredPosition;
        Vector2 endPosition = new Vector2(startPosition.x, targetValue);
        float elapsedTime = 0f;

        while (elapsedTime < panDuration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, elapsedTime / panDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = endPosition;
        panCoroutine = null;
    }
}