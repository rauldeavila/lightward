using System.Collections;
using UnityEngine;

public class BreatheEffectScale : MonoBehaviour
{
    public Transform TransformToApplyEffect;
    public float MinScaleValue = 0.6f;
    public float MaxScaleValue = 0.8f;
    public float Time = 1f;
    public float AllowedVariation = 0.2f;
    public float NewValuesAfterTime = 2f;
    public float NewMinScaleValue = 1.1f;
    public float NewMaxScaleValue = 1.3f;

    private float _currentMinScale;
    private float _currentMaxScale;
    private Coroutine breatheCoroutine;

    private void Start()
    {
        _currentMinScale = MinScaleValue;
        _currentMaxScale = MaxScaleValue;
        breatheCoroutine = StartCoroutine(Breathe());
        StartCoroutine(UpdateScaleRange());
        GameState.Instance.OnEnterSafeZone.AddListener(HandleEnterSafeZone);
        GameState.Instance.OnExitSafeZone.AddListener(HandleExitSafeZone);
    }

    private void OnEnable()
    {
        _currentMinScale = MinScaleValue;
        _currentMaxScale = MaxScaleValue;
        if (GameState.Instance.AtSafeZone)
        {
            HandleEnterSafeZone();
        }
        else
        {
            breatheCoroutine = StartCoroutine(Breathe());
        }
    }

    private void OnDisable()
    {
        if (breatheCoroutine != null)
        {
            _currentMinScale = MinScaleValue;
            _currentMaxScale = MaxScaleValue;
            StopCoroutine(breatheCoroutine);
            breatheCoroutine = null;
        }
    }

    private void HandleEnterSafeZone()
    {
        if (breatheCoroutine != null)
        {
            StopCoroutine(breatheCoroutine);
        }
        StartCoroutine(ScaleToSafeZone());
        TransformToApplyEffect.gameObject.SetActive(false);
    }

    private void HandleExitSafeZone()
    {
        TransformToApplyEffect.gameObject.SetActive(true);
        breatheCoroutine = StartCoroutine(Breathe());
    }

    private IEnumerator Breathe()
    {
        while (true)
        {
            float randomScale = Random.Range(_currentMinScale - AllowedVariation, _currentMaxScale + AllowedVariation);
            randomScale = Mathf.Clamp(randomScale, _currentMinScale, _currentMaxScale);

            Vector3 targetScale = new Vector3(randomScale, randomScale, randomScale);
            Vector3 initialScale = TransformToApplyEffect.localScale;
            float elapsedTime = 0f;

            while (elapsedTime < Time)
            {
                TransformToApplyEffect.localScale = Vector3.Lerp(initialScale, targetScale, (elapsedTime / Time));
                elapsedTime += UnityEngine.Time.deltaTime;
                yield return null;
            }

            TransformToApplyEffect.localScale = targetScale;
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f)); // Adding a random wait time to vary the effect
        }
    }

    private IEnumerator ScaleToSafeZone()
    {
        Vector3 targetScale = new Vector3(1.2f, 1.2f, 1.2f);
        Vector3 initialScale = TransformToApplyEffect.localScale;
        float elapsedTime = 0f;
        float duration = 0.5f;

        while (elapsedTime < duration)
        {
            TransformToApplyEffect.localScale = Vector3.Lerp(initialScale, targetScale, (elapsedTime / duration));
            elapsedTime += UnityEngine.Time.deltaTime;
            yield return null;
        }

        TransformToApplyEffect.localScale = targetScale;
    }

    private IEnumerator UpdateScaleRange()
    {
        yield return new WaitForSeconds(NewValuesAfterTime);
        _currentMinScale = NewMinScaleValue;
        _currentMaxScale = NewMaxScaleValue;
    }

    private void OnDestroy()
    {
        GameState.Instance.OnEnterSafeZone.RemoveListener(HandleEnterSafeZone);
        GameState.Instance.OnExitSafeZone.RemoveListener(HandleExitSafeZone);
    }
}
