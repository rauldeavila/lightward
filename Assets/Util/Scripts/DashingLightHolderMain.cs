using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingLightHolderMain : MonoBehaviour {
    
[SerializeField] private Vector3 _upperLight;
[SerializeField] private Vector3 _bottomLight;
[SerializeField] private Vector3 _closestLight;
[SerializeField] private GameObject _closestLightObject;
[SerializeField] private List<Vector3> _lightsInsideTrigger = new List<Vector3>();

private void OnTriggerStay2D(Collider2D collider) {
    if(collider.CompareTag("Light") && collider.gameObject.layer == LayerMask.NameToLayer("Light")){
        // Get the distance between the player and the light
        float distanceToPlayer = Vector3.Distance(PlayerController.Instance.transform.position, collider.transform.position);
        
        if(collider.transform.position.y > PlayerController.Instance.transform.position.y){
            // Set _upperLight to the position of the collider
            if (_bottomLight == collider.transform.position) {
                _bottomLight = Vector3.zero;
            }
            _upperLight = collider.transform.position;
        } else {
            // Set _bottomLight to the position of the collider
            if (_upperLight == collider.transform.position) {
                _upperLight = Vector3.zero;
            }
            _bottomLight = collider.transform.position;
        }
        
        if (!_lightsInsideTrigger.Contains(collider.transform.position)) {
            // Add the position of the collider to the list of lights inside the trigger zone
            _lightsInsideTrigger.Add(collider.transform.position);
        }
        
        if (distanceToPlayer != null && collider != null && collider.CompareTag("Light")) {
            UpdateClosestLight(distanceToPlayer, collider);
        } else {
            _closestLight = Vector3.zero;
            _closestLightObject = null;
            StateController.Instance.CanDashToLight = false;
            PlayerState.Instance.TargetLightPosition = Vector3.zero;
        }
    }
}

private void OnTriggerExit2D(Collider2D collider) {
    if(collider.CompareTag("Light")){
        if(collider.transform.position == _upperLight){
            // If the upper light leaves the trigger zone, set _upperLight to zero
            _upperLight = Vector3.zero;
        } else {
            // If the bottom light leaves the trigger zone, set _bottomLight to zero
            _bottomLight = Vector3.zero;
        }
        
        if (_lightsInsideTrigger.Contains(collider.transform.position)) {
            // Remove the position of the collider from the list of lights inside the trigger zone
            _lightsInsideTrigger.Remove(collider.transform.position);
        }
        
        UpdateClosestLight();
    }
}

private void UpdateClosestLight(float distanceToPlayer = Mathf.Infinity, Collider2D collider = null) {
    if (collider != null && (_closestLight == Vector3.zero || distanceToPlayer < Vector3.Distance(PlayerController.Instance.transform.position, _closestLight))) {
        // if (_closestLightObject != null) {
        //     _closestLightObject.GetComponentInParent<LightingSystem>().ResetLightTarget();
        // }

        _closestLight = collider.transform.position;
        _closestLightObject = collider.gameObject;
        StateController.Instance.CanDashToLight = true;
        PlayerState.Instance.TargetLightPosition = _closestLight;
        // _closestLightObject.GetComponentInParent<LightingSystem>().SetLightAsTarget();
    } else if (_lightsInsideTrigger.Count > 0) {
        Vector3 newClosestLight = GetClosestLight();
        if (_closestLight != newClosestLight) {
            if (_closestLightObject != null) {
                // _closestLightObject.GetComponentInParent<LightingSystem>().ResetLightTarget();
            }

            _closestLight = newClosestLight;
            _closestLightObject = FindLightByPosition(_closestLight);
            StateController.Instance.CanDashToLight = true;
            PlayerState.Instance.TargetLightPosition = _closestLight;
            if (_closestLightObject != null) {
                // _closestLightObject.GetComponentInParent<LightingSystem>().ResetLightTarget();
            }
        }
    } else {
        if (_closestLightObject != null) {
            // _closestLightObject.GetComponentInParent<LightingSystem>().ResetLightTarget();
        }

        _closestLight = Vector3.zero;
        _closestLightObject = null;
        StateController.Instance.CanDashToLight = false;
        PlayerState.Instance.TargetLightPosition = Vector3.zero;
    }
}

// Helper method to get the position of the closest light to the player
private Vector3 GetClosestLight() {
    float distanceToClosestLight = Mathf.Infinity;
    Vector3 closestLight = Vector3.zero;

    foreach (Vector3 lightPosition in _lightsInsideTrigger) {
        float distanceToPlayer = Vector3.Distance(PlayerController.Instance.transform.position, lightPosition);

        // Skip this light if the player is too close to it
        if (distanceToPlayer < 1f) {
            continue;
        }

        if (distanceToPlayer < distanceToClosestLight) {
            closestLight = lightPosition;
            distanceToClosestLight = distanceToPlayer;
        }
    }

    return closestLight;
}

// Helper method to find the GameObject of a light given its position
    private GameObject FindLightByPosition(Vector3 position) {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(position, 0.1f)) {
            if (collider.CompareTag("Light") && collider.transform.position == position) {
                return collider.gameObject;
            }
        }

        return null;
    }
}
