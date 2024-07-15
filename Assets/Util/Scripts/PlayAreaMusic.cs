using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Sirenix.OdinInspector;

public class PlayAreaMusic : MonoBehaviour {
    
    public int howManyLayers;
    public bool hasOneLayer = true;
    public bool hasTwoLayers = false;
    public bool hasThreeLayers = false;
    public bool hasFourLayers = false;
    public bool hasFiveLayers = false;

    
    [ShowIf("hasOneLayer")]
    public string Layer1 = "Layer1";
    [ShowIf("hasOneLayer")]
    [SerializeField] [Range(0f, 1f)]
    private float layer1_inspector;

    [ShowIf("hasTwoLayers")]
    public string Layer2 = "Layer2";
    [ShowIf("hasTwoLayers")]
    [SerializeField] [Range(0f, 1f)]
    private float layer2_inspector;

    [ShowIf("hasThreeLayers")]
    public string Layer3 = "Layer3";
    [ShowIf("hasThreeLayers")]
    [SerializeField] [Range(0f, 1f)]
    private float layer3_inspector;

    [ShowIf("hasFourLayers")]
    public string Layer4 = "Layer4";
    [ShowIf("hasFourLayers")]
    [SerializeField] [Range(0f, 1f)]
    private float layer4_inspector;

    [ShowIf("hasFiveLayers")]
    public string Layer5 = "Layer5";
    [ShowIf("hasFiveLayers")]
    [SerializeField] [Range(0f, 1f)]
    private float layer5_inspector;

    public FMOD.Studio.EventInstance instance_layer1;
    public FMOD.Studio.EventInstance instance_layer2;
    public FMOD.Studio.EventInstance instance_layer3;
    public FMOD.Studio.EventInstance instance_layer4;
    public FMOD.Studio.EventInstance instance_layer5;

    FMOD.Studio.Bus layer1;
    FMOD.Studio.Bus layer2;
    FMOD.Studio.Bus layer3;
    FMOD.Studio.Bus layer4;
    FMOD.Studio.Bus layer5;

    private void Awake(){
        layer1 = FMODUnity.RuntimeManager.GetBus("bus:/Master/music/layer1");
        layer2 = FMODUnity.RuntimeManager.GetBus("bus:/Master/music/layer2");
        layer3 = FMODUnity.RuntimeManager.GetBus("bus:/Master/music/layer3");
        layer4 = FMODUnity.RuntimeManager.GetBus("bus:/Master/music/layer4");
        layer5 = FMODUnity.RuntimeManager.GetBus("bus:/Master/music/layer5");

        layer1.setVolume(layer1_inspector);
        layer2.setVolume(layer2_inspector);
        layer3.setVolume(layer3_inspector);
        layer4.setVolume(layer4_inspector);
        layer5.setVolume(layer5_inspector);
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

    public void SetLayerValue(int layer, float value){
        switch(layer){
            case 1:
                layer1.setVolume(value);
                break;
            case 2:
                layer2.setVolume(value);
                break;
            case 3:
                layer3.setVolume(value);
                break;
            case 4:
                layer4.setVolume(value);
                break;
            case 5:
                layer5.setVolume(value);
                break;
            default:
                break;
        }
    }


    public void SetLayer1ValueTo(float value){
        layer1.setVolume(value);
    }

    public void SetLayer2ValueTo(float value){
        layer2.setVolume(value);
    }

    public void SetLayer3ValueTo(float value){
        layer3.setVolume(value);
    }

    public void SetLayer4ValueTo(float value){
        layer4.setVolume(value);
    }

    public void SetLayer5ValueTo(float value){
        layer5.setVolume(value);
    }

    // public float GetLayer1Value(){
    //     return layer1.getVolume();
    // }

    // public float GetLayer2Value(){
    //     return layer2.getVolume();
    // }

    // public float GetLayer3Value(){
    //     return layer3.getVolume();
    // }

    // public float GetLayer4Value(){
    //     return layer4.getVolume();
    // }

    // public float GetLayer5Value(){
    //     return layer5.getVolume();
    // }

}
