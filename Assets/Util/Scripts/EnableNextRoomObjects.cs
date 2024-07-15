using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableNextRoomObjects : MonoBehaviour {
    
    public GameObject RoomObjects;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("WizHitBox")) {
            RoomObjects.SetActive(true);
        }
    }
}
