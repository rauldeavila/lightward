using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMenuButonOnAwake : MonoBehaviour {

    public bool slider;
    public Slider startWithThisSliderSelected;
    public Button startWithThisButtonSelected;
    public Button localizationButton;
    private bool started = false;

    void Start(){
        if(started == false){
            if(slider){
                startWithThisSliderSelected.Select();
            } else{
                if(PlayerPrefs.GetInt("Language", 0) == 0){
                    startWithThisButtonSelected.Select();
                } else if(PlayerPrefs.GetInt("Language", 0) == 1){
                    if(localizationButton != null){
                        localizationButton.Select();
                    } else {
                        startWithThisButtonSelected.Select();
                    }
                }
            }
        }
    }

    private void Update() {
        started = true;
    }

    private void OnEnable() {
        if(started == true){
            StartCoroutine(Activate());
        }
    }

    IEnumerator Activate(){
        yield return new WaitForSecondsRealtime(0.05f);
        if(slider){
            startWithThisSliderSelected.Select();
        } else{
            startWithThisButtonSelected.Select();
        }
    }

}
