using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyProjectile : MonoBehaviour {

    private ParticleSystem particleSystem;
    private Rigidbody2D rb;
    public GameObject hitImpact;

    [SerializeField] private bool affectedByGravity = true; // Added boolean flag

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    public float speed = 10f;
    public Vector3 vector;

    void LateUpdate(){
        rb.velocity = vector.normalized * speed;
        if (affectedByGravity) {
            rb.gravityScale = 1f;
        } else {
            rb.gravityScale = 0f;
        }
    }


    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("WizHitBox") || collider.CompareTag("Ground")){

            rb.velocity = Vector3.zero;

            Instantiate(hitImpact, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
