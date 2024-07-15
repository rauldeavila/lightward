using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu] 
public class IntValue : ScriptableObject, ISerializationCallbackReceiver {

    public int initialValue;
    public int runTimeValue;
    public int maxValue;

    public void OnBeforeSerialize() {
        initialValue = runTimeValue;
        maxValue = maxValue;
    }

    public void OnAfterDeserialize() {
        runTimeValue = initialValue;
        maxValue = maxValue;
    }
}