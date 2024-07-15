using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraInfluence : MonoBehaviour {

    public bool FollowingWiz = true;
    private bool _flag = false;
    private DOTweenAnimation _tweenAnimation;

    void Awake(){
        _tweenAnimation = GetComponent<DOTweenAnimation>();
    }

    void Update(){
        if(FollowingWiz){
            this.transform.position = PlayerController.Instance.transform.position;
        } else {
            if(_flag == false){
                _flag = true;
                CameraSystem.Instance.SetLookAt(this.transform);
                _tweenAnimation.DORestart();
            }
        }
    }
}
