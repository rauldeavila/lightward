using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour{

    private EnemyController enemy;
    private EnemyState enemyState;
    private Checkers checkerScript;
    private SpriteRenderer sr;
    public bool callOnBehaviour = false;

    public float movementSpeed;


    private void Awake() {
        enemy = GetComponent<EnemyController>();
        checkerScript = GetComponentInChildren<Checkers>();
        enemyState = GetComponent<EnemyState>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        if(!callOnBehaviour){
            Move();
        }
    }

    public void Move(){
        if(!enemyState.Dazed && !enemyState.Dead){
            enemyState.Patrolling = true;
            CheckIfGrounded();
            CheckIfHitWall();
            enemyState.Patrolling = true;
            enemy.Rigidbody2D.velocity = Vector2.right * movementSpeed * Time.deltaTime;
        } else{
            Stop();
            enemyState.Patrolling = false;
        }
    }

    private void CheckIfGrounded(){
        if(!checkerScript.isGrounded){
            Flip();
        }
    }

    private void CheckIfHitWall(){
        if(checkerScript.isWalled){
            Flip();
        }
    }

    private void Flip(){
        enemyState.FacingRight = !enemyState.FacingRight;
        this.transform.Rotate(new Vector3(0, 180, 0));
        movementSpeed = -movementSpeed;
    }

    private void Stop(){
        enemy.Rigidbody2D.velocity = Vector2.zero;
    }
    

}
