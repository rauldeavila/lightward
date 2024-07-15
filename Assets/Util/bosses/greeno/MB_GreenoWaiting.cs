using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_GreenoWaiting : MonoBehaviour {

    private EnemyController enemy;
    private EnemyState enemyState;
    private Checkers checkerScript;
    private TakeDamage damage;

    // Attack animation curves
    public AnimationCurve xAttackCurve, xAttackCurveLeft, yAttackCurve;
    private Vector2 startPosition;
    private bool started = false;
    private float timeElapsed = 0f;

    private float timeBetweenAttacks = 0f;
    public float startTimeBetweenAttacks = 10f;



    private void Awake() {
        enemy = GetComponent<EnemyController>();
        enemyState = GetComponent<EnemyState>();
        damage = GetComponent<TakeDamage>();
        checkerScript = GetComponentInChildren<Checkers>();
    }

    private void FixedUpdate(){
        if(timeBetweenAttacks > 0f){
            timeBetweenAttacks -= Time.deltaTime;
        }
    }


    public void Attack(){
        if(!started){
            started = true;
            timeElapsed = 0f;
            startPosition = transform.position;
        } else {
            timeElapsed += Time.deltaTime;
            // move rb position to current position at cur time
            if(enemyState.FacingRight){
                enemy.Rigidbody2D.MovePosition(new Vector2(
                    startPosition.x + (xAttackCurve.Evaluate(timeElapsed)),
                    startPosition.y + (yAttackCurve.Evaluate(timeElapsed))
                ));
            } else {
                enemy.Rigidbody2D.MovePosition(new Vector2(
                    startPosition.x + (xAttackCurveLeft.Evaluate(timeElapsed)),
                    startPosition.y + (yAttackCurve.Evaluate(timeElapsed))
                ));
            }
        }

    }

}
