using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScriptableObjectOnTrigger : MonoBehaviour {

    public BoolValue scriptable;
    public float delay = 0f;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            Invoke("SetToTrue", delay);
        }
    }

    private void SetToTrue(){
        scriptable.initialValue = true;
    }

}
