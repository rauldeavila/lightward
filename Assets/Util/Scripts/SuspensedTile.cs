using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SuspensedTile : MonoBehaviour {

    public GameObject invisibleWall;
    private bool doOnce = true;
    private bool secondDoOnce = true;
    private TriggersWhenGrounded triggersWhenGrounded;

    private void Awake(){
        triggersWhenGrounded = GetComponentInChildren<TriggersWhenGrounded>();
        doOnce = true;
    }

    private void Update(){
        if(invisibleWall.active == false){

            if(doOnce){
                doOnce = false;
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                GetComponent<Rigidbody2D>().gravityScale = 5f;
            }

            if(triggersWhenGrounded.triggered == true){
                if(secondDoOnce){
                    secondDoOnce = false;
                    SetRigidbodyBackToStatic();
                }
            }
            
        }
    }

    public void SetRigidbodyBackToStatic(){ // called when landed
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }





}
