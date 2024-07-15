using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class WizLightShaker : MonoBehaviour
{
    public float lerpSpeed = 0.5f;
    public float shakeAmount = 0.3f;

    private Light2D light2D;
    public Light2D mbgLight;

    private float targetIntensity;
    private float targetInnerRadius;
    private float targetOuterRadius;

    private bool isLerping = false;
    private bool shakeEffect = false;

    public float DefaultIntensity = 1f;
    public float DefaultInnerRadius = 60f;
    public float DefaultOuterRadius = 60f;

    public static WizLightShaker Instance;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        }

        
    }


    private void Start()
    {
        light2D = GetComponent<Light2D>();
        SetLight(DefaultIntensity, DefaultInnerRadius, DefaultOuterRadius);
    }

    private void Update()
    {
        if (isLerping)
        {
            LerpLightParameters();
        }
    }

    public void ResetLight(){
        targetIntensity = DefaultIntensity;
        targetInnerRadius = DefaultInnerRadius;
        targetOuterRadius = DefaultOuterRadius;
        shakeEffect = false;

        isLerping = true;
    }

    public void SetLight(float intensity, float innerRadius, float outerRadius, bool shake = false, float newLerpSpeed = 0.5f) {
        lerpSpeed = newLerpSpeed;
        targetIntensity = intensity;
        targetInnerRadius = innerRadius;
        targetOuterRadius = outerRadius;
        shakeEffect = shake;

        isLerping = true;
    }

    private void LerpLightParameters()
    {
        light2D.intensity = Mathf.Lerp(light2D.intensity, targetIntensity, lerpSpeed * Time.deltaTime);
        // mbgLight.intensity = Mathf.Lerp(light2D.intensity, targetIntensity, lerpSpeed * Time.deltaTime);
        light2D.pointLightInnerRadius = Mathf.Lerp(light2D.pointLightInnerRadius, targetInnerRadius, lerpSpeed * Time.deltaTime);
        mbgLight.pointLightInnerRadius = Mathf.Lerp(light2D.pointLightInnerRadius, targetInnerRadius, lerpSpeed * Time.deltaTime);

        float shakeOffset = shakeEffect ? Random.Range(-shakeAmount, shakeAmount) : 0f;
        light2D.pointLightOuterRadius = Mathf.Lerp(light2D.pointLightOuterRadius, targetOuterRadius + shakeOffset, lerpSpeed * Time.deltaTime);
        // mbgLight.pointLightOuterRadius = Mathf.Lerp(mbgLight.pointLightOuterRadius, targetOuterRadius + shakeOffset, lerpSpeed * Time.deltaTime);

        // If the difference between the target parameters and the current parameters is small enough, stop lerping.
        if (Mathf.Abs(light2D.intensity - targetIntensity) < 0.01f && Mathf.Abs(light2D.pointLightInnerRadius - targetInnerRadius) < 0.01f && Mathf.Abs(light2D.pointLightOuterRadius - targetOuterRadius) < 0.01f)
        {
            isLerping = false;
        }
    }
}
