using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelyTrigger : MonoBehaviour {

    private Skely _skely;

    void Awake(){
        _skely = FindObjectOfType<Skely>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            _skely.StartBattle();
        }
    }
    

}
