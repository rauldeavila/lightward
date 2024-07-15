using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlocksGate : MonoBehaviour {

    public GameObject gateHolder;

    public void UnlockGatesOnGateHolder(){
        if(gateHolder != null){
            gateHolder.GetComponentInChildren<GateTrigger>().UnlockGates();
        }
    }

}
