using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ToggleObjectWithColliders : MonoBehaviour {

    public bool IfScriptableTrueIgnoreTriggers = false;
    public bool IfScriptableFalseIgnoreTriggers = false;
    public bool ForceDisableBasedOnScriptable = false;
    public bool ForceEnableBasedOnScriptable = false;
    public BoolValue Scriptable;

    public GameObject TheObject;
    public bool DisableOnTriggerExit;
    public bool EnableOnTriggerExit;
    public bool DisableOnTriggerEnter;
    public bool EnableOnTriggerEnter;

    void Start(){
        if(ForceDisableBasedOnScriptable){
            if((IfScriptableFalseIgnoreTriggers && Scriptable.runTimeValue == false) || (IfScriptableTrueIgnoreTriggers && Scriptable.runTimeValue == true)){
                TheObject.SetActive(false);
            }
        } else if(ForceEnableBasedOnScriptable){
            if((IfScriptableFalseIgnoreTriggers && Scriptable.runTimeValue == false) || (IfScriptableTrueIgnoreTriggers && Scriptable.runTimeValue == true)){
                TheObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if((IfScriptableFalseIgnoreTriggers && Scriptable.runTimeValue == false) || (IfScriptableTrueIgnoreTriggers && Scriptable.runTimeValue == true)){
            return;
        } else {
            if(collider.CompareTag("WizHitBox")){
                if(DisableOnTriggerEnter){
                    TheObject.SetActive(false);
                }
                if(EnableOnTriggerEnter){
                    TheObject.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if((IfScriptableFalseIgnoreTriggers && Scriptable.runTimeValue == false) || (IfScriptableTrueIgnoreTriggers && Scriptable.runTimeValue == true)){
            return;
        } else {
            if(collider.CompareTag("WizHitBox") && !PlayerController.Instance.AnimatorIsPlaying("dodge")){
                if(DisableOnTriggerExit){
                    TheObject.SetActive(false);
                }
                if(EnableOnTriggerExit){
                    TheObject.SetActive(true);
                }
            }
        }
    }


}
