using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScriptableIntOnAnimator : MonoBehaviour
{
    public IntValue scriptableObject;

    public void Reduce1()
    {
        scriptableObject.runTimeValue = scriptableObject.runTimeValue - 1;
    }

    public void Increase1()
    {
        scriptableObject.runTimeValue = scriptableObject.runTimeValue - 1;
    }

    public void IncrementValue(int increment)
    {
        scriptableObject.runTimeValue = scriptableObject.runTimeValue + increment;
    }

}
