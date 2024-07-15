using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameObjectInTime : MonoBehaviour {

    public float Time;
    public GameObject ObjectToEnable;
    public bool Disable = false;

    void Start(){
        Invoke("EnableObject", Time);
    }

    void EnableObject(){
        if(ObjectToEnable!= null){
            if(!Disable){
                ObjectToEnable.SetActive(true);
            } else {
                ObjectToEnable.SetActive(false);
            }
        }
    }

}
