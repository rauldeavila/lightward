using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardBAreaController : MonoBehaviour {


    public BoolValue rightBarricadeSO;
    public BoolValue leftBarricadeSO;
    public GameObject rightTrigger;
    public GameObject leftTrigger;

    public GameObject regionToDeactivate;

    void Update(){
        if(leftBarricadeSO.initialValue && rightBarricadeSO.initialValue){
            this.gameObject.SetActive(false);
        }

        if(leftBarricadeSO.initialValue && leftTrigger.GetComponent<TriggerHandler>().wizOnTrigger){
            regionToDeactivate.SetActive(false);
        } else if(rightBarricadeSO.initialValue && rightTrigger.GetComponent<TriggerHandler>().wizOnTrigger){
            regionToDeactivate.SetActive(false);
        } else{
            regionToDeactivate.SetActive(true);
        }

    }

}
