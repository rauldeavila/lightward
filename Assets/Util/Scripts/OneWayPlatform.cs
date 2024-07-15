using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour {

    public GameObject GroundCollider;

    void Update(){
        if((PlayerController.Instance.transform.position.y -1f) > this.transform.position.y){
            if(Inputs.Instance.HoldingDownArrow){
                GroundCollider.SetActive(false);
            } else {
                GroundCollider.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            GroundCollider.SetActive(false);
        }
    }

}
