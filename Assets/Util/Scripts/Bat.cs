using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Pathfinding;

// [RequireComponent(typeof(Seeker))]
public class Bat : MonoBehaviour {

    // Seeker seeker;

    private Transform target;
    [SerializeField] private Transform basePosition;


    public GameObject seekArea;
    public GameObject triggerArea;

    public bool Based = true;

    private TriggerArea triggerScript;
    private TriggerArea seekerScript;

    private Enemy Enemy;
    private Animator animator;
    private Rigidbody2D _rb;

    public float speed = 200f;
    public float nextWaypoint = 3f;
    public float timeBetweenFlips = 0.3f;
    public bool canFlip = true;
    public bool fly = false;

    // Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    void Awake(){
        // seeker = GetComponent<Seeker>(); 
        Enemy = GetComponent<Enemy>();
        triggerScript = triggerArea.GetComponent<TriggerArea>();
        seekerScript = seekArea.GetComponent<TriggerArea>();
        animator = GetComponent<Animator>();
        target = basePosition;
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start(){
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void OnTriggerStay2D(Collider2D collider) {
        if(collider.CompareTag("Base")){
            Based = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("Base")){
            Based = false;
        }
    }

    public void BatIdle(){
        fly = false;
        if(PlayerController.Instance.transform.position.x > this.transform.position.x){ // wiz on the right
            if(PlayerState.Instance.FacingRight){
                if(triggerScript.inside){
                    StartFlying();
                }
            }

        } else { // wiz on the left
            if(PlayerState.Instance.FacingRight == false){
                if(triggerScript.inside){
                    StartFlying();
                }
            }
        }
    }

    public void FixedUpdate(){
        if(fly){
            Fly();
        }
    }

    public void BatFly(){
        fly = true;
    }

    public void Fly(){
        if(seekerScript.inside){
            FlipBasedOnWizPosition();
            target = PlayerController.Instance.transform;
        } else {
            target = basePosition;  
            FlipBasedOnDestination(basePosition);

            if(Based){
                fly = false;
                GetComponent<Rigidbody2D>().velocity = Vector3.zero; // stop rigidbody
                animator.Play("bat_idle");
            }

        }

        // if(path == null){
        //     return;
        // }

        // if(currentWaypoint >= path.vectorPath.Count){
        //     reachedEndOfPath = true;
        //     return;
        // } else {
        //     reachedEndOfPath = false;
        // }

        // Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - _rb.position).normalized;
        // Vector2 force = direction * speed * Time.deltaTime;

        if(Enemy.GetDazed() == false){
            // GetComponent<Rigidbody2D>().velocity = new Vector2(force.x, force.y);
        }

        // float distance = Vector2.Distance(_rb.position, path.vectorPath[currentWaypoint]);

        // if(distance < nextWaypoint){
        //     currentWaypoint++;
        // }
    }

    void UpdatePath(){
        // if(seeker.IsDone()){
        //     seeker.StartPath(_rb.position, target.transform.position, OnPathComplete);
        // }
    }


    // void OnPathComplete(Path p){
    //     if(!p.error){
    //         path = p;
    //         currentWaypoint = 0;
    //     }
    // }


    public void FlipBasedOnWizPosition(){
        if(PlayerController.Instance.transform.position.x > this.transform.position.x){ // wiz on the right
            if(Enemy.FacingRight == false){
                Enemy.Flip();
            }
        } else { // wiz on the left
            if(Enemy.FacingRight){
                Enemy.Flip();
            }
        }
    }

    public void FlipBasedOnDestination(Transform destination){
        if(destination.position.x > this.transform.position.x){
            if(Enemy.FacingRight == false){
                Enemy.Flip();
            }
        } else { // wiz on the left
            if(Enemy.FacingRight){
                Enemy.Flip();
            }
        }
    }

    public void TriggerChasing(){
        animator.Play("bat_flying");
    }

    public void StartFlying(){
        animator.Play("bat_flying");
    }

}
