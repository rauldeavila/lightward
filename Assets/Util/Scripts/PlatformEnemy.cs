using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformEnemy : MonoBehaviour {
    
    [SerializeField] private float _moveSpeed = 1.2f;
    [SerializeField] private float _savedMoveSpeed = 0f;
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] public int _index;
    [SerializeField] private float _rotSpeed = 200f;
    private bool _isTurn;

    private void FixedUpdate() {
        Move();
    }

    public void Move() {
        if (Time.timeScale != 0) {
            transform.position = Vector2.MoveTowards(transform.position, _wayPoints[_index].position, _moveSpeed * Time.deltaTime);
            if (_isTurn){
                TurnRotation();
            }
            if (Vector2.Distance(transform.position, _wayPoints[_index].position) <= 0.05f) {
                _index++;
                _isTurn = true;
                if (_index > _wayPoints.Length - 1) {
                    _index = 0;
                }
            }
        }
    }

    private void TurnRotation() {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _wayPoints[_index].rotation, _rotSpeed * Time.deltaTime);
    }   
    
    public void SetMoveSpeedToZero(){
        _moveSpeed = 0f;
    }

    public void SetMoveSpeedToDefault(){
        _moveSpeed = _savedMoveSpeed;
    }

}
