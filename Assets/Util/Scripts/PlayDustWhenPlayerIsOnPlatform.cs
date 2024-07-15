using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDustWhenPlayerIsOnPlatform : MonoBehaviour {

    public GameObject particles;
    public float yOffset = 0f;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            Instantiate(particles, new Vector2(this.transform.position.x, this.transform.position.y + yOffset), Quaternion.identity);
        }
        
    }

}
