using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBCloseRangeAttack : MonoBehaviour {

    public float DistanceThreshold = 10f;
    public string AnimationClipName;
    private bool _playerNearby = false;

    void Update() {
        float _distanceToPlayer = Vector3.Distance(this.transform.position, PlayerController.Instance.transform.position);

        if (_distanceToPlayer <= DistanceThreshold) { // &&  !_playerNearby if want to stop looping

            _playerNearby = true;
            GetComponent<Enemy>().PlayAnimationIfExist(AnimationClipName);

        } else if (_distanceToPlayer > DistanceThreshold && _playerNearby) {

            _playerNearby = false;
        }
    }
}
