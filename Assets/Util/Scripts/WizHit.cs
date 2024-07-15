using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizHit : MonoBehaviour {

    private PlayerController wiz;
    private PlayerHealth wizHealthScript;
    private Vector2 startPosition;
    public AnimationCurve xHitCurve, xHitCurveLeft, yHitCurve;
    private bool started = false;
    private float timeElapsed = 0f;
    public bool running = false;

    private void Awake(){
        wiz = FindObjectOfType<PlayerController>();
        wizHealthScript = FindObjectOfType<PlayerHealth>();
    }

    private void Update(){
        if(!running){
            started = false;
            timeElapsed = 0f;
        } 
    }

    public void Hit(){
        wiz.State.Hit = true;
        running = true;
        wiz.DisablePlayerAttack();
        wiz.DisablePlayerControls();
        if(!started){
            started = true;
            timeElapsed = 0f;
            startPosition = transform.position;
        } else {
            timeElapsed += Time.deltaTime;
            // move rb position to current position at cur time
            if(wizHealthScript.difference.x > 0){
                wiz.Rigidbody2D.MovePosition(new Vector2(
                    startPosition.x + (xHitCurve.Evaluate(timeElapsed)),
                    startPosition.y + (yHitCurve.Evaluate(timeElapsed))
                ));
            } else {
                wiz.Rigidbody2D.MovePosition(new Vector2(
                    startPosition.x + (xHitCurveLeft.Evaluate(timeElapsed)),
                    startPosition.y + (yHitCurve.Evaluate(timeElapsed))
                ));
            }
        }
    }

    public void End(){
        running = false;
        wiz.EnablePlayerAttack();
        wiz.EnablePlayerControls();
        ReturnSoundBackToNormalOnAnimator();
        wiz.State.Hit = false;
    }

    public void ReturnSoundBackToNormalOnAnimator(){
        wizHealthScript.ReturnSoundToNormal();
    }
}
