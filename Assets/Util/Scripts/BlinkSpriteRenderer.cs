using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteFlashTool))]
public class BlinkSpriteRenderer : MonoBehaviour {

    // private Material mat;
    private SpriteFlashTool spriteFlashTool;
    public bool startOnAwake = false;
    public float frequency = 1f;

    void Awake() {
        // mat = GetComponent<Renderer>().material;
        spriteFlashTool = GetComponent<SpriteFlashTool>();
        if (startOnAwake) {
            BlinkSpriteEverySeconds(frequency);
        }

    }

    public void BlinkLoopEachSecond()
    {
        InvokeRepeating("FlashSprite", 0f, 1f);
    }

    public void StopBlinking()
    {
        CancelInvoke();
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
        spriteFlashTool.Flash();
        // mat.SetFloat("_HitEffectBlend", 1f);
        // Invoke("Unhit", 0.3f);
    }

    void Unhit(){
        // mat.SetFloat("_HitEffectBlend", 0f);
    }

}
