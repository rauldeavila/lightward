using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    // private GameManager gameManager;

    // private float speed = 2f;
    // private bool returnTimeToNormal = false;
    // private float returnSpeedForPlayerHit = 5f;
    // private bool timeScaleIsZero = false;

    // private void Awake() {
    //     gameManager = FindObjectOfType<GameManager>();
    // }

    // private void Start() {
    //     returnTimeToNormal = false;
    // }

    // private void Update() {

    //     if(Time.timeScale != 1 && gameManager.State.Paused == false){
    //     }


    //     if(returnTimeToNormal){
            
    //         if(timeScaleIsZero){
    //             Time.timeScale = 0.1f;
    //             timeScaleIsZero = false;
    //         }


    //         if(Time.timeScale < 1f){
    //             Time.timeScale += Time.deltaTime * speed;
    //         } else {
    //             gameManager.State.TimeStopped = false;
    //             Time.timeScale = 1f;
    //             returnTimeToNormal = false;
    //         }
    //     }
    // }

    // public void StopTimeWhenHit(){
    //     speed = returnSpeedForPlayerHit;
    //     timeScaleIsZero = true;

 
    //     StopCoroutine(StartTimeAgain(0));
    //     StartCoroutine(StartTimeAgain(0.1f));


    //     Time.timeScale = 0f;
    //     gameManager.State.TimeStopped = true;
    // }


    // public void StopTime(float newTimeScale, int timeUntilReturnToNormal, float delay){

    //     speed = timeUntilReturnToNormal;

    //     if(delay > 0){
    //         StopCoroutine(StartTimeAgain(delay));
    //         StartCoroutine(StartTimeAgain(delay));
    //     } else {
    //         returnTimeToNormal = true;
    //     }

    //     Time.timeScale = newTimeScale;
    //     gameManager.State.TimeStopped = true;
    // }

    // IEnumerator StartTimeAgain(float amount){
    //     yield return new WaitForSecondsRealtime(amount);
    //     returnTimeToNormal = true;
    // }

    // public void NormalTime(){
    //     Time.timeScale = 1;
    // }


}
