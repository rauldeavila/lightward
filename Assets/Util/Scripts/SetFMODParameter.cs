using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFMODParameter : MonoBehaviour {

    private FMOD.Studio.Bus bus;

    [SerializeField] [Range(-80f, 80f)]
    private float busVolume;

    private void Start() {
        bus = FMODUnity.RuntimeManager.GetBus("bus:/music");
    }

    private void Update(){
        bus.setVolume(DecibelToLinear(busVolume));
    }

    private float DecibelToLinear(float dB){
        float linear = Mathf.Pow(10.0f, dB / 20f);
        return linear;
    }

}
