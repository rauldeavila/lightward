using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blant : MonoBehaviour {

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
            _enemy.Health = 999;
            if(!_enemy.AnimatorIsPlaying("blant_closed") && !_enemy.AnimatorIsPlaying("blant_closing")){
                _enemy.PlayAnimationIfExist("blant_closing");
            }

        } else if (_distanceToPlayer > DistanceThreshold && _playerNearby) {
            _playerNearby = false;
            if(_enemy.AnimatorIsPlaying("blant_closed") || _enemy.AnimatorIsPlaying("blant_closing")){
                _enemy.Health = 1;
                _enemy.PlayAnimationIfExist("blant_opening");
            }
        }
    }
}
