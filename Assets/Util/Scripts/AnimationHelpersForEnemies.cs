using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelpersForEnemies : MonoBehaviour {

    private Enemy _enemy;

    void Awake(){
        _enemy = GetComponentInParent<Enemy>();
    }

    public void DisableFlip(){
        _enemy.DisableFlip();
    }

    public void EnableFlip(){
        _enemy.EnableFlip();
    }


}
