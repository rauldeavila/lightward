using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour {


    public bool inside = false;
    public bool entered = false;
    public bool exited = false;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            entered = true;
            Invoke("ResetEnteredState", 0.5f);
        }
    }
    private void OnTriggerStay2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            inside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            exited = true;
            inside = false;
            Invoke("ResetExitedState", 0.5f);
        }        
    }


    #region aux
    private void ResetEnteredState(){
        entered = false;
    }

    private void ResetExitedState(){
        exited = false;
    }

    #endregion

}
