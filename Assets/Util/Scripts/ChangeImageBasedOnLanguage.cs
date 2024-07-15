using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageBasedOnLanguage : MonoBehaviour {

    public Image thisImage;
    public Sprite englishFrame;
    public Sprite portugueseFrame;

    void Update(){
        if(PlayerPrefs.GetInt("Language", 0) == 0){ // EN
            thisImage.sprite = englishFrame;
        } else if(PlayerPrefs.GetInt("Language", 0) == 1){ // PTBR
            thisImage.sprite = portugueseFrame;
        }        
    }
}
