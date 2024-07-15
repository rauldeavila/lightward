    using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skeleton : MonoBehaviour {


    public bool startAwake = false;

    public GameObject triggerArea;
    public GameObject dirtParticles;
    

    private TriggerArea triggerScript;

    private EnemyBaseScript enemy;
    private PlayerController wiz;
    private Animator animator;


    public float speed = 200f;
    public bool groundEnemy = true;
    public float timeBetweenFlips = 0.3f;
    public bool canFlip = true;
    public bool run = false;
    


    void Awake(){
        enemy = GetComponent<EnemyBaseScript>();
        wiz = FindObjectOfType<PlayerController>();
        triggerScript = triggerArea.GetComponent<TriggerArea>();
        animator = GetComponent<Animator>();
    }
    
    void Start(){
        if (startAwake) {
            WakeUp();
        }
    }

    public void WakeUp() {
        animator.Play("waking");
    }
    

    public void FixedUpdate(){
        if (enemy.AnimatorIsPlaying("idle")) {
            if (triggerScript.exited) {
                WakeUp();
                triggerScript.exited = false;
            }
        }

    }

    public void FlipBasedOnWizPosition(){
        if(wiz.transform.position.x > this.transform.position.x){ // wiz on the right
            if(enemy.facingRight == false){
                enemy.Flip();
            }
        } else { // wiz on the left
            if(enemy.facingRight){
                enemy.Flip();
            }
        }
    }


    public void InstantiateDirtParticles() {
        Instantiate(dirtParticles, this.transform.position, Quaternion.identity);
    }

    public void ForceUp() {
        enemy.rb.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
    }

}
