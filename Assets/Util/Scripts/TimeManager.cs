using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public static TimeManager Instance;
    public FloatValue savefile_timestamp;

    private TimeSpan timePlaying;
    private bool timerGoing;
    public bool runTimerOnStart = false;
    public TextMeshProUGUI speedrunClock;

    private float elapsedTime;

    private bool _waiting = false; // for hit stop

    void Awake(){
        Instance = this;
        timerGoing = false;
    }

    void Start(){
        if(runTimerOnStart){
            BeginTimer();
        }
    }

    public void Stop(float duration){
        // if(_waiting){
        //     return;
        // }
        // Time.timeScale = 0.1f;
        // StartCoroutine(Wait(duration));
    }

    IEnumerator Wait(float duration){
        _waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        _waiting = false;
    }

    public void BeginTimer(){
        
        timerGoing = true;
        elapsedTime = savefile_timestamp.initialValue;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer(){

        savefile_timestamp.initialValue = elapsedTime;
 
        timerGoing = false;

    }

    private IEnumerator UpdateTimer(){
        while(timerGoing){
            elapsedTime += Time.deltaTime; // elapsed time = time in seconds
            savefile_timestamp.initialValue = elapsedTime;
            if(speedrunClock != null){
                speedrunClock.text = TimeSpan.FromSeconds(elapsedTime).ToString(@"hh\:mm\:ss\.ff");
            }
            yield return null;
        }
    }

    private void OnDestroy() {
        if(runTimerOnStart){
            EndTimer();
        }
    }

    // TimeSpan.FromSeconds(SaveManager.instance.wizObjects.savefile_timestamp.initialValue).ToString(@"hh\:mm");
    // TimeSpan.FromSeconds(SaveManager.instance.wizObjects.savefile_timestamp.initialValue).ToString(@"hh\:mm\:ss");
    // TimeSpan.FromSeconds(SaveManager.instance.wizObjects.savefile_timestamp.initialValue).ToString(@"hh\:mm\:ss\.ff");

}
