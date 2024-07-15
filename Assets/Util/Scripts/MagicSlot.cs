using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSlot : MonoBehaviour {

    public static MagicSlot Instance;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }

    public IntValue magicSlot1Index; // 0 = empty / 1 = time / 2 = fire / 3 = health / 4 = wolfsiege / 5  = dsoul / 6 = dlight
    public IntValue magicSlot2Index; // 0 = empty / 1 = time / 2 = fire / 3 = health / 4 = wolfsiege / 5  = dsoul / 6 = dlight

    public GameObject timeControl;
    public GameObject fireball;
    public GameObject healthRecover;
    public GameObject wolfsiege;
    public GameObject dashingSoul;
    public GameObject dashingLight;

    public GameObject timeControl2;
    public GameObject fireball2;
    public GameObject healthRecover2;
    public GameObject wolfsiege2;
    public GameObject dashingSoul2;
    public GameObject dashingLight2;

    void Start(){
        UpdateSlot1Image();
        UpdateSlot2Image();
    }

    public void UpdateSlot1Image(){
        switch(magicSlot1Index.initialValue){
            case 0:
                DisableAllSlot1Images();
                break;
            case 1:
                DisableAllSlot1Images();
                timeControl.SetActive(true);
                break;
            case 2:
                DisableAllSlot1Images();
                fireball.SetActive(true);
                break;
            case 3:
                DisableAllSlot1Images();
                healthRecover.SetActive(true);
                break;
            case 4:
                DisableAllSlot1Images();
                wolfsiege.SetActive(true);
                break;
            case 5:
                DisableAllSlot1Images();
                dashingSoul.SetActive(true);
                break;
            case 6:
                DisableAllSlot1Images();
                dashingLight.SetActive(true);
                break;
            default:
                DisableAllSlot1Images();
                break;
        }
    }

    public void UpdateSlot1(int magic_index){
        magicSlot1Index.initialValue = magic_index;
        UpdateSlot1Image();
    }

    private void DisableAllSlot1Images(){
        timeControl.SetActive(false);
        fireball.SetActive(false);
        healthRecover.SetActive(false);
        wolfsiege.SetActive(false);
        dashingSoul.SetActive(false);
        dashingLight.SetActive(false);
    }



    // ------- SLOT 2

    public void UpdateSlot2Image(){
        switch(magicSlot2Index.initialValue){
            case 0:
                DisableAllSlot2Images();
                break;
            case 1:
                DisableAllSlot2Images();
                timeControl2.SetActive(true);
                break;
            case 2:
                DisableAllSlot2Images();
                fireball2.SetActive(true);
                break;
            case 3:
                DisableAllSlot2Images();
                healthRecover2.SetActive(true);
                break;
            case 4:
                DisableAllSlot2Images();
                wolfsiege2.SetActive(true);
                break;
            case 5:
                DisableAllSlot2Images();
                dashingSoul2.SetActive(true);
                break;
            case 6:
                DisableAllSlot2Images();
                dashingLight2.SetActive(true);
                break;
            default:
                DisableAllSlot2Images();
                break;
        }
    }

    public void UpdateSlot2(int magic_index){
        magicSlot2Index.initialValue = magic_index;
        UpdateSlot2Image();
    }

    private void DisableAllSlot2Images(){
        timeControl2.SetActive(false);
        fireball2.SetActive(false);
        healthRecover2.SetActive(false);
        wolfsiege2.SetActive(false);
        dashingSoul2.SetActive(false);
        dashingLight2.SetActive(false);
    }


    public void ResetBothMagicSlots(){
        magicSlot1Index.initialValue = 0;
        magicSlot2Index.initialValue = 0;
        UpdateSlot1Image();
        UpdateSlot2Image();
    }











}
