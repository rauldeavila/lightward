using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransform : MonoBehaviour
{

    public float StartingX = 0f;
    public float StartingY = 0f;
    public float MoveSpeed = 5f;

    public float LoopWhenReaches = 1062f;

    public bool useRigidbody = false;

    private Rigidbody2D _rb;
    private float _initialStartingX;

    void Start(){
        _rb = GetComponent<Rigidbody2D>();
        _initialStartingX = StartingX;
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
            while(StartingX < LoopWhenReaches)
            {
                StartingX += Time.deltaTime * MoveSpeed * 10;
                yield return null;
            }
            StartingX = _initialStartingX;
        }
    }

    IEnumerator DecrementXVelocity(){
        while(true)
        {
            while(StartingX > LoopWhenReaches)
            {
                StartingX += Time.deltaTime * MoveSpeed * 10;
                yield return null;
            }
            StartingX = _initialStartingX;
        }
    }

    public void FixedUpdate(){
        MoveObject();
    }

    void MoveObject(){
        if(useRigidbody)
        {
            _rb.velocity = new Vector2(StartingX, 0);
        } else 
        {
            transform.Translate(StartingX * Time.deltaTime, StartingY * Time.deltaTime, 0);
        }
    }

}
