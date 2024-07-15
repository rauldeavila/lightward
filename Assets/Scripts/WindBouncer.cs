using System.Collections;
using UnityEngine;

public class WindBouncer : MonoBehaviour
{
    public bool IsWindy = false;
    public float WindStrength = 30f;
    public float LerpDuration = 1f;
    public float ZeroWindDuration = 0f;
    public float PositiveWindDuration = 1f;
    public float NegativeWindDuration = 1f;

    private WindController windController;

    void Start()
    {
        windController = WindController.Instance;
        if (IsWindy)
        {
            StartCoroutine(BounceWind());
        }
    }

    public void StartBouncing()
    {
        IsWindy = true;
        StartCoroutine(BounceWind());
    }

    public void StopBouncing()
    {
        IsWindy = false;
        StopAllCoroutines();
    }

    private IEnumerator BounceWind()
    {
        while (IsWindy)
        {
            // Lerp to negative wind strength
            windController.SetWindStrength(-WindStrength, LerpDuration);
            yield return new WaitForSeconds(LerpDuration);

            // Stay at negative wind strength
            yield return new WaitForSeconds(NegativeWindDuration);

            // Optionally pause at zero wind strength
            if (ZeroWindDuration > 0f)
            {
                windController.SetWindStrength(0f, LerpDuration);
                yield return new WaitForSeconds(LerpDuration);
                yield return new WaitForSeconds(ZeroWindDuration);
            }

            // Lerp to positive wind strength
            windController.SetWindStrength(WindStrength, LerpDuration);
            yield return new WaitForSeconds(LerpDuration);

            // Stay at positive wind strength
            yield return new WaitForSeconds(PositiveWindDuration);

            // Optionally pause at zero wind strength
            if (ZeroWindDuration > 0f)
            {
                windController.SetWindStrength(0f, LerpDuration);
                yield return new WaitForSeconds(LerpDuration);
                yield return new WaitForSeconds(ZeroWindDuration);
            }
        }
    }
}
