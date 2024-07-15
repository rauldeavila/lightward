using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLink : MonoBehaviour {

    private Chain chain;

    void Awake() {
        chain = GetComponentInParent<Chain>();
    }
    
    
    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("WizHitBox")){
            chain.DisableColliders();
        }
    }
    
}
