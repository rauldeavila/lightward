using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExternalWorldSpaceCanvas : MonoBehaviour {

    void OnEnable(){
        EventSystem.current.SetSelectedGameObject(null);
    }

    void OnDisable(){
        EventSystem.current.SetSelectedGameObject(null);
    }

}
