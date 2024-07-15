using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

public class EnemyBaseScript : MonoBehaviour {

    #region VARIABLES AND REFS

    [Header("Hitbox")]
    public GameObject HitBox;
    
    [Header("Debug")]
    public bool MoveOnUpdate = false;

    [Header("Enemy Type")]
    public bool patroller;
    public bool jumper;
    public bool chaser;

    [Header("Basic Attributes")]
    public bool facingRight = true;
    public bool dazed = false;
    public bool alive = true;
    
    [Header("Specifics")]
    public bool toggleDazedAttributes = false;
    [ShowIf("toggleDazedAttributes")]
    public bool hasDazedAnimation; // if not, single freeze frame
    [ShowIf("toggleDazedAttributes")]
    public bool ignoreDazedState = false;
    [ShowIf("toggleDazedAttributes")]
    public float dazedDuration = 1f;

    [ShowIf("patroller")]
    public float patrolSpeed= 4f;
    [ShowIf("jumper")]
    public float jumpForce =  10f;
    [ShowIf("chaser")]
    public float chaseSpeed= 6f;
    [ShowIf("chaser")]
    public float chaseDistance = 6f;
    
    
    // Grounded and Hole
    public bool toggleGroundedAttributes = false;
    [ShowIf("toggleGroundedAttributes")]
    public bool grounded = false;
    [ShowIf("toggleGroundedAttributes")]
    public Vector3 heightOffset;
    [ShowIf("toggleGroundedAttributes")]
    public Vector3 colliderOffset;
    [ShowIf("toggleGroundedAttributes")]
    public Vector3 heightOffsetLeft;
    [ShowIf("toggleGroundedAttributes")]
    public float groundLength = 0.18f;
    [ShowIf("toggleGroundedAttributes")]
    public LayerMask groundLayer;
    [ShowIf("toggleGroundedAttributes")]
    public bool flipOnHoles = false;
    [ShowIf("flipOnHoles")]
    public GameObject HoleChecker;

    // Jumping
    public bool toggleJumpingAttributes = false;
    [ShowIf("toggleJumpingAttributes")]
    public bool nearLeftWall = false;
    [ShowIf("toggleJumpingAttributes")]
    public bool nearRightWall = false;
    public Vector3 wallOffset;
    [ShowIf("toggleJumpingAttributes")]
    public float wallCircleRadius;
    
    [HideInInspector] public Rigidbody2D rb;
    private bool flipCooldown = false;
    private PlayerController wiz;
    private Animator animator;
    private Vector3 zeroedVelocity;
    private SpriteFlashTool spriteFlash;
    private float animatorDefaultSpeed = 1f; // for dazed

    #endregion

    #region UNITY METHODS

    void Awake(){
        spriteFlash = GetComponentInChildren<SpriteFlashTool>();
        wiz = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        zeroedVelocity = Vector3.zero;
        animatorDefaultSpeed = 1f;
    }

    void Update(){
        grounded = Physics2D.Raycast(transform.position + heightOffset + colliderOffset, Vector2.down, groundLength, groundLayer) || Physics2D.Raycast(transform.position + heightOffset - colliderOffset, Vector2.down, groundLength, groundLayer);
        if(MoveOnUpdate){
            Move();
        }
    }

    #endregion

    #region GROUND ENEMIES
    

    public void Move(){

        if(chaser){
            if(Mathf.Abs(wiz.transform.position.x - this.transform.position.x) > chaseDistance){
                Chase(); // can jump or flip here
                return;
            }
        }

        if(patroller){
            Patrol(); // can jump or flip here
            return;
        }

    }

