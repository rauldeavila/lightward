using System.Collections;
using UnityEngine;

public class MoveTransform : MonoBehaviour
{
    public float StartingX = 0f;
    public float StartingY = 0f;
    public float MoveSpeed = 5f;
    public float LoopWhenReaches = 1062f;
    public bool useRigidbody = false;

    private Rigidbody2D _rb;
    private Vector3 _initialPosition;

    void Start()
    {
        _initialPosition = new Vector3(StartingX, StartingY, 0);
        transform.position = _initialPosition;

        if (useRigidbody)
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        if (MoveSpeed > 0)
        {
            StartCoroutine(IncrementXPosition());
        }
        else
        {
            StartCoroutine(DecrementXPosition());
        }
    }

    IEnumerator IncrementXPosition()
    {
        while (true)
        {
            while (transform.position.x < LoopWhenReaches)
            {
                MoveObject(MoveSpeed);
                yield return null;
            }
            transform.position = _initialPosition;
        }
    }

    IEnumerator DecrementXPosition()
    {
        while (true)
        {
            while (transform.position.x > LoopWhenReaches)
            {
                MoveObject(MoveSpeed);
                yield return null;
            }
            transform.position = _initialPosition;
        }
    }

    void MoveObject(float speed)
    {
        if (useRigidbody)
        {
            _rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }
}
