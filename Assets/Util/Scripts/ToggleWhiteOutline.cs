using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWhiteOutline : MonoBehaviour {

    private Material mat;

    private void Awake() {
        mat = GetComponent<Renderer>().material; 
    }

    public void TurnOutlineOn(){
        mat.SetFloat("_OutlineAlpha", 0.75f);
    }

    public void TurnOutlineOff(){
        mat.SetFloat("_OutlineAlpha", 0f);
    }







   
}
