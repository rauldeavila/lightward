using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableColliderOnAnimator : MonoBehaviour {

    private BoxCollider2D collider;

    private void Awake(){
        collider = GetComponent<BoxCollider2D>();
    }

    public void DisableBoxCollider2D(){
        collider.enabled = false;
    }

}
