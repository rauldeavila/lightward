using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleTest : MonoBehaviour {

    public float TimeScaleValue = 0f;

    void Start(){
        print("Setting time scale to " + TimeScaleValue);
        Time.timeScale = TimeScaleValue;
    }

    void Update() {
        print("Time.timeScale: " + Time.timeScale);
    }

}
