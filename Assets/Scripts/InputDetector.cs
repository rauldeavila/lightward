using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDetector : MonoBehaviour {

    private bool _flag = false;
    public Animator MenuAnimator;

    void Update(){
        if(Inputs.Instance.AnyKeyWasPressed && _flag == false){
            _flag = true;
            if(Inputs.Instance.Keyboard){
                SFXController.Instance.Play("event:/game/00_ui/ui_spellselection");
                MenuAnimator.SetTrigger("keyboard");
            } else {
                SFXController.Instance.Play("event:/game/00_ui/ui_spellselection");
                MenuAnimator.SetTrigger("controller");
            }
        }
    }


}
