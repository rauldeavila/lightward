using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DarkworldMagicManager : MonoBehaviour
{
    public UnityEvent OnDarkworldConsumingMagicStarted;
    public UnityEvent OnDarkworldConsumingMagicEnded;
    public UnityEvent OnDarkworldConsumingHealthStarted;
    public UnityEvent OnDarkworldConsumingHealthEnded;

    private Coroutine adjustMagicCoroutine;
    private Coroutine adjustHealthCoroutine;
    private float decreaseInterval = 0.5f; // Interval for decreasing magic and health
    private float decreaseAmount = 1f; // Amount to decrease each interval

    public static DarkworldMagicManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }

    private void Start() {
        PlayerStats.Instance.OnMagicChanged.AddListener(HandleMagicChanged);
        GameManager.Instance.OnEnterDarkworld.AddListener(HandleEnterDarkworld);
        GameManager.Instance.OnExitDarkworld.AddListener(HandleExitDarkworld);
        GameState.Instance.OnEnterSafeZone.AddListener(HandleEnterSafeZone);
        GameState.Instance.OnExitSafeZone.AddListener(HandleExitSafeZone);
    }

    private void HandleMagicChanged() {
        if (adjustMagicCoroutine != null) {
            StopCoroutine(adjustMagicCoroutine);
            adjustMagicCoroutine = null;
            OnDarkworldConsumingMagicEnded?.Invoke();
        }
        
        if (adjustHealthCoroutine != null) {
            StopCoroutine(adjustHealthCoroutine);
            adjustHealthCoroutine = null;
            OnDarkworldConsumingHealthEnded?.Invoke();
        }

        // Determine which coroutine to restart
        if (GameState.Instance.Darkworld) {
            if (GameState.Instance.AtSafeZone) {
                adjustMagicCoroutine = StartCoroutine(IncreaseMagic());
            } else {
                FloatValue heroMagic = ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("hero_magic");
                if (heroMagic.runTimeValue > 0) {
                    adjustMagicCoroutine = StartCoroutine(DecreaseMagic());
                } else {
                    adjustHealthCoroutine = StartCoroutine(DecreaseHealth());
                }
            }
        }
    }

    private void HandleEnterDarkworld() {
        if (GameState.Instance.AtSafeZone) {
            HandleEnterSafeZone();
        } else {
            HandleExitSafeZone();
        }
    }

    private void HandleExitDarkworld() {
        if (adjustMagicCoroutine != null) {
            StopCoroutine(adjustMagicCoroutine);
            adjustMagicCoroutine = null;
            OnDarkworldConsumingMagicEnded?.Invoke();
        }
        if (adjustHealthCoroutine != null) {
            StopCoroutine(adjustHealthCoroutine);
            adjustHealthCoroutine = null;
            OnDarkworldConsumingHealthEnded?.Invoke();
        }
    }

    private void HandleEnterSafeZone() {
        if (adjustMagicCoroutine != null) {
            StopCoroutine(adjustMagicCoroutine);
            adjustMagicCoroutine = null;
            OnDarkworldConsumingMagicEnded?.Invoke();
        }
        if (adjustHealthCoroutine != null) {
            StopCoroutine(adjustHealthCoroutine);
            adjustHealthCoroutine = null;
            OnDarkworldConsumingHealthEnded?.Invoke();
        }
        adjustMagicCoroutine = StartCoroutine(IncreaseMagic());
    }

    private void HandleExitSafeZone() {
        if (adjustMagicCoroutine != null) {
            StopCoroutine(adjustMagicCoroutine);
            adjustMagicCoroutine = null;
            OnDarkworldConsumingMagicEnded?.Invoke();
        }
        if (adjustHealthCoroutine != null) {
            StopCoroutine(adjustHealthCoroutine);
            adjustHealthCoroutine = null;
            OnDarkworldConsumingHealthEnded?.Invoke();
        }
        adjustMagicCoroutine = StartCoroutine(DecreaseMagic());
    }

    private IEnumerator IncreaseMagic() {
        if (!GameState.Instance.Darkworld) yield break;

        FloatValue heroMagic = ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("hero_magic");
        float currentMagic = heroMagic.runTimeValue;
        float maxMagic = heroMagic.maxValue;

        while (currentMagic < maxMagic) {
            currentMagic += decreaseAmount;
            heroMagic.runTimeValue = Mathf.Min(currentMagic, maxMagic);
            Magic.Instance.UpdateMagic();
            yield return new WaitForSeconds(decreaseInterval);
        }

        heroMagic.runTimeValue = maxMagic; // Ensure the value is set to max at the end
        adjustMagicCoroutine = null;
        OnDarkworldConsumingMagicEnded?.Invoke();
    }

    private IEnumerator DecreaseMagic() {
        if (!GameState.Instance.Darkworld) yield break;

        OnDarkworldConsumingMagicStarted?.Invoke();

        FloatValue heroMagic = ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("hero_magic");
        float currentMagic = heroMagic.runTimeValue;
        float minMagic = 0f; // Assuming the minimum value is 0

        while (currentMagic > minMagic) {
            currentMagic -= decreaseAmount;
            heroMagic.runTimeValue = Mathf.Max(currentMagic, minMagic);
            Magic.Instance.UpdateMagic();
            yield return new WaitForSeconds(decreaseInterval);
        }

        heroMagic.runTimeValue = minMagic; // Ensure the value is set to min at the end
        adjustMagicCoroutine = null;
        OnDarkworldConsumingMagicEnded?.Invoke();

        // Start draining health when magic is zeroed
        if (heroMagic.runTimeValue == minMagic) {
            if (adjustHealthCoroutine == null) {
                adjustHealthCoroutine = StartCoroutine(DecreaseHealth());
            }
        }
    }

    private IEnumerator DecreaseHealth() {
        if (!GameState.Instance.Darkworld) yield break;

        OnDarkworldConsumingHealthStarted?.Invoke();

        FloatValue heroHealth = ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("hero_health");
        FloatValue heroYellowHealth = ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("hero_health_yellow");
        FloatValue heroMagic = ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("hero_magic");
        float currentHealth = heroHealth.runTimeValue;
        float currentYellowHealth = heroYellowHealth.runTimeValue;
        float minHealth = 0f; // Assuming the minimum value is 0

        while (currentHealth > minHealth || currentYellowHealth > minHealth) {
            // Check if magic is not zero anymore
            if (heroMagic.runTimeValue > 0) {
                if (adjustMagicCoroutine == null) {
                    adjustMagicCoroutine = StartCoroutine(DecreaseMagic());
                }
                OnDarkworldConsumingHealthEnded?.Invoke();
                yield break; // Stop draining health
            }

            if (currentYellowHealth > minHealth) {
                currentYellowHealth -= decreaseAmount;
                heroYellowHealth.runTimeValue = Mathf.Max(currentYellowHealth, minHealth);
            } else {
                currentHealth -= decreaseAmount;
                heroHealth.runTimeValue = Mathf.Max(currentHealth, minHealth);
            }

            Health.Instance.UpdateHealth(); // Assuming a similar UpdateHealth method exists
            yield return new WaitForSeconds(decreaseInterval);
        }

        heroHealth.runTimeValue = minHealth; // Ensure the value is set to min at the end
        adjustHealthCoroutine = null;
        OnDarkworldConsumingHealthEnded?.Invoke();
    }
}
