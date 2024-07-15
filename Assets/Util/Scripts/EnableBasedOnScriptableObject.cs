using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBasedOnScriptableObject : MonoBehaviour {

    public BoolValue ScriptableObjectToCheck;
    public GameObject FalseHolder;
    public GameObject TrueHolder;

    public bool CheckOnUpdate = false;
    

    private void Awake() {
        if(ScriptableObjectToCheck.runTimeValue == false){
            if(FalseHolder != null){
                FalseHolder.SetActive(false);
            }
        } else{
            if(TrueHolder){
                TrueHolder.SetActive(true);
            }
        }
    }

    private void Update(){
        if (CheckOnUpdate) {
            if(ScriptableObjectToCheck.runTimeValue == false){
                if(FalseHolder != null){
                    FalseHolder.SetActive(false);
                }
            } else{
                if(TrueHolder){
                    TrueHolder.SetActive(true);
                }
            }
        }  
    }

}
