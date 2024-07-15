using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODParameterHandler : MonoBehaviour {


    [FMODUnity.EventRef]
    public string event_name = "event:/music/intro and tut/graveyard";
    public FMOD.Studio.EventInstance event_instance;
    public string parameter_name;

    void Start () {
        event_instance = FMODUnity.RuntimeManager.CreateInstance(event_name);
        event_instance.start();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            print("1");
            SetParameterToOne();
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            print("1-1 = 0 heh");
            SetParameterToZero();
        }
    }

    public void SetParameterToOne(){
        event_instance.setParameterByName(parameter_name, 1f);
    }

    public void SetParameterToZero(){
        event_instance.setParameterByName(parameter_name, 0f);
    }


}
