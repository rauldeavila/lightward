using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicParticle : MonoBehaviour {

    private Transform player;
    private PlayerStats playerStats;
    private float velocity = 1;
    private float timeToSpeedUp = 0.5f;


    private void Awake() {
        player = FindObjectOfType<PlayerController>().transform;
        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void Update() {
        Invoke("IncreaseSpeed", timeToSpeedUp);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
    
            if((playerStats.wiz_magic.runTimeValue + 25) <= playerStats.wiz_magic.maxValue){
                playerStats.wiz_magic.runTimeValue += 25;
            }
            transform.localScale = Vector3.zero;
            Destroy(gameObject);

        }
    }

    private void IncreaseSpeed(){
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, velocity * Time.deltaTime);
        velocity += 0.25f;
        timeToSpeedUp = 0f; // só na primeira vez vai ter delay
    }




}
