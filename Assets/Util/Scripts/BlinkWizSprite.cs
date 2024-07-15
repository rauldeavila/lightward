using System.Collections;
using System.Collections.Generic;
using UnityEngine;

                                                                        // THIS SCRIPT IS NOT WORKING!!!
public class BlinkWizSprite : MonoBehaviour {

    private Material mat;
    public bool startOnAwake = false;
    public float frequency = 1f;

    public static BlinkWizSprite Instance;

    void Awake() {
        if (Instance != null && Instance != this){ 
                Destroy(this); 
        } else { 
                Instance = this; 
        } 

        mat = GetComponent<Renderer>().material;
        if (startOnAwake) {
            BlinkSpriteEverySeconds(frequency);
        }

    }

    public void BlinkSpriteEverySeconds(float time) {
        InvokeRepeating("FlashSprite", 0f, time);
    }

    public void SingleBlink() {
        FlashSprite();
    }

    public void SingleBlinkIn1Sec(){
        Invoke("SingleBlink", 1f);
    }

    void FlashSprite() {
        mat.SetColor("HitEffectBlend", new Color(255, 88, 88, 255));
        Invoke("Unflash", 0.3f);
    }

    void Unflash(){
        mat.SetColor("HitEffectBlend",new Color(255, 255, 255, 255));
        
    }

}
