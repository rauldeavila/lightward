using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ShakeCamIfWizNearby : MonoBehaviour {

    public int ShakeIntensity = 0; // 0 = soft | 1 = medium | 2 = hard | -1 for disabling

    private bool _shakeEnabled = false;

    private void OnTriggerStay2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            _shakeEnabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider){
        if(collider.CompareTag("WizHitBox")){
            _shakeEnabled = false;
        }
    }

    public void ShakeCam(){ // call on animator or other script
        if(_shakeEnabled && (ShakeIntensity != -1)){
            CameraSystem.Instance.ShakeCamera(ShakeIntensity);
        }
    }

}
