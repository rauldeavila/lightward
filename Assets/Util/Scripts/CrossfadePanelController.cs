using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossfadePanelController : MonoBehaviour {

    public static CrossfadePanelController Instance;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }

    public void FadeOut(){
        GetComponent<Animator>().SetTrigger("fadeout");
    }

    public void FadeIn(){
        GetComponent<Animator>().SetTrigger("fadein");
        // BaseCamera.Instance.GetComponent<Camera>().backgroundColor = Color.black;
    }

    public void Default()
    {
        GetComponent<Animator>().Play("default");
    }

}
