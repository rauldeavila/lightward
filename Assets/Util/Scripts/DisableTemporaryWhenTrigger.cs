using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTemporaryWhenTrigger : MonoBehaviour {
    
    public bool playHiddenAreaSFX = false;

    [FMODUnity.EventRef]
    public string hiddenSound = "";

    public GameObject gameObject1;
    public GameObject gameObject2;
    public GameObject gameObject3;
    public BoxCollider2D boxCollider1;
    public BoxCollider2D boxCollider2;
    public BoxCollider2D boxCollider3;
    public BoxCollider2D boxCollider4;
    public BoxCollider2D boxCollider5;
    public BoxCollider2D boxCollider6;
    public BoxCollider2D boxCollider7;
    public BoxCollider2D boxCollider8;
    public BoxCollider2D boxCollider9;
    public BoxCollider2D boxCollider10;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            DisableAllColliders();
        }
    }

    private void OnTriggerStay2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            DisableAllColliders();
        }
    }

    private void DisableAllColliders(){
        if(playHiddenAreaSFX){
            FMODUnity.RuntimeManager.PlayOneShot(hiddenSound, transform.position);
        }

        if(boxCollider1 != null){
            boxCollider1.enabled = false;
        }
        if(boxCollider2 != null){
            boxCollider2.enabled = false;
        }
        if(boxCollider3 != null){
            boxCollider3.enabled = false;
        }
        if(boxCollider4 != null){
            boxCollider4.enabled = false;
        }
        if(boxCollider5 != null){
            boxCollider5.enabled = false;
        }
        if(boxCollider6 != null){
            boxCollider6.enabled = false;
        }
        if(boxCollider7 != null){
            boxCollider7.enabled = false;
        }
        if(boxCollider8 != null){
            boxCollider8.enabled = false;
        }
        if(boxCollider9 != null){
            boxCollider9.enabled = false;
        }
        if(boxCollider10 != null){
            boxCollider10.enabled = false;
        }

        if(gameObject1 != null){
            gameObject1.SetActive(false);
        }
        if(gameObject2 != null){
            gameObject2.SetActive(false);
        }
        if(gameObject3 != null){
            gameObject3.SetActive(false);
        }
    }
}