    public void Chase(){
        if(wiz.transform.position.x > this.transform.position.x){
            // print("wiz on the right");
            if(!facingRight){
                Flip();
            }
        } else {
            // print("wiz on the left");
            if(facingRight){
                Flip();
            }
        }
        if(facingRight){
            GetComponent<Rigidbody2D>().velocity = new Vector2(chaseSpeed, GetComponent<Rigidbody2D>().velocity.y);
            // HitBox.GetComponent<TakeHit>().GetComponent<Rigidbody2D>().velocity = new Vector2(chaseSpeed, GetComponent<Rigidbody2D>().velocity.y);
            if(jumper){
                Jump();
            } else{
                FlipOnWalls();
            }
        } else{
            GetComponent<Rigidbody2D>().velocity = new Vector2(-chaseSpeed, GetComponent<Rigidbody2D>().velocity.y);
            // HitBox.GetComponent<TakeHit>().GetComponent<Rigidbody2D>().velocity = new Vector2(-chaseSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if(jumper){
                Jump();
            } else{
                FlipOnWalls();
            }
        }           
    }

    public void Patrol(){
        if(facingRight){
            GetComponent<Rigidbody2D>().velocity = new Vector2(patrolSpeed , GetComponent<Rigidbody2D>().velocity.y);
            // HitBox.GetComponent<TakeHit>().GetComponent<Rigidbody2D>().velocity = new Vector2(patrolSpeed , GetComponent<Rigidbody2D>().velocity.y);
            if(jumper){
                Jump();
            } else{
                FlipOnHoles();
                FlipOnWalls();
            }
        } else{
            GetComponent<Rigidbody2D>().velocity = new Vector2(-patrolSpeed, GetComponent<Rigidbody2D>().velocity.y);
                        // HitBox.GetComponent<TakeHit>().GetComponent<Rigidbody2D>().velocity = new Vector2(-patrolSpeed , GetComponent<Rigidbody2D>().velocity.y);
            if(jumper){
                Jump();
            } else{
                FlipOnHoles();
                FlipOnWalls();
            }
        }           
    }

    public void Jump(){
        grounded = Physics2D.Raycast(transform.position + heightOffset + colliderOffset, Vector2.down, groundLength, groundLayer) || Physics2D.Raycast(transform.position + heightOffset - colliderOffset, Vector2.down, groundLength, groundLayer);
        if(grounded){
            nearRightWall = Physics2D.OverlapCircle(transform.position + wallOffset, wallCircleRadius, groundLayer);
            nearLeftWall = Physics2D.OverlapCircle(transform.position - wallOffset, wallCircleRadius, groundLayer);

            if(facingRight && nearRightWall){
                GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
                // HitBox.GetComponent<TakeHit>().GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
            } else if(!facingRight && nearLeftWall){
                GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
                // HitBox.GetComponent<TakeHit>().GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
            }
        }
    }

    #endregion

    #region AUX METHODS

    public void Flip(){
        if(flipCooldown == false){
            flipCooldown = true;
            Invoke("CooldownOff", 0.3f);
            facingRight = !facingRight;
            transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
            // HitBox.transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
        }
    }

    private void CooldownOff(){
        flipCooldown = false;
    }

    public void DazeEnemy(){
        if(!ignoreDazedState) {
            dazed = true;
            if(hasDazedAnimation){
                animator.Play("Dazed");
            } else{
                animatorDefaultSpeed = animator.speed;
                animator.speed = 0.1f;
                Invoke("DazedOff", 0.3f);
            }
        }
    }

    public void DazedOff(){
        dazed = false;
        animator.speed = animatorDefaultSpeed;
    }

    public void CallOnAnimatorSetDazedToFalse(){
        dazed = false;
    }

    public void KillEnemy(){
        alive = false;
        animator.Play("Dead");
    }

    public void CallOnAnimatorStopRigidbody(){
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        // HitBox.GetComponent<TakeHit>().GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        
    }

    void FlipOnWalls(){

        if(grounded){
            nearRightWall = Physics2D.OverlapCircle(transform.position + wallOffset, wallCircleRadius, groundLayer);
            nearLeftWall = Physics2D.OverlapCircle(transform.position - wallOffset, wallCircleRadius, groundLayer);

            if(facingRight && nearRightWall){
                Flip();
            } else if(!facingRight && nearLeftWall){
                Flip();
            }
        }
    }

    void FlipOnHoles(){
        if(flipOnHoles){
            if(HoleChecker.GetComponent<HoleChecker>().onHole == true){
                Flip();
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + heightOffset + colliderOffset, transform.position + heightOffset + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position + heightOffset - colliderOffset, transform.position + heightOffset - colliderOffset + Vector3.down * groundLength);
        
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position + wallOffset, wallCircleRadius);
        Gizmos.DrawWireSphere(transform.position - wallOffset, wallCircleRadius);

    }

    public void AddForceToEnemy(float xForce, float yForce){
        GetComponent<Rigidbody2D>().velocity = new Vector2(xForce, yForce);
    }


    public void FlipBasedOnVelocity(){
        if(GetComponent<Rigidbody2D>().velocity.x > 0.5f && !facingRight){
            Flip();
        } else if(GetComponent<Rigidbody2D>().velocity.x < 0.5f && facingRight){
            Flip();
        }
    }
    
    
    public void FreezeRigidbody(){
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void UnfreezeRigidbody(){
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    
    public bool AnimatorIsPlaying(string stateName){
        if(animator != null){
            return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        } else{
            return false;
        }
    }
    
    
    public void FlipBasedOnWizPosition(){
        if(wiz.transform.position.x > this.transform.position.x){ // wiz on the right
            if(facingRight == false){
                Flip();
            }
        } else { // wiz on the left
            if(facingRight){
                Flip();
            }
        }
    }

    #endregion

}
