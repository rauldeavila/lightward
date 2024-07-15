using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            GetComponent<ParticleSystem>().Play();
        }
    }
}
