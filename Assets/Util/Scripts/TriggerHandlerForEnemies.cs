using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandlerForEnemies : MonoBehaviour {

    public bool TriggerOtherEnemy = false;
    public Enemy EnemyToTrigger;
    private Enemy _enemy;


    void Awake(){
        if(TriggerOtherEnemy && EnemyToTrigger != null)
        {
            _enemy = EnemyToTrigger;
        }
        else
        {
            _enemy = GetComponentInParent<Enemy>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        _enemy.HitCollider(collider);
    }

}
