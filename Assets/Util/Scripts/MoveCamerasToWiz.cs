using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MoveCamerasToWiz : MonoBehaviour {


    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;
    public GameObject wiz;

    [Button(ButtonSizes.Gigantic)]
    private void MoveCamerasToWizPos() {
        cam1.transform.position = new Vector3(wiz.transform.position.x, wiz.transform.position.y, -2);
        cam2.transform.position = new Vector3(wiz.transform.position.x, wiz.transform.position.y, -2);
        cam3.transform.position = new Vector3(wiz.transform.position.x, wiz.transform.position.y, -2);
    }
    
    [Button(ButtonSizes.Large), GUIColor(0.4f, 0.8f, 1)]
    private void ResetCameraPos(){
        this.transform.position = Vector3.zero;
    }






}
