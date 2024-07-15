using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsTriggerHolder : MonoBehaviour {

    private BoxCollider2D collider;

    private void Awake(){
        collider = GetComponent<BoxCollider2D>();
        StartCoroutine(EnableCollider());
    }

    IEnumerator EnableCollider(){
        yield return new WaitForSeconds(0.1f);
        collider.enabled = true;
    }



}
