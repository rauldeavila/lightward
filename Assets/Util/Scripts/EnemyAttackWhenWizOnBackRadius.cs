using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackWhenWizOnBackRadius : MonoBehaviour {
    
    
    private EnemyController enemy;
    private EnemyState enemyState;
    private Checkers checkerScript;
    public bool canAttack = false;
    private Vector2 startPosition;
    public AnimationCurve xAttackCurve, xAttackCurveLeft, yAttackCurve;
    private bool started = false;
    private float timeElapsed = 0f;

    private float timeBetweenAttacks = 0f;
    public float startTimeBetweenAttacks = 10f;



    private void Awake() {
        enemy = GetComponent<EnemyController>();
        checkerScript = GetComponentInChildren<Checkers>();
        enemyState = GetComponent<EnemyState>();
    }

    private void FixedUpdate(){
        if(timeBetweenAttacks > 0f){
            timeBetweenAttacks -= Time.deltaTime;
        }
        CheckIfWizOnRadius();
        if(!enemyState.Attacking){
            started = false;
            timeElapsed = 0f;
        }
    }

    private void CheckIfWizOnRadius(){
        if(checkerScript.wizBack){
            TryToAttack();
        }
    }

    private void TryToAttack(){
        if(timeBetweenAttacks <= 0 && enemyState.Patrolling){
            timeBetweenAttacks = startTimeBetweenAttacks;
            enemy.Animator.SetTrigger("back_attack");
        }
    }

    public void Attack(){
        enemyState.Attacking = true;
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
