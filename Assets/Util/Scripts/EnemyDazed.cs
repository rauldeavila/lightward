using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDazed : MonoBehaviour {

    private EnemyController enemy;
    private EnemyState enemyState;
    private TakeDamage takeDamage;
    private Vector2 startPosition;
    public AnimationCurve xDazedCurve, xDazedCurveLeft, yDazedCurve;
    private bool started = false;
    private float timeElapsed = 0f;

    private void Awake(){
        enemy = GetComponent<EnemyController>();
        enemyState = GetComponent<EnemyState>();
        takeDamage = GetComponent<TakeDamage>();
    }

    private void Update(){
        if(enemyState.Dazed == false){
            started = false;
            timeElapsed = 0f;
        }
    }

    public void Dazed(){
        if(!started){
            started = true;
            timeElapsed = 0f;
            startPosition = transform.position;
        } else {
            timeElapsed += Time.deltaTime;
            if(takeDamage.damageFromRight){
                enemy.Rigidbody2D.MovePosition(new Vector2(
                    startPosition.x + (xDazedCurve.Evaluate(timeElapsed)),
                    startPosition.y + (yDazedCurve.Evaluate(timeElapsed))
                ));
            } else {
                enemy.Rigidbody2D.MovePosition(new Vector2(
                    startPosition.x + (xDazedCurveLeft.Evaluate(timeElapsed)),
                    startPosition.y + (yDazedCurve.Evaluate(timeElapsed))
                ));
            }
        }
    }

}
