using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableIfTriggerForSeconds : MonoBehaviour {

    public GameObject ObjToEnable;
    public float WaitingTime = 0f;
    public bool IsCampfireHint = false;

     private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            Invoke("EnableObject", WaitingTime);
        }
    }

    void EnableObject(){
        ObjToEnable.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            CancelInvoke("EnableObject");
            ObjToEnable.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            if(IsCampfireHint){
                if(!PlayerController.Instance.AnimatorIsPlaying("sit")){
                    ObjToEnable.SetActive(false);
                }
            }
        }
    }


    
}
