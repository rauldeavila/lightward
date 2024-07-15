using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateEmissionIntensity : MonoBehaviour {

    public Material mat;
    
    private void Start(){
        mat.SetColor("Color", Color.red);
    }

}
