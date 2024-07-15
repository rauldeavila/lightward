using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfield : MonoBehaviour {

    private PlayerController wiz;

    private void Awake(){
        wiz = FindObjectOfType<PlayerController>();
    }
    private void Update(){
        if(wiz.State.FacingRight == false){
            print("not facing right");
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -4);
        } else{
            print("facing right");
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 4);
        }
    }






}
