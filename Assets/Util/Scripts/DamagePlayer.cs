using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.UI;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {

    public bool isSpike = false;
    public bool isParticle = false;
    public bool isFire = false;
    private bool _coolDownForDashingLight = false;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(!_coolDownForDashingLight)
        {
            if(isFire && (PlayerState.Instance.DashingLight || PlayerState.Instance.InsideLight))
            {
                _coolDownForDashingLight = true;
                Invoke("SetCooldownToFalseInSeconds", 0.5f);
                return;
            }
            else
            {
                if(collider.CompareTag("WizHitBox"))
                {
                    PlayerHealth.Instance.TakeDamage(isSpike, transform.position);
                }
            }
        }
    }

    private void OnParticleCollision(GameObject collider){
        if(isParticle){
            if(collider.CompareTag("Player")){
                PlayerHealth.Instance.TakeDamage(isSpike, transform.position);
            }
        }
    }

    void SetCooldownToFalseInSeconds()
    {
        _coolDownForDashingLight = false;
    }

}
