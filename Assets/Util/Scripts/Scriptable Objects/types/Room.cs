using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu] // podemos criar como objeto ao clicar com botao direito
public class Room : ScriptableObject, ISerializationCallbackReceiver {
    
     public bool seen;
     public bool saved;

     public void OnBeforeSerialize(){

     }

     public void OnAfterDeserialize(){
          
     }
}

