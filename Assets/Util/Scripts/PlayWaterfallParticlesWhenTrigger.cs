using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWaterfallParticlesWhenTrigger : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            WaterParticlesHandler.Instance.PlayWaterJumpParticles();
        }
    }


}
