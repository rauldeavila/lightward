using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

    public bool On = false;
    public bool Outside = false;
    public Gate Gate;
    public Elevator Elevator;
    public BoolValue scriptableObject;

    void Start(){
        // Elevator levers DO NOT USE SOs (exactly, none of them!)
        if(scriptableObject != null){
            On = scriptableObject.runTimeValue;
            if (On) {
                Gate.UnlockGate();
            } else {
                Gate.LockGate();
            }
        }
    }


    public void Toggle(){
        if (scriptableObject != null) {
            On = !On;
            scriptableObject.runTimeValue = On;
            if (On)
                Gate.UnlockGate();
            else
                Gate.LockGate();
        }

        if(Elevator != null && !Outside){
            if(Elevator.CurLevel == 1){
                Elevator.GoUp();
            } else if(Elevator.CurLevel == 2){
                Elevator.GoDown();
            }
        }
        if(Elevator != null && Outside){
            if(Elevator.CurLevel == 1){
                Elevator.GoUpFast();
            } else if(Elevator.CurLevel == 2){
                Elevator.GoDownFast();
            }
        }
    }

}
