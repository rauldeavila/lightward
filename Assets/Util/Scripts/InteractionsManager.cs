using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsManager : MonoBehaviour {

    // they all reset when not interacting by default on StateController.cs
    public bool Dialogue = false;
    public bool Animation = false;
    public bool Cutscene = false;

    public static InteractionsManager Instance;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }

}
