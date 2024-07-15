using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameObjectOnTriggerEnter : MonoBehaviour {

    public GameObject TheObject;
    public bool DisableOnTriggerExit;
    public bool EnableOnTriggerExit;
    public bool DisableOnTriggerEnter;
    public bool EnableOnTriggerEnter;



    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            if(DisableOnTriggerEnter){
                TheObject.SetActive(false);
            }
            if(EnableOnTriggerEnter){
                TheObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox") && !PlayerController.Instance.AnimatorIsPlaying("dodge")){
            if(DisableOnTriggerExit){
                TheObject.SetActive(false);
            }
            if(EnableOnTriggerExit){
                TheObject.SetActive(true);
            }
        }
    }




}
