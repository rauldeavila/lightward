using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleSpeedrunMode : MonoBehaviour {

    int speedrunMode = 0;

    public GameObject speedrunClock;

    void Start(){
        speedrunMode = PlayerPrefs.GetInt("SpeedrunMode", 0);
    }

    void Update(){
        if(PlayerPrefs.GetInt("SpeedrunMode") == 1){
            speedrunClock.SetActive(true);
        } else{
            speedrunClock.SetActive(false);
        }
    }

}
