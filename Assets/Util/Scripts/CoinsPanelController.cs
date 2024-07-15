using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CoinsPanelController : MonoBehaviour {

    public IntValue wiz_coins;
    private Animator animator;
    public GameObject tempCoins;
    public TextMeshProUGUI tempCoinsText;

    public static CoinsPanelController Instance;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
        animator = GetComponent<Animator>();
    }


    public void ShowTempCoins(){
        tempCoins.SetActive(true);
    }

    public void HideTempCoins(){
        tempCoins.SetActive(false);
    }

    public void UpdateTempCoinsText(string newValue){
        tempCoinsText.text = "+" + newValue;
    }

}
