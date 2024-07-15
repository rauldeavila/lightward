using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableInvisibleWallBasedOnOtherGameObject : MonoBehaviour {

    public GameObject objectToCheck;

    private void Update(){
        if(objectToCheck.active == false){
            this.gameObject.SetActive(false);
        }
    }

}
