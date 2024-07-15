using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Sirenix.OdinInspector;

public class MusicLayerController : MonoBehaviour {

    public string Layer1 = "Layer1";
    [SerializeField] [Range(0f, 1f)]
    private float layer1_inspector;

    public string Layer2 = "Layer2";
    [SerializeField] [Range(0f, 1f)]
    private float layer2_inspector;

    public string Layer3 = "Layer3";
    [SerializeField] [Range(0f, 1f)]
    private float layer3_inspector;

    public string Layer4 = "Layer4";
    [SerializeField] [Range(0f, 1f)]
    private float layer4_inspector;

    public string Layer5 = "Layer5";
    [SerializeField] [Range(0f, 1f)]
    private float layer5_inspector;

    FMOD.Studio.Bus layer1;
    FMOD.Studio.Bus layer2;
    FMOD.Studio.Bus layer3;
    FMOD.Studio.Bus layer4;
    FMOD.Studio.Bus layer5;

    private Dictionary<int, Coroutine> lerpCoroutines = new Dictionary<int, Coroutine>();

    private void Awake(){
        layer1 = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/music/layer1");
        layer2 = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/music/layer2");
        layer3 = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/music/layer3");
        layer4 = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/music/layer4");
        layer5 = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/music/layer5");

        layer1_inspector = GetCurrentValueFromLayer(1);
        layer2_inspector = GetCurrentValueFromLayer(2);
        layer3_inspector = GetCurrentValueFromLayer(3);
        layer4_inspector = GetCurrentValueFromLayer(4);
        layer5_inspector = GetCurrentValueFromLayer(5);
    }

    private void Update(){
        layer1.setVolume(layer1_inspector);
        layer2.setVolume(layer2_inspector);
        layer3.setVolume(layer3_inspector);
        layer4.setVolume(layer4_inspector);
        layer5.setVolume(layer5_inspector);
    }

    public float GetCurrentValueFromLayer(int layer){
        float value = 0f;
        switch(layer){
            case 1:
                layer1.getVolume(out value);
                break;
            case 2:
                layer2.getVolume(out value);
                break;
            case 3:
                layer3.getVolume(out value);
                break;
            case 4:
                layer4.getVolume(out value);
                break;
            case 5:
                layer5.getVolume(out value);
                break;
            default:
                break;
        }
        return value;
    }

    public void SetLayerValue(int layer, float value, float duration){
        float startValue = GetCurrentValueFromLayer(layer);
        Coroutine lerpCoroutine;

        if(lerpCoroutines.TryGetValue(layer, out lerpCoroutine)){
            StopCoroutine(lerpCoroutine);
        }

        lerpCoroutine = StartCoroutine(LerpLayerValue(layer, startValue, value, duration));
        lerpCoroutines[layer] = lerpCoroutine;
    }

    private IEnumerator LerpLayerValue(int layer, float startValue, float endValue, float duration){
        float timeElapsed = 0f;

        while(timeElapsed < duration){
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / duration);
            float lerpedValue = Mathf.Lerp(startValue, endValue, t);

            UpdateInspectorValue(layer, lerpedValue);

            yield return null;
        }

        UpdateInspectorValue(layer, endValue);
    }

    private void UpdateInspectorValue(int layer, float value){
        switch(layer){
            case 1:
                print("LAYER1 NEW VALUE ======= " + value);
                layer1_inspector = value;
                break;
            case 2:
                layer2_inspector = value;
                break;
            case 3:
                layer3_inspector = value;
                break;
            case 4:
                layer4_inspector = value;
                break;
            case 5:
                layer5_inspector = value;
                break;
            default:
                break;
        }
    }

    // Methods for setting specific layer values...
    public void SetLayer1ValueTo(float value){
        SetLayerValue(1, value, 3f); // Change 1f to the desired duration
    }

    public void SetLayer2ValueTo(float value){
        SetLayerValue(2, value, 1f); // Change 1f to the desired duration
    }

    public void SetLayer3ValueTo(float value){
        SetLayerValue(3, value, 1f); // Change 1f to the desired duration
    }

    public void SetLayer4ValueTo(float value){
        SetLayerValue(4, value, 1f); // Change 1f to the desired duration
    }

    public void SetLayer5ValueTo(float value){
        SetLayerValue(5, value, 1f); // Change 1f to the desired duration
    }
    public float GetLayer1Value(){
        float value;
        layer1.getVolume(out value);
        return value;
    }

    public float GetLayer2Value(){
        float value;
        layer2.getVolume(out value);
        return value;
    }

    public float GetLayer3Value(){
        float value;
        layer3.getVolume(out value);
        return value;
    }

    public float GetLayer4Value(){
        float value;
        layer4.getVolume(out value);
        return value;
    }

    public float GetLayer5Value(){
        float value;
        layer5.getVolume(out value);
        return value;
    }
}
