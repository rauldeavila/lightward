using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

public class WindController : MonoBehaviour
{
    public static WindController Instance { get; private set; }
    public float windStrength = 0f;
    public bool IsWindLerping { get; private set; }
    public float WindStrength => windStrength;

    public delegate void WindStrengthChanged(float newStrength);
    public event WindStrengthChanged OnWindStrengthChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [Button("New Wind")]
    public void SetWindStrength(float newStrength, float duration)
    {
        StopAllCoroutines(); // Stop any existing wind strength change coroutine
        StartCoroutine(LerpWindStrength(newStrength, duration));
    }

    private IEnumerator LerpWindStrength(float newStrength, float duration)
    {
        float initialStrength = windStrength;
        float elapsedTime = 0f;
        IsWindLerping = true;

        while (elapsedTime < duration)
        {
            windStrength = Mathf.Lerp(initialStrength, newStrength, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            OnWindStrengthChanged?.Invoke(windStrength); // Trigger event
            yield return null;
        }

        windStrength = newStrength; // Ensure it ends exactly at the target strength
        IsWindLerping = false;
        OnWindStrengthChanged?.Invoke(windStrength); // Trigger event
    }
}