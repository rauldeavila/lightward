using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSaveOnAnimator : MonoBehaviour {

    public void AutoSave(){
        SaveManager.instance.AutoSave();
    }



}
