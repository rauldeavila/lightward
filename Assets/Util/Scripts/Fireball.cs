using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fireball : MonoBehaviour {

    private ParticleSystem particleSystem;
    private Rigidbody2D rb;
    public GameObject fireballImpact;
    private string FireballImpact = "event:/char/wiz/fireball_impact";

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    public float speed = 10f;
    public bool right = false;

    void LateUpdate(){
        if(right){
            rb.velocity = transform.right * speed;
        } else{
            rb.velocity = -transform.right * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Enemy") || collider.CompareTag("Ground") || collider.CompareTag("Grass")){
            rb.velocity = Vector3.zero;
            FMODUnity.RuntimeManager.PlayOneShot(FireballImpact, transform.position);
            Instantiate(fireballImpact, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }


}
