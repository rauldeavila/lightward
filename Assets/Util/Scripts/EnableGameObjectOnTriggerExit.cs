using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameObjectOnTriggerExit : MonoBehaviour {

    public GameObject camBoundaryToEnable;

    private void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            camBoundaryToEnable.SetActive(true);
        }
    }





}
