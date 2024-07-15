using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoor : MonoBehaviour {

    public BoolValue event_scriptable;
    public GameObject thisGameObject;


    void Start() {
        if(event_scriptable.initialValue == true){
            thisGameObject.SetActive(false);
        }
        
    }

}
