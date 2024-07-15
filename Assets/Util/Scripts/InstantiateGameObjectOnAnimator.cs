using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateGameObjectOnAnimator : MonoBehaviour {

    public GameObject ObjectToInstantiateFacingRight;
    public GameObject ObjectToInstantiateFacingLeft;
    public GameObject OptionalPosition;

    public GameObject RightParticles;
    public GameObject RightPosition;

    public GameObject LeftParticles;
    public GameObject LeftPosition;

    private GameObject _objToInstantiate;

    void Update(){
        if(GetComponentInParent<Enemy>().FacingRight){
            _objToInstantiate = ObjectToInstantiateFacingRight;
        } else {
            _objToInstantiate = ObjectToInstantiateFacingLeft;
        }
    }

    public void InstantiateGameObject(){
        if(OptionalPosition != null){
            Instantiate(_objToInstantiate, OptionalPosition.transform.position, Quaternion.identity);
        } else {
            Instantiate(_objToInstantiate, this.transform.position, Quaternion.identity);
        }
    }

    public void InstantiateRightParticles(){
        Instantiate(RightParticles, RightPosition.transform.position, RightParticles.transform.rotation);
    }

    public void InstantiateLeftParticles(){
        Instantiate(LeftParticles, LeftPosition.transform.position, LeftParticles.transform.rotation);
    }

}
