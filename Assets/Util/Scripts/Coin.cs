using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rb;

    void Awake(){
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {

        if(collider.CompareTag("WizHitBox")){
            FreezeRigidbody();
            PlayerStats.Instance.PickUpCoin();
            animator.Play("pickup");
        }
    }

    public void DestroyCoin(){
        Destroy(this.gameObject);
    }

    public void FreezeRigidbody(){
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }




}
