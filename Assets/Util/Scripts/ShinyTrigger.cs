using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinyTrigger : MonoBehaviour {

    public Animator GameObjectToTrigger;
    public bool WithDelay = false;
    public float delay;
    
    
    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            if(WithDelay){
                Invoke("SetTriggerToTrue", delay);
            } else {
                SetTriggerToTrue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider){
        if(collider.CompareTag("WizHitBox")){
            GameObjectToTrigger.SetBool("shine", false);
        }
    }
    
    private void SetTriggerToTrue(){
        GameObjectToTrigger.SetBool("shine", true);
    }


}
