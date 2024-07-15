using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockProjectile : MonoBehaviour
{

    public Vector2 InitialForceVector;
    public float RotationSpeed;
    private Rigidbody2D _rb;    
    private int groundHitCount = 0;
    public int maxGroundHits = 10;
    public GameObject BreakEffect;

    void Awake(){
        _rb = GetComponent<Rigidbody2D>();
        RandomizeForceVector();
    }

    void RandomizeForceVector(){
        if(InitialForceVector.y != 0f)
        {
            float randomX = Random.Range(InitialForceVector.x - 3f, InitialForceVector.x + 3f);
            float randomY = Random.Range(InitialForceVector.y - 3f, InitialForceVector.y + 3f);
            InitialForceVector = new Vector2(randomX, randomY);
        }
    }

    void Start(){
        _rb.AddForce(InitialForceVector, ForceMode2D.Impulse);
    }

    void Update(){
        transform.rotation *= Quaternion.Euler(0, 0, RotationSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("WizHitBox"))
        {
            Break();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
       if(collider.CompareTag("WizHitBox"))
       {
        Break();
       } 
    }

    public void Break()
    {
        SFXController.Instance.Play("event:/game/00_game/stone_breaking");
        Instantiate(BreakEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void HitGround()
    {
        groundHitCount++;
        if (groundHitCount >= maxGroundHits)
        {
            Break();
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/game/00_game/stone_impact", transform.position);
            // SFXController.Instance.Play("event:/game/00_game/stone_impact");
        }
    }

}
