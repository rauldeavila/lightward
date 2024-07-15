using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODParameter : MonoBehaviour {

    private FMOD.Studio.EventInstance instance;

    [FMODUnity.EventRef]
    public string fmodEvent;

    [SerializeField] [Range(-1f, 1f)]
    private float volume;

    private void Start(){
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }

    private void Update(){
        instance.setParameterByName("Volume", volume);
    }








}

