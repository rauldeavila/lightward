using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasts : MonoBehaviour {

    public bool StrongerDodge = false;
    public bool MuchStrongerDodge = false;
    public bool LighterDodge = false;
    public float raycastLength = 10f;
    public float yVariation = 0.25f;
    RaycastHit2D hit;
    int groundLayerMask;
    Vector2 direction;

    public static Raycasts Instance;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }

    void Start() {
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    void Update() {
        direction = PlayerState.Instance.FacingRight ? Vector2.right : Vector2.left;
        hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + yVariation, 0f), direction, raycastLength, groundLayerMask);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y + yVariation, 0f), direction * raycastLength);

        if (hit.collider != null) {
            if(hit.distance > 3.1f && hit.distance <= 3.5f){
                LighterDodge = false;
                MuchStrongerDodge = false;
                StrongerDodge = true;
            } else if (hit.distance > 3.5f && hit.distance <= 4.2f){
                StrongerDodge = false;
                LighterDodge = false;
                MuchStrongerDodge = true;
            } else if(hit.distance > 4.2f && hit.distance <= 4.6f) {
                StrongerDodge = false;
                MuchStrongerDodge = false;
                LighterDodge = true;
            } else {
                LighterDodge = false;
                StrongerDodge = false;
                MuchStrongerDodge = false;
            }
            // Debug.Log("Distance to ground: " + hit.distance);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y + yVariation, 0f), hit.point);
        } else {
            StrongerDodge = false;
            LighterDodge = false;
            MuchStrongerDodge = false;
        }
    }
}
