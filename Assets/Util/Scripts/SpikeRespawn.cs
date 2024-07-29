using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(BoxCollider2D))]
public class SpikeRespawn : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox") || collider.CompareTag("WizRoll")){
            ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("respawn_x", this.transform.position.x);
            ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("respawn_y", this.transform.position.y);
        }
    }    
}
