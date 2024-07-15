using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashWizSprite : MonoBehaviour {
    
    private SpriteFlashTool wizSprite;
    private PlayerController wiz;
    private bool flag = false;
    private bool flashNow = false;
    private bool isFlashing = false;

    void Awake(){
        wizSprite = GetComponent<SpriteFlashTool>();
        wiz = FindObjectOfType<PlayerController>();
    }

    void Update(){
        if(wiz.State.Invulnerable){
            flag = true;
            if(isFlashing == false){
                InvokeRepeating("Flash", 0f, 0.5f);
            }
        } else if(wiz.State.Invulnerable == false && flag == true) {
            isFlashing = false;
            flag = false;
            CancelInvoke("Flash");
        }
    }

    void Flash(){
        isFlashing = true;
        wizSprite.Flash();
    }
}
