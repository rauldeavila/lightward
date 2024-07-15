using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SpikeRespawn : MonoBehaviour {

    
    [InfoBox("On Trigger Enter this transform.position will be wiz respawn position", InfoMessageType.Warning)]
    
    private DamagePlayerHandler spike;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox") || collider.CompareTag("WizRoll")){
            ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("respawn_x", this.transform.position.x);
            ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("respawn_y", this.transform.position.y);
        }
    }    
}
