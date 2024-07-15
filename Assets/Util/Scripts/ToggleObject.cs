using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour {
    
    public GameObject Object1;
    public GameObject Object2;
    public GameObject Object3;
    public bool Is1Enabled = false;
    public bool Is2Enabled = false;
    public bool Is3Enabled = false;
    
    private void Awake(){
        if(!Object1 == null){
            if(Object1.activeInHierarchy){
                Is1Enabled = true;
            } else{
                Is1Enabled = false;
            }
        }
        if(!Object2 == null){
            if(Object2.activeInHierarchy){
                Is2Enabled = true;
            } else{
                Is2Enabled = false;
            }
        }
        if(!Object3 == null){
            if(Object3.activeInHierarchy){
                Is3Enabled = true;
            } else{
                Is3Enabled = false;
            }
        }
    }
    
    public void Toggle(){
        if(Object1 != null){
            if(Is1Enabled){
                Object1.SetActive(false);
                Is1Enabled = false;
            } else{
                Object1.SetActive(true);
                Is1Enabled = true;
            }
        }
        if(Object2 != null){
            if(Is2Enabled){
                Object2.SetActive(false);
                Is2Enabled = false;
            } else{
                Object2.SetActive(true);
                Is2Enabled = true;
            }
        }
        if(Object3 != null){
            if(Is3Enabled){
                Object3.SetActive(false);
                Is3Enabled = false;
            } else{
                Object3.SetActive(true);
                Is3Enabled = true;
            }
        }
    }

}
