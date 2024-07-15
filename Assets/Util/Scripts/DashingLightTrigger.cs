using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingLightTrigger : MonoBehaviour {

    private PlayerController controller;

    void Awake(){
        controller = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerStay2D(Collider2D collider) {
        if(collider.CompareTag("Light") && controller.Animator.GetBool("atLight")){
            collider.GetComponent<DashingLightReceiver>().WizInside();
        } else if(!(controller.AnimatorIsPlaying("inside_light") || controller.AnimatorIsPlaying("dashing_to_light"))){
            if(collider.GetComponent<DashingLightReceiver>() != null){
                collider.GetComponent<DashingLightReceiver>().WizOutside();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("Light")){
            collider.GetComponent<DashingLightReceiver>().WizOutside();
        }
    }

}
