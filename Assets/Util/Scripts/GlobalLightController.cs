using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightController : MonoBehaviour {
    
    // private Light2D _globalLight;
    
    // public static GlobalLightController Instance;
    // public float DefaultIntensity = 0.55f;
    // public float fadeDuration = 1.0f;
    // public float tempLight = 0f;

    // void Awake(){
    //     if (Instance != null && Instance != this){ 
    //         Destroy(this); 
    //     } else { 
    //         Instance = this; 
    //     } 
    //     _globalLight = GetComponent<Light2D>();
    // }

    // public float GetCurrentLight(){
    //     return _globalLight.intensity;
    // }

    // public void SetGlobalLight(float newLightIntensity, float _duration = 2.34f){
    //     if(_duration != 2.34f){
    //         StartCoroutine(FadeLight(newLightIntensity, _duration));
    //     } else {
    //          StartCoroutine(FadeLight(newLightIntensity));
    //     }
    //     tempLight = newLightIntensity;
    // }

    // public void SetGlobalLightTorches(){
    //     StartCoroutine(FadeLight(0f));
    // }


    // public void SetGlobalLightBackToRegionDefault(){
    //     if(tempLight != 0){
    //         StartCoroutine(FadeLight(tempLight));
    //     } else {
    //         StartCoroutine(FadeLight(DefaultIntensity));
    //     }
    // }

    // public void SetGlobalLightBackToDefault(){
    //     StartCoroutine(FadeLight(DefaultIntensity));
    // }

    // IEnumerator FadeLight(float newLightIntensity, float _duration = 2.34f) {
    //     float startIntensity = _globalLight.intensity;
    //     float elapsedTime = 0.0f;
        
    //     if(_duration != 2.34f){
    //         while (elapsedTime < _duration) {
    //             _globalLight.intensity = Mathf.Lerp(startIntensity, newLightIntensity, elapsedTime / fadeDuration);
    //             elapsedTime += Time.deltaTime;
    //             yield return null;
    //         }
    //     } else {
    //         while (elapsedTime < fadeDuration) {
    //             _globalLight.intensity = Mathf.Lerp(startIntensity, newLightIntensity, elapsedTime / fadeDuration);
    //             elapsedTime += Time.deltaTime;
    //             yield return null;
    //         }
    //     }

    //     _globalLight.intensity = newLightIntensity;
    // }
}