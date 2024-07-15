using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : MonoBehaviour {
    private EnemyController enemy;
    private EnemyState enemyState;
    private TakeDamage takeDamage;
    private Vector2 startPosition;
    public AnimationCurve xDyingCurve, xDyingCurveLeft, yDyingCurve;
    private bool started = false;
    private float timeElapsed = 0f;

    private void Awake(){
        enemy = GetComponent<EnemyController>();
        enemyState = GetComponent<EnemyState>();
        takeDamage = GetComponent<TakeDamage>();
    }

    // private void Update(){
    //     if(enemyState.Dazed == false){
    //         started = false;
    //         timeElapsed = 0f;
    //     } 
    // }


    public void KillEnemy(){
        enemyState.Dazed = true;
        if(!started){
            started = true;
            timeElapsed = 0f;
            startPosition = transform.position;
        } else {
            timeElapsed += Time.deltaTime;
            // move rb position to current position at cur time
            if(takeDamage.damageFromRight){
                enemy.Rigidbody2D.MovePosition(new Vector2(
                    startPosition.x + (xDyingCurve.Evaluate(timeElapsed)),
                    startPosition.y + (yDyingCurve.Evaluate(timeElapsed))
                ));
            } else {
                enemy.Rigidbody2D.MovePosition(new Vector2(
                    startPosition.x + (xDyingCurveLeft.Evaluate(timeElapsed)),
                    startPosition.y + (yDyingCurve.Evaluate(timeElapsed))
                ));
            }
        }

    }




}
