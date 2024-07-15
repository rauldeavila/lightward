using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBasedOnFalseSO : MonoBehaviour {

    public BoolValue scriptableObjectToCheck;
    public GameObject thisGameObject;

    private void Start() {
        if(scriptableObjectToCheck.initialValue == false){
            thisGameObject.SetActive(true);
        }
    }

    private void Update(){
        if(scriptableObjectToCheck.initialValue == false){
            thisGameObject.SetActive(true);
        } else{
            thisGameObject.SetActive(false);
        }
    }

}
