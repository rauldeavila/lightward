using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour {

    public GameObject keyboardButton;
    public GameObject joystickButton;
 
    private void Update(){
        
        if(PlayerController.Instance.lastUsedInputIsKeyboard()){
            joystickButton.SetActive(false);
            keyboardButton.SetActive(true);
        } else {
            keyboardButton.SetActive(false);
            joystickButton.SetActive(true);
        } 

    }

}
