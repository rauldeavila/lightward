using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObjectByScriptableObject : MonoBehaviour {

    public GameObject objectToDisable;
    public GameObject objectToDisable2;
    public GameObject objectToDisable3;
    public BoolValue scriptableObjectToCheckIftrue;

    void Start(){
        if(scriptableObjectToCheckIftrue.initialValue == true){

            if(objectToDisable != null){
                objectToDisable.SetActive(false);       
            }

            if(objectToDisable2 != null){
                objectToDisable2.SetActive(false);
            }

            if(objectToDisable3 != null){
                objectToDisable3.SetActive(false);
            }
        }
    }

    void Update(){
        if(scriptableObjectToCheckIftrue.initialValue == true){

            if(objectToDisable != null){
                objectToDisable.SetActive(false);       
            }

            if(objectToDisable2 != null){
                objectToDisable2.SetActive(false);
            }

            if(objectToDisable3 != null){
                objectToDisable3.SetActive(false);
            }
        }
    }

}
