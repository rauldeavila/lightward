using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlantDecoration : MonoBehaviour {

    public static BlantDecoration Instance;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }
    public void Tiles(){
        GetComponent<SpriteRenderer>().sortingLayerName = "Tiles";
    }

    public void Foreground(){
        GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
    }

}
