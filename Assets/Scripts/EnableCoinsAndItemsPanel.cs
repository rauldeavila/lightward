using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCoinsAndItemsPanel : MonoBehaviour {

    public GameObject Coins;
    // public GameObject ItemSlot;
    public static EnableCoinsAndItemsPanel Instance;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }

    void Start(){
        // if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("game_new_game").runTimeValue){
        //     Coins.SetActive(false);
        //     // ItemSlot.SetActive(false);
        // } else {
        //     EnableObjects();
        // }
    }

    // public void EnableObjects(){
    //     Coins.SetActive(true);
    //     // ItemSlot.SetActive(true);
    // }

    public void ShowCoins(){
        Coins.SetActive(true);
    }

    public void HideCoins(){
        Coins.SetActive(false);
    }

}
