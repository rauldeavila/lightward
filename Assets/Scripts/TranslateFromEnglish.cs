using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TranslateFromEnglish : MonoBehaviour {

    private TextMeshProUGUI _thisTextComponent;
    
    void Awake(){
        _thisTextComponent = GetComponent<TextMeshProUGUI>();
    }

    void Update(){
        if(PlayerPrefs.GetInt("Language") == 1){
            if(_thisTextComponent.text == "Lostwind Woods"){
                _thisTextComponent.text = "Bosque dos Ventos Uivantes";
            } else if(_thisTextComponent.text == "Soulfields"){
                _thisTextComponent.text = "Pátio das Almas";
            } else if(_thisTextComponent.text == "Catacombs"){
                _thisTextComponent.text = "Catacumbas";
            }

        }
    }

}
