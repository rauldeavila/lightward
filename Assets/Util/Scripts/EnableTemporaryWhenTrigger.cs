using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTemporaryWhenTrigger : MonoBehaviour {

    public GameObject gameObjectToEnable;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            if(!gameObjectToEnable.activeInHierarchy){
                gameObjectToEnable.SetActive(true);
            }
        }  
    }

    private void OnTriggerStay2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            if(!gameObjectToEnable.activeInHierarchy){
                gameObjectToEnable.SetActive(true);
            }

        }
    }

    
}
