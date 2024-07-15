using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAirPatrol : MonoBehaviour {

    private EnemyController enemy;
    private EnemyState enemyState;
    private Checkers checkerScript;

    public float movementSpeed;
    public float ySpeed;

    public float timeToFlip = 3f;
    private float currentTimeBetweenFlip;

    public AnimationCurve xPatrolCurve, xPatrolCurveLeft, yPatrolCurve;
    private Vector2 startPosition;
    private bool started = false;
    private float timeElapsed = 0f;


    private void Awake() {
        enemy = GetComponent<EnemyController>();
        checkerScript = GetComponentInChildren<Checkers>();
        enemyState = GetComponent<EnemyState>();
        currentTimeBetweenFlip = timeToFlip;
    }

    private void FixedUpdate(){
        if(enemyState.Patrolling){
            Move();
            Timer();
        }
    }

    public void Move(){
        CheckIfHitWall();
        if(!started){
            started = true;
            timeElapsed = 0f;
            startPosition = transform.position;
        } else {
            timeElapsed += Time.deltaTime;
            // move rb position to current position at cur time
            if(enemyState.FacingRight){
                enemy.Rigidbody2D.MovePosition(new Vector2(
                    startPosition.x + (xPatrolCurve.Evaluate(timeElapsed)),
                    startPosition.y + (yPatrolCurve.Evaluate(timeElapsed))
                ));
            } else {
                enemy.Rigidbody2D.MovePosition(new Vector2(
                    startPosition.x + (xPatrolCurveLeft.Evaluate(timeElapsed)),
                    startPosition.y + (yPatrolCurve.Evaluate(timeElapsed))
                ));
            }
        }
    } 

    private void Timer(){
        if(currentTimeBetweenFlip > 0){
            currentTimeBetweenFlip -= Time.deltaTime;
        } else {
            Flip();
            currentTimeBetweenFlip = timeToFlip;
        }
    }

    private void CheckIfHitWall(){
        if(checkerScript.isWalled){
            Flip();
        }
    }

    private void Flip(){
        started = false;
        timeElapsed = 0f;
        enemyState.FacingRight = !enemyState.FacingRight;
        this.transform.Rotate(new Vector3(0, 180, 0));
        movementSpeed = -movementSpeed;
    }

    private void Stop(){
        enemy.Rigidbody2D.velocity = Vector2.zero;
    }

}
