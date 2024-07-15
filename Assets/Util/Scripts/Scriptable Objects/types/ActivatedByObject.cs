using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu] // podemos criar como objeto ao clicar com botao direito
public class ActivatedByObject : ScriptableObject {

    public bool initialValue;
    public bool runTimeValue;
    public bool beingActivated;

}
