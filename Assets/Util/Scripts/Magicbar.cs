using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magicbar : MonoBehaviour {

    public BoolValue wiz_has_magic;
    public IntValue wiz_magic;

    public Image magicfillImage;
    public Image magicFXImage;
    private float fxSpeed = 0.01f;

    private void Update(){
        // magicfillImage.fillAmount = (wiz_magic.runTimeValue * 0.01f);

        // if(magicFXImage.fillAmount > magicfillImage.fillAmount){
        //     magicFXImage.fillAmount -= fxSpeed;
        // } else{
        //     magicFXImage.fillAmount = magicfillImage.fillAmount;
        // }

    }

}
