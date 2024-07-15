using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizEyesController : MonoBehaviour 
{

    public Color ColorYellow;
    public Color ColorRed;
    public Color ColorBlue;
    public Color ColorGreen;
    public Color ColorWhite;
    public Color ColorBlack;
    private SpriteRenderer _sr;
    private Color startColor;
    private Color targetColor;
    private float tweenDuration = 2f; // Duration in seconds for the color tween

    private Coroutine colorTweenCoroutine; // Reference to the active color tween coroutine

    public static WizEyesController Instance;

    private Material _defaultMaterial;
    private Material _brighterMaterial;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
        _sr = GetComponent<SpriteRenderer>();
    }

    void Start(){
        _defaultMaterial = Resources.Load<Material>("Materials/Glow/eyes_default");
        _brighterMaterial = Resources.Load<Material>("Materials/Glow/eyes_brighter");
        switch (ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("wiz_cloak").runTimeValue)
        {
            case "default":
                SetEyesToDefault();
                break;
            case "blue":
                SetEyesToBlue();
                break;
            case "red":
                SetEyesToRed();
                break;
            case "black":
                switch (ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("wiz_sword").runTimeValue)
                {
                    case "default":
                        SetEyesToDefault();
                        break;
                    case "ancient":
                        SetEyesToGreen();
                        break;
                    case "master":
                        SetEyesToBlue();
                        break;
                    case "black":
                        SetEyesToRed();
                        break;
                    default:
                        break;
                }    
                break;
            default:
                break;
        }
    }

    public void SetEyesToDefault()
    {
        ChangeEyesColor(ColorWhite);
        _sr.material = _defaultMaterial;
    }
    public void SetEyesToGreen()
    {
        ChangeEyesColor(ColorGreen);
        _sr.material = _brighterMaterial;
    }
    public void SetEyesToBlue()
    {
        ChangeEyesColor(ColorBlue);
        _sr.material = _brighterMaterial;
    }
    public void SetEyesToRed()
    {
        ChangeEyesColor(ColorRed);
        _sr.material = _brighterMaterial;
    }

    public void SetEyesToBlack()
    {
        ChangeEyesColor(ColorBlack);
        _sr.material = _brighterMaterial;
    }


    public void ChangeEyesColor(Color color)
    {
        startColor = _sr.color;
        targetColor = color;
        StopColorTween();
        if(color == ColorYellow)
        {
            colorTweenCoroutine = StartCoroutine(TweenColor(true));
        }
        else
        {
            colorTweenCoroutine = StartCoroutine(TweenColor());
        }
    }

    public void SetEyesToYellow()
    {
        ChangeEyesColor(ColorYellow);
    }

    private IEnumerator TweenColor(bool yellow = false){
        float elapsedTime = 0f;
        while (elapsedTime < 0.1f){
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / tweenDuration;
            _sr.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        // Ensure the final color is set correctly
        _sr.color = targetColor;

        // Wait for a second before tweening back to white
        yield return new WaitForSeconds(1f);
        if(!yellow)
        {
            yield break;
        }

        // Tween back to original color
        Color originalColor = startColor;
        startColor = targetColor;
        targetColor = originalColor;
        elapsedTime = 0f;
        while (elapsedTime < tweenDuration){
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / tweenDuration;
            _sr.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }
        _sr.color = targetColor;
    }

    private void StopColorTween(){
        if (colorTweenCoroutine != null){
            StopCoroutine(colorTweenCoroutine);
            colorTweenCoroutine = null;
        }
    }
}
