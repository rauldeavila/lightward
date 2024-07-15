using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactPositionListener : MonoBehaviour {

    public bool active = false;
    public bool fourOrFive = false;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Ground") || collider.CompareTag("Grass") || collider.CompareTag("Spike") || collider.CompareTag("Enemy") || collider.CompareTag("Breakable")){
            if(PlayerState.Instance.Grounded && fourOrFive){ // to avoid hiting ground when grounded
                active = false;
                return;
            }
            active = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collider) {
        if(collider.CompareTag("Ground") || collider.CompareTag("Grass") || collider.CompareTag("Spike") || collider.CompareTag("Enemy") || collider.CompareTag("Breakable")){
            if(PlayerState.Instance.Grounded && fourOrFive){ // to avoid hiting ground when grounded
                active = false;
                return;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collider){
        if(collider.CompareTag("Ground") || collider.CompareTag("Grass") || collider.CompareTag("Spike") || collider.CompareTag("Enemy") || collider.CompareTag("Breakable")){
            active = false;
        }
    }



}
