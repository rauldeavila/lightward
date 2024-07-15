using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class TriggerWithCollider : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            GetComponent<Animator>().SetTrigger("trigger");
        }
        
    }



}
