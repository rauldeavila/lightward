using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOnElevatorState : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            PlayerState.Instance.OnElevator = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            PlayerState.Instance.OnElevator = false;
        }
    }


}
