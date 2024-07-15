using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu] 
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver {
    
     public float initialValue;
     public float runTimeValue;
     public float maxValue;

     public void OnBeforeSerialize() {
          initialValue = runTimeValue;
          maxValue = maxValue;
     }

     public void OnAfterDeserialize() {
          runTimeValue = initialValue;
          maxValue = maxValue;
     }

}
