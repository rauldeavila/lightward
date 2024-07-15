using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treethro : MonoBehaviour {

    public float DistanceThreshold = 10f;
    private bool _playerNearby = false;
    private Enemy _enemy;

    void Awake(){
        _enemy = GetComponent<Enemy>();
    }

    void Update() {
        float _distanceToPlayer = Vector3.Distance(this.transform.position, PlayerController.Instance.transform.position);

        if (_distanceToPlayer <= DistanceThreshold) { // &&  !_playerNearby if want to stop looping

            _playerNearby = true;
            if(_enemy.AnimatorIsPlaying("treethro_shooting") || _enemy.AnimatorIsPlaying("treethro_returning")){
                _enemy.PlayAnimationIfExist("treethro_hiding");
            }

        } else if (_distanceToPlayer > DistanceThreshold && _playerNearby) {
            _playerNearby = false;
            if(_enemy.AnimatorIsPlaying("treethro_hidden")){
                _enemy.PlayAnimationIfExist("treethro_returning");
            }
        }
    }
}
