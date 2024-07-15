using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireball : MonoBehaviour {

    private ParticleSystem particleSystem;
    private Rigidbody2D rb;
    public GameObject fireballImpact;
    public string impactSoundPath;

    public Vector2 initialImpulse = new Vector2(0f, 0f);
    public float speed = 10f;
    public bool HitByPlayer = false;

    private BoxCollider2D myCollider;

    void Awake(){
        myCollider = GetComponent<BoxCollider2D>();
    }

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        particleSystem = GetComponent<ParticleSystem>();
        ApplyInitialImpulse();
        myCollider.enabled = false;  // Disable the collider at the start
        StartCoroutine(EnableColliderAfterDelay(0.1f));  // Enable the collider after a delay of 0.5 seconds
    }

    IEnumerator EnableColliderAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);  // Wait for the specified delay time
        myCollider.enabled = true;  // Enable the collider
    }

    void LateUpdate(){
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    private void ApplyInitialImpulse(){
        rb.AddForce(initialImpulse, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player") && 
        (
            PlayerController.Instance.AnimatorIsPlaying("attack") ||
            PlayerController.Instance.AnimatorIsPlaying("attack2") ||
            PlayerController.Instance.AnimatorIsPlaying("attack_down") ||
            PlayerController.Instance.AnimatorIsPlaying("attack_up")
        )){
            Knockback();
            return;
        }
        if(collider.CompareTag("WizHitBox") && !PlayerController.Instance.AnimatorIsPlaying("dodge")){
            Hit();
        } else if(collider.CompareTag("Ground")) {
            Hit();
        } else if(collider.CompareTag("Projectile")){
            Hit();
        } else if(collider.CompareTag("Enemy")){
            Hit();
        }
    }

    private void Hit(){
        rb.velocity = Vector2.zero;
        if(impactSoundPath != null){
            FMODUnity.RuntimeManager.PlayOneShot(impactSoundPath, transform.position);
        }
        if(fireballImpact != null){
            Instantiate(fireballImpact, this.transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }

    private void Knockback(){
        speed = -speed;
        HitByPlayer = true;
    }
}
