using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arrow : MonoBehaviour {

    public float horizontalSpeed = 15f;
    public bool facingRight;
    public float timeToStart;
    public GameObject hitCollider;
    public GameObject groundCollider;

    private Rigidbody2D rb;
    private bool hitWall = false;
    private BoxCollider2D collidesWithGround;

    void Awake(){
        collidesWithGround = GetComponent<BoxCollider2D>();
        collidesWithGround.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        hitWall = true;
        Invoke("InitiateArrow", timeToStart);
    }

    public void InitiateArrow(){
        hitWall = false;
        Invoke("EnableCollider", 1f);
    }

    public void EnableCollider(){
        collidesWithGround.enabled = true;
    }

    void FixedUpdate(){
        if(!hitWall){
            if(Time.timeScale == 1f){
                Move();
            } else {
                Stop();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Ground")){
            hitWall = true;
            Stop();
        }
    }

    void Move(){
        if(!hitCollider.activeInHierarchy){
            UnfreezeRigidbody();
            groundCollider.SetActive(false);
            hitCollider.SetActive(true);
        }
        if(facingRight){
            GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalSpeed, 0f);
        } else{
            GetComponent<Rigidbody2D>().velocity = new Vector2(-horizontalSpeed, 0f);
        }
    }

    void Stop(){
        if(!groundCollider.activeInHierarchy){
            FreezeRigidbody();
            hitCollider.SetActive(false);
            groundCollider.SetActive(true);
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
    }

    private void FreezeRigidbody(){
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void UnfreezeRigidbody(){
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }


}
