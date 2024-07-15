using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StringValue : ScriptableObject, ISerializationCallbackReceiver  {
    
     public string initialValue;
     public string runTimeValue;

     public void OnBeforeSerialize() {
        initialValue = runTimeValue;
    }

    public void OnAfterDeserialize() {
        runTimeValue = initialValue;
    }
     
}
