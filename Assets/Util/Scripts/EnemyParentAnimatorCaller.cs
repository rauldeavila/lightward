using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParentAnimatorCaller : MonoBehaviour {
    private EnemyController enemy;

    private void Awake(){
        enemy = GetComponentInChildren<EnemyController>();
    }

    public void _DazeEnemy(){
        enemy.DazeEnemy();
    }

    public void _KillEnemy(){
        enemy.KillEnemy();
    }

    public void _FreezeRigidbody(){
        enemy.FreezeRigidbody();
    }

    public void _EnableEnemyToTakeDamage(){
        enemy.EnableEnemyToTakeDamage();
    }
}
