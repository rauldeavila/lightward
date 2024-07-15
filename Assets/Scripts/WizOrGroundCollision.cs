using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizOrGroundCollision : MonoBehaviour {

    public bool CollidedWithGround = false;
    public bool CollidedWithWiz = false;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            CollidedWithWiz = true;
        }
        if(collider.CompareTag("Ground") || collider.CompareTag("Grass")){
            CollidedWithGround = true;
        }
    }


}
