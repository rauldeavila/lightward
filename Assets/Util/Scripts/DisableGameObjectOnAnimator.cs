using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObjectOnAnimator : MonoBehaviour {

    public GameObject ObjectToDisableOrEnable;
    public GameObject SecondObject;

    public void DisableGameObject(){
        ObjectToDisableOrEnable.SetActive(false);
    }

    public void EnableGameObject(){
        ObjectToDisableOrEnable.SetActive(true);
    }

	public void DisableSecondGameObject(){
		if(SecondObject != null){
			SecondObject.SetActive(false);
		}
    }

	public void EnableSecondGameObject(){
		if(SecondObject != null){
			SecondObject.SetActive(true);
		}
    }

}
