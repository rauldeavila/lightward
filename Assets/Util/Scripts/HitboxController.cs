using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
    public EdgeCollider2D dodgeCollider;
    private bool _flag = false;

    void Awake(){
        DisableCollider();
    }

    void Update(){
        if(PlayerController.Instance.AnimatorIsPlaying("dodge") || PlayerController.Instance.AnimatorIsPlaying("backdash")){
            if(_flag == false){
                EnableCollider();
                _flag = true;
            }
        } else {
            if(_flag){
                _flag = false;
                DisableCollider();
            }
        }
    }

    public void EnableCollider(){
        dodgeCollider.enabled = true;
    }

    public void DisableCollider(){
        dodgeCollider.enabled = false;
    }
}
