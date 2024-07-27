using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering.Universal;

public class DarkworldLight : MonoBehaviour
{
    public bool BounceFallOff2D = true;
    [ShowIf("BounceFallOff2D")]
    public float MinValue = 0.2f;
    [ShowIf("BounceFallOff2D")]
    public float MaxValue = 0.6f;
    [ShowIf("BounceFallOff2D")]
    public float BounceDuration = 1f;
    [ShowIf("BounceFallOff2D")]
    public float StartDelay = 0f;

    private Light2D light2D;

    private void Start()
    {
        light2D = GetComponent<Light2D>();

        if (BounceFallOff2D)
        {
            StartCoroutine(BounceFallOff());
        }
    }

    private IEnumerator BounceFallOff()
    {
        
        yield return new WaitForSeconds(StartDelay);

        while (true)
        {
            float elapsedTime = 0f;
            while (elapsedTime < BounceDuration)
            {
                light2D.falloffIntensity = Mathf.Lerp(MinValue, MaxValue, (elapsedTime / BounceDuration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            elapsedTime = 0f;
            while (elapsedTime < BounceDuration)
            {
                light2D.falloffIntensity = Mathf.Lerp(MaxValue, MinValue, (elapsedTime / BounceDuration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("WizHitBox"))
        {
            GameState.Instance.AtSafeZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("WizHitBox"))
        {
            GameState.Instance.AtSafeZone = false;
        }
    }
}
