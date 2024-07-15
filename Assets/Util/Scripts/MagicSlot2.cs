using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSlot2 : MonoBehaviour {

    public IntValue magicSlot2Index; // 0 = empty / 1 = time / 2 = fire / 3 = health / 4 = wolfsiege / 5  = dsoul / 6 = dlight

    public GameObject timeControl;
    public GameObject fireball;
    public GameObject healthRecover;
    public GameObject wolfsiege;
    public GameObject dashingSoul;
    public GameObject dashingLight;

    public void UpdateImage(){

        switch(magicSlot2Index.initialValue){

            case 0:
                DisableAllImages();
                break;
            case 1:
                DisableAllImages();
                timeControl.SetActive(true);
                break;
            case 2:
                DisableAllImages();
                fireball.SetActive(true);
                break;
            case 3:
                DisableAllImages();
                healthRecover.SetActive(true);
                break;
            case 4:
                DisableAllImages();
                wolfsiege.SetActive(true);
                break;
            case 5:
                DisableAllImages();
                dashingSoul.SetActive(true);
                break;
            case 6:
                DisableAllImages();
                dashingLight.SetActive(true);
                break;
            default:
                DisableAllImages();
                break;
                
        }
    }


    public void UpdateSlot(int magic_index){
        magicSlot2Index.initialValue = magic_index;
        UpdateImage();
    }


    private void DisableAllImages(){
        timeControl.SetActive(false);
        fireball.SetActive(false);
        healthRecover.SetActive(false);
        wolfsiege.SetActive(false);
        dashingSoul.SetActive(false);
        dashingLight.SetActive(false);
    }

}
