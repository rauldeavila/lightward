using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private Rigidbody2D rb;
    public GameObject ImpactParticles;
    public string ImpactSoundPath;
    public Vector2 initialImpulse = new Vector2(0f, 0f);
    public float Speed = 10f;
    public bool GravityAsInitialSpeed = false;
    public bool DoNothingOnGroundCollision = false; // ao bater no chao, esse script é desabilitado
    public float SpeedAfterHit = 10f;
    public float Multiplier = 1.1f;
    public bool LastHitByPlayer = false;

    private BoxCollider2D myCollider;
    private bool _startedFacingRight = false;
    private Transform parentTransform;

    public bool GoingUp = false;
    public bool GoingDown = false;
    public bool GoingLeft = false;
    public bool GoingRight = false;

    private float _upRotation = 0f;
    private float _downRotation = 0f;
    private float _leftRotation = 0f;
    private float _rightRotation = 0f;

    void Awake(){
        myCollider = GetComponent<BoxCollider2D>();
        if(Speed > 0){
            _startedFacingRight = true;
        } else {
            _startedFacingRight = false;
        }
    }

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        particleSystem = GetComponent<ParticleSystem>();
        ApplyInitialImpulse();
        myCollider.enabled = false; 
        StartCoroutine(EnableColliderAfterDelay(0.1f)); 
        SetDefaultRotationsBasedOnStartVelocity();
        parentTransform = transform.parent;
    }


    void LateUpdate(){
        if(!GravityAsInitialSpeed){ // its set to false when hit or knocked back
            if(GoingUp){
                rb.velocity = new Vector2(0, Speed);
            } else if(GoingDown){
                rb.velocity = new Vector2(0, -Speed);
            } else if(GoingLeft){
                rb.velocity = new Vector2(-Speed, 0);
            } else if(GoingRight){
                rb.velocity = new Vector2(Speed, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player") && 
        (
            PlayerController.Instance.AnimatorIsPlaying("attack") ||
            PlayerController.Instance.AnimatorIsPlaying("attack2") ||
            PlayerController.Instance.AnimatorIsPlaying("attack_down") ||
            PlayerController.Instance.AnimatorIsPlaying("attack_up")
        )){
            if(PlayerController.Instance.AnimatorIsPlaying("attack_up")){
                WizAttackImpact.Instance.CheckImpactPositionAndGenerate(3, this.transform.position);
            } else if(PlayerController.Instance.AnimatorIsPlaying("attack_down")) {
                WizAttackImpact.Instance.CheckImpactPositionAndGenerate(2, this.transform.position);
            } else { // regular attack
                WizAttackImpact.Instance.CheckImpactPositionAndGenerate(1, this.transform.position);
            }
            Knockback(collider);
            return;
        }

        if(collider.CompareTag("DeflectProjectile")){
            if(collider.GetComponent<DeflectProjectile>().Up){
                Knockback(collider, "up");
                return;
            } else if(collider.GetComponent<DeflectProjectile>().Down){
                Knockback(collider, "down");
                return;
            } else if(collider.GetComponent<DeflectProjectile>().Left){
                Knockback(collider, "left");
                return;
            } else if(collider.GetComponent<DeflectProjectile>().Right){
                Knockback(collider, "right");
                return;
            }
        }
        if(collider.CompareTag("WizHitBox") && !PlayerController.Instance.AnimatorIsPlaying("dodge")){
            Hit();
        } else if(collider.CompareTag("Ground") || collider.CompareTag("Grass") || collider.CompareTag("Breakable")) {
            if(DoNothingOnGroundCollision && !LastHitByPlayer){
                this.enabled = false;
                return;
            }
            Hit();
        } else if(collider.CompareTag("Projectile")){
            Hit();
        } else if(collider.CompareTag("Enemy")){
            if(LastHitByPlayer){
                Hit();
            } else{
                return;
            }
        }
    }

    private void Hit(){
        DisableGravityCalculations();
        rb.velocity = Vector2.zero;
        if(ImpactSoundPath != null){
            FMODUnity.RuntimeManager.PlayOneShot(ImpactSoundPath, transform.position);
        }
        if(ImpactParticles != null){
            Instantiate(ImpactParticles, this.transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
        if (parentTransform != null) {
            Destroy(parentTransform.gameObject);
        }
    }

    private void Knockback(Collider2D collider, string direction = "none"){
        DisableGravityCalculations();

        float diffX = -Mathf.Abs(collider.transform.position.x - transform.position.x);

        if (PlayerController.Instance.AnimatorIsPlaying("attack_down") || direction == "down") {
            ResetAllGoingStates();
            GoingDown = true;
            transform.rotation = Quaternion.Euler(0, 0, _downRotation);
        } 
        else if (PlayerController.Instance.AnimatorIsPlaying("attack_up") || direction == "up") {
            ResetAllGoingStates();
            GoingUp = true;
            transform.rotation = Quaternion.Euler(0, 0, _upRotation);
        } else{
            if(direction != "none"){
                if(direction == "right"){
                    ResetAllGoingStates();
                    GoingRight = true;
                    transform.rotation = Quaternion.Euler(0, 0, _rightRotation);
                } else if(direction == "left"){
                    ResetAllGoingStates();
                    GoingLeft = true;
                    transform.rotation = Quaternion.Euler(0, 0, _leftRotation);
                }
            } else {
                if(PlayerState.Instance.FacingRight && diffX <= 0){
                    ResetAllGoingStates();
                    GoingRight = true;
                    transform.rotation = Quaternion.Euler(0, 0, _rightRotation);
                } else {
                    ResetAllGoingStates();
                    GoingLeft = true;
                    transform.rotation = Quaternion.Euler(0, 0, _leftRotation);
                }
            }
        }
        Speed = Mathf.Abs(Speed) * Multiplier; 
        LastHitByPlayer = true;
    }






    private void SetDefaultRotationsBasedOnStartVelocity(){ // called on start only
        if(GoingUp){
            _downRotation = 180f;
            _rightRotation = -90f;
            _upRotation = 0f;
            _leftRotation = 90f;
        } else if(GoingDown){
            _downRotation = 0f;
            _rightRotation = 90f;
            _upRotation = 180f;
            _leftRotation = -90f;
        } else if(GoingLeft){
            _downRotation = 90f;
            _rightRotation = 180f;
            _upRotation = -90f;
            _leftRotation = 0f;
        } else if(GoingRight){
            _downRotation = -90f;
            _rightRotation = 0f;
            _upRotation = 90f;
            _leftRotation = 180f;
        }
    }

    private void DisableGravityCalculations(){ // called on collision only
        if(GravityAsInitialSpeed){
            GravityAsInitialSpeed = false; // for running only once
            Speed = SpeedAfterHit;
            rb.gravityScale = 0f;
        }
    }
    private void ResetAllGoingStates(){
        GoingUp = false;
        GoingDown = false;
        GoingLeft = false;
        GoingRight = false;
    }
    private void ApplyInitialImpulse(){ // called on start only
        rb.AddForce(initialImpulse, ForceMode2D.Impulse);
    }
    IEnumerator EnableColliderAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);  
        myCollider.enabled = true;  
    }

}
