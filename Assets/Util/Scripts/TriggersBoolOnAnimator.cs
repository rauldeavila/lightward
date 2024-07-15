using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersBoolOnAnimator : MonoBehaviour {

    public GameObject object1;
    public string object1boolName;
    public GameObject object2;
    public string object2boolName;

    public bool boolValueToSet = false;

    private void OnTriggerEnter2D(Collider2D collider) {

        if(collider.CompareTag("WizHitBox")){
            if(object1 != null){
                object1.GetComponent<Animator>().SetBool(object1boolName, boolValueToSet);
            }
            if(object2 != null){
                object2.GetComponent<Animator>().SetBool(object2boolName, boolValueToSet);
            }

        }
        
    }

}
