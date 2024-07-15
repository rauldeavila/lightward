using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {

    public bool isSpike = false;
    public bool isParticle = false;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            PlayerHealth.Instance.TakeDamage(isSpike, transform.position);
        }
    }

    private void OnParticleCollision(GameObject collider){
        if(isParticle){
            if(collider.CompareTag("Player")){
                PlayerHealth.Instance.TakeDamage(isSpike, transform.position);
            }
        }
    }

}
