using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skunner : MonoBehaviour {

    public enum State {
        Idle,
        Waking,
        Lifting,
        Running
    }

    public bool Awaken = false;
    public float RunSpeed = 4;
    public BoxCollider2D Collider;
    public State CurrentState;
    private Animator _anim;
    private Enemy _enemy;
    private bool _canCheckGrounded = false;
    
    void Awake(){
        _anim = GetComponent<Animator>();
        _enemy = GetComponentInParent<Enemy>();
        _enemy.SetRBConstraints(true, true, true);
    }

    void Start()
    {
        if(Awaken)
        {
            WakeUp();
        }
        else 
        {
            CurrentState = State.Idle;
        }
    }

    void Update(){
        if(_enemy.Grounded && _canCheckGrounded){ // can check grounded is true after a while in skunner-lifting animation
            _anim.SetBool("grounded", true);
        } else {
            _anim.SetBool("grounded", false);
        }

        if(_enemy.AnimatorIsPlaying("skunner-run") ){
            _enemy.SetRBVelocity(_enemy.FacingRight ? RunSpeed : -RunSpeed, 0);
        }
    }

    public string GetState(){
        return CurrentState.ToString();
    }

    public void ChangeState(State newState){
        CurrentState = newState;
    }

    public void WakeUp(){
        CurrentState = State.Waking;
        _anim.Play("skunner-waking");
    }

    public void EnableGroundCheckAndBoxCollider(){
        _canCheckGrounded = true;
        _enemy.SetRBConstraints(false, false, true);
        GetComponent<BoxCollider2D>().enabled = true; // trigger for damage
        Collider.enabled = true; // collider
    }

    public void LiftOff(){
        _enemy.SetRBVelocity(0, 10);
        Invoke("EnableGroundCheckAndBoxCollider", 0.3f);
    }
}
