using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopTransformHorizontally : MonoBehaviour
{

    public float StartingXPosition = 1062f;
    public float MoveSpeed = -5f; // this speed sets the direction

    public float FinalXPosition = -1346f;
    private float _currentXPosition = 0f;

    void Start(){
        _currentXPosition = StartingXPosition;
        if(MoveSpeed > 0)
        {
            StartCoroutine(IncrementXVelocity());
        }
        else
        {
            StartCoroutine(DecrementXVelocity());
        }
    }

    IEnumerator IncrementXVelocity()
    {
        while(true)
        {
            while(_currentXPosition < FinalXPosition)
            {
                _currentXPosition += Time.deltaTime * MoveSpeed * 10;
                yield return null;
            }
            _currentXPosition = StartingXPosition;
        }
    }

    IEnumerator DecrementXVelocity(){
        while(true)
        {
            while(_currentXPosition > FinalXPosition)
            {
                _currentXPosition += Time.deltaTime * MoveSpeed * 10;
                yield return null;
            }
            _currentXPosition = StartingXPosition;
        }
    }

    public void FixedUpdate(){
        transform.Translate(_currentXPosition * Time.deltaTime, Time.deltaTime, 0);
    }

}