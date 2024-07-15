using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour {

    public CapsuleCollider2D[] capsuleColliders;
    public EdgeCollider2D[] edgeColliders;

    public void DisableColliders(){
        foreach (CapsuleCollider2D col in capsuleColliders){
            col.enabled = false;
        }        
        foreach (EdgeCollider2D col in edgeColliders){
            col.enabled = false;
        }
        Invoke("EnableColliders", 1f);
    }

    public void EnableColliders() {
        foreach (CapsuleCollider2D col in capsuleColliders) {
            col.enabled = true;
        }
        foreach (EdgeCollider2D col in edgeColliders){
            col.enabled = true;
        }
    }




}
