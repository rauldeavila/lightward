using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatformGround : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("Player")){
            PlayerState.Instance.OnPlatform = true;
        }
    }

    private void OnCollisionStay2D(Collision2D col) {
        if(col.gameObject.CompareTag("Player")){
            if(Inputs.Instance.HoldingJump && Inputs.Instance.HoldingDownArrow){
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col){
        if(col.gameObject.CompareTag("Player")){
            PlayerState.Instance.OnPlatform = false;
        }
    }
}
