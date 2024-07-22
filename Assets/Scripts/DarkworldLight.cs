using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkworldLight : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox"))
        {
            GameState.Instance.AtSafeZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox"))
        {
            GameState.Instance.AtSafeZone = false;
        }
    }
}