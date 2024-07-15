using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizLightSetter : MonoBehaviour {
    // SetLight(float intensity, float innerRadius, float outerRadius, bool shake = false)

    public bool ResetOnExit = false;

    public float Intensity = 1f;
    public float InnerRadius = 4f;
    public float OuterRadius = 20f;
    public bool Shake = false;
    public float LerpVelocity = 0.5f;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            WizLightShaker.Instance.SetLight(Intensity, InnerRadius, OuterRadius, Shake, LerpVelocity);
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            if(ResetOnExit == true){
                WizLightShaker.Instance.ResetLight();
            }
        }
    }


}
