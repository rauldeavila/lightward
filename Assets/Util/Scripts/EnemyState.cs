using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
[RequireComponent(typeof(TakeDamage))]
[RequireComponent(typeof(TakeHit))]
public class EnemyState : MonoBehaviour {

    [SerializeField] private bool _facingRight;
    public bool FacingRight { get => _facingRight; set => _facingRight = value; }

    [SerializeField] private bool _patrolling;
    public bool Patrolling { get => _patrolling; set => _patrolling = value; }

    [SerializeField] private bool _dazed;
    public bool Dazed { get => _dazed; set => _dazed = value; }

    [SerializeField] private bool _dead;
    public bool Dead { get => _dead; set => _dead = value; }

    [SerializeField] private bool _canTakeDamage;
    public bool CanTakeDamage { get => _canTakeDamage; set => _canTakeDamage = value; }

    [SerializeField] private bool _attacking;
    public bool Attacking { get => _attacking; set => _attacking = value; }

    public void SetAttacking(){
        SetAllToFalse();
        CanTakeDamage = true;
        Attacking = true;
    }

    public void SetPatrolling(){
        SetAllToFalse();
        CanTakeDamage = true;
        Patrolling = true;
    }

    public void SetDazed(){
        SetAllToFalse();
        Dazed = true;
    }

    public void SetDead(){
        SetAllToFalse();
        Dead = true;
    }

    public void SetCanTakeDamage(){
        CanTakeDamage = true;
    }

    public void SetCanTakeDamageToFalse(){
        CanTakeDamage = false;
    }

    public void SetAllToFalse(){
        Attacking = false;
        Patrolling = false;
        Dazed = false;
        Dead = false;
        CanTakeDamage = false;
    }
}
