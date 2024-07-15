using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameObjectOnAnimator : MonoBehaviour {

    public GameObject ObjectToEnable;
    public GameObject SecondObject;


    public void EnableGameObject(){
        ObjectToEnable.SetActive(true);
    }

    public void EnableSecondGameObject(){
        SecondObject.SetActive(true);
    }

}
