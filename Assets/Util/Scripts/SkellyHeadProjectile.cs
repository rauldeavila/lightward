using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkellyHeadProjectile : MonoBehaviour {

    private ParticleSystem particleSystem;
    private Rigidbody2D rb;
    public BoolValue boss1;

    public int contacts = 1;

    public float speed = 10f;
    private float step;
    private Vector2 target;
    public bool right = false;

    private Vector2 velocityRight;
    private Vector2 velocityLeft;
    private CircleCollider2D collider;

    private Skely skely;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        particleSystem = GetComponent<ParticleSystem>();
        skely = FindObjectOfType<Skely>();
        collider = GetComponent<CircleCollider2D>();
        EnableColliderInSeconds(0.2f);
    }

    void EnableColliderInSeconds(float seconds) {
        Invoke("EnableCollider", seconds);
    }

    void EnableCollider() {
        collider.enabled = true;
    }

    void Start() {
        if (right) {
            target = new Vector2(skely.target1Right.transform.position.x, skely.target1Right.transform.position.y);
        } else {
            target = new Vector2(skely.target1Left.transform.position.x, skely.target1Left.transform.position.y);
        }
        step = speed * Time.fixedDeltaTime;
        velocityRight = new Vector2(speed, 0f);
        velocityLeft = new Vector2(-speed, 0f);
    }

    void FixedUpdate(){
        if (boss1.initialValue == true) {
            this.gameObject.SetActive(false);
        }
        if(!GameState.Instance.Paused){
            transform.position = Vector2.MoveTowards(transform.position, target, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("WizHitBox")){
            rb.velocity = Vector3.zero;
            //Instantiate(fireballImpact, this.transform.position, Quaternion.identity);
            // return to head?
            skely.PlayHeadPoppin();
            Destroy(this.gameObject);
        }

        if (collider.CompareTag("Enemy") && contacts == 8) {
            skely.PlayReturnToHead();
            Destroy(this.gameObject);
        }
        
        if (collider.CompareTag("ModifyProjectile")) {
            contacts = contacts + 1;
            switch (contacts) {
                case 1:
                    SetTarget(1);
                    break;
                case 2:
                    SetTarget(2);
                    break;
                case 3:
                    SetTarget(3);
                    break;
                case 4:
                    SetTarget(4);
                    break;
                case 5:
                    SetTarget(5);
                    break;
                case 6:
                    SetTarget(6);
                    break;
                case 7:
                    SetTarget(7);
                    break;
                case 8:
                    SetTarget(8);
                    break;
                default:
                    break;
            }
        }
    }
    

    private void SetTarget(int targetNum) {
        switch (targetNum) {
            case 2:
                if (right) {
                    target = new Vector2(skely.target2Right.transform.position.x, skely.target2Right.transform.position.y);
                } else {
                    target = new Vector2(skely.target2Left.transform.position.x, skely.target2Left.transform.position.y);
                }
                break;
            case 3:
                if (right) {
                    target = new Vector2(skely.target3Right.transform.position.x, skely.target3Right.transform.position.y);
                } else {
                    target = new Vector2(skely.target3Left.transform.position.x, skely.target3Left.transform.position.y);
                }
                break;
            case 4:
                if (right) {
                    target = new Vector2(skely.target4Right.transform.position.x, skely.target4Right.transform.position.y);
                } else {
                    target = new Vector2(skely.target4Left.transform.position.x, skely.target4Left.transform.position.y);
                }
                break;
            case 5:
                if (right) {
                    target = new Vector2(skely.target5Right.transform.position.x, skely.target5Right.transform.position.y);
                } else {
                    target = new Vector2(skely.target5Left.transform.position.x, skely.target5Left.transform.position.y);
                }
                break;
            case 6:
                if (right) {
                    target = new Vector2(skely.target6Right.transform.position.x, skely.target6Right.transform.position.y);
                } else {
                    target = new Vector2(skely.target6Left.transform.position.x, skely.target6Left.transform.position.y);
                }
                break;
            case 7:
                if (right) {
                    target = new Vector2(skely.target7Right.transform.position.x, skely.target7Right.transform.position.y);
                } else {
                    target = new Vector2(skely.target7Left.transform.position.x, skely.target7Left.transform.position.y);
                }
                break;
            case 8:
                if (right) {
                    target = new Vector2(skely.target8Right.transform.position.x, skely.target8Right.transform.position.y);
                } else {
                    target = new Vector2(skely.target8Left.transform.position.x, skely.target8Left.transform.position.y);
                }
                break;
            default:
                break;
        }

    }


}