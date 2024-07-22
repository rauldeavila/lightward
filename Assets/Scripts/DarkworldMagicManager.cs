using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkworldMagicManager : MonoBehaviour
{
    private Coroutine adjustMagicCoroutine;
    private bool isMagicChangedExternally = false;

    private void Start() {
        PlayerStats.Instance.OnMagicChanged.AddListener(HandleMagicChanged);
        // Start monitoring the game state
        StartCoroutine(MonitorGameState());
    }

    private void HandleMagicChanged() {
        isMagicChangedExternally = true;
        if (adjustMagicCoroutine != null) {
            StopCoroutine(adjustMagicCoroutine);
            adjustMagicCoroutine = null;
        }
    }

    private IEnumerator MonitorGameState() {
        while (true) {
            if(GameState.Instance.Darkworld)
            {
                if (GameState.Instance.AtSafeZone) {
                    if (adjustMagicCoroutine != null) {
                        StopCoroutine(adjustMagicCoroutine);
                        adjustMagicCoroutine = null;
                    }
                    adjustMagicCoroutine = StartCoroutine(IncreaseMagic());
                } else {
                    if (adjustMagicCoroutine != null) {
                        StopCoroutine(adjustMagicCoroutine);
                        adjustMagicCoroutine = null;
                    }
                    adjustMagicCoroutine = StartCoroutine(DecreaseMagic());
                }
            }
            yield return null;
        }
    }
    private IEnumerator IncreaseMagic() {
        FloatValue heroMagic = ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("hero_magic");
        float currentMagic = heroMagic.runTimeValue;
        float maxMagic = heroMagic.maxValue;
        float duration = 1f; // Duration of the increase
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Quadratic Ease-Out function (starts slow, accelerates)
            t = t * (2f - t);

            heroMagic.runTimeValue = Mathf.Lerp(currentMagic, maxMagic, t);
            Magic.Instance.UpdateMagic();
            yield return null;
        }

        heroMagic.runTimeValue = maxMagic; // Ensure the value is set to max at the end
        adjustMagicCoroutine = null;
    }

    private IEnumerator DecreaseMagic() {
        FloatValue heroMagic = ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("hero_magic");
        float currentMagic = heroMagic.runTimeValue;
        float minMagic = 0f; // Assuming the minimum value is 0
        float duration = 2f; // Duration of the decrease
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            t = t * t * (3f - 2f * t); // Ease-in-out function
            heroMagic.runTimeValue = Mathf.Lerp(currentMagic, minMagic, t);
            Magic.Instance.UpdateMagic();
            yield return null;
        }

        heroMagic.runTimeValue = minMagic; // Ensure the value is set to min at the end
        adjustMagicCoroutine = null;
    }
}