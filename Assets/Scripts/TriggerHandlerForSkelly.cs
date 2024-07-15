using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandlerForSkelly : MonoBehaviour
{
    private Enemy _enemy;

    void Awake(){
        _enemy = FindObjectOfType<Skelly>().GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        _enemy.HitCollider(collider);
        if(_enemy.Health <= 0)
        {
            this.transform.parent.gameObject.SetActive(false);
        }
    }

}
