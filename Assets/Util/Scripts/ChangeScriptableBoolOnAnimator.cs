using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScriptableBoolOnAnimator : MonoBehaviour
{

    public BoolValue scriptableObject;

    public void ChangeScriptableBoolToTrue(){
        scriptableObject.runTimeValue = true;
        scriptableObject.initialValue = true;
    }

    public void ChangeScriptableBoolToFalse(){
        scriptableObject.runTimeValue = false;
        scriptableObject.initialValue = false;
    }

    public void SetScriptableToTrueInSeconds(float _delay)
    {
        Invoke("ChangeScriptableBoolToTrue", _delay);
    }

    public void SetScriptableToFalseInSeconds(float _delay)
    {
        Invoke("ChangeScriptableBoolToFalse", _delay);
    }
}
