using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeTextUIController : MonoBehaviour {

    public Image ThisImage;
    public Sprite RegularText;
    public Sprite FreezingText;

    void Update(){
        // handles if heart is filled or not
        if(PlayerState.Instance.Freezing){ // freezing
            ThisImage.sprite = FreezingText;
        } else { // default
            ThisImage.sprite = RegularText;
        }
    }

}
