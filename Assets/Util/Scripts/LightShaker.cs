using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightShaker : MonoBehaviour {

    public float falloffShakeInterval = 1.5f;
    public float falloffShakeAmount = 0.3f;
    public float lerpSpeed = 0.5f;

    private float falloffShakeTimer = 0f;
    private Vector3[] targetShapePath;

    private float baseInnerRadius;
    private float baseOuterRadius;
    private Vector3[] baseShapePath;
    private Light2D light2D;

    private void Start()
    {
        light2D = GetComponent<Light2D>();
        if (light2D.lightType == Light2D.LightType.Point) {
            baseInnerRadius = light2D.pointLightInnerRadius;
            baseOuterRadius = light2D.pointLightOuterRadius;
        } else {
            baseShapePath = (Vector3[])light2D.shapePath.Clone();
            targetShapePath = (Vector3[])baseShapePath.Clone();
        }
    }

    private void Update() {
        UpdateFalloffShake();
    }


    private void UpdateFalloffShake()
    {
        falloffShakeTimer += Time.deltaTime;
        if (falloffShakeTimer >= falloffShakeInterval)
        {
            falloffShakeTimer = 0f;

            if (light2D.lightType == Light2D.LightType.Point)
            {
                float innerRadiusOffset = Random.Range(-falloffShakeAmount, falloffShakeAmount);
                float outerRadiusOffset = Random.Range(-falloffShakeAmount, falloffShakeAmount);
                light2D.pointLightInnerRadius = baseInnerRadius + innerRadiusOffset;
                light2D.pointLightOuterRadius = baseOuterRadius + outerRadiusOffset;
            }
            else
            {
                for (int i = 0; i < baseShapePath.Length; i++)
                {
                    float offsetX = Random.Range(-falloffShakeAmount, falloffShakeAmount);
                    float offsetY = Random.Range(-falloffShakeAmount, falloffShakeAmount);
                    targetShapePath[i] = baseShapePath[i] + new Vector3(offsetX, offsetY, 0);
                }
            }
        }

        if (light2D.lightType != Light2D.LightType.Point)
        {
            Vector3[] currentShapePath = new Vector3[baseShapePath.Length];
            for (int i = 0; i < baseShapePath.Length; i++)
            {
                currentShapePath[i] = Vector3.Lerp(light2D.shapePath[i], targetShapePath[i], lerpSpeed * Time.deltaTime);
            }
            light2D.SetShapePath(currentShapePath);
        }
    }
}
