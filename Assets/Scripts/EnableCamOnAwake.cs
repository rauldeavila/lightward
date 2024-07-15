using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCamOnAwake : MonoBehaviour {

    public GameObject Cam0;
    public GameObject Cam1;
    public GameObject Cam2;
    public GameObject Cam3;
    public GameObject Cam4;
    public GameObject Cam5;

    void Awake(){
        DisableAllCameras();
        switch (ScriptableObjectsManager.Instance.GetScriptableObject<IntValue>("game_cam").runTimeValue) {
            case 0:
                Cam0.SetActive(true);
                break;
            case 1:
                Cam1.SetActive(true);
                break;
            case 2:
                Cam2.SetActive(true);
                break;
            case 3:
                Cam3.SetActive(true);
                break;
            case 4:
                Cam4.SetActive(true);
                break;
            case 5:
                Cam5.SetActive(true);
                break;
            default:
                break;
        }
    }

    void DisableAllCameras(){
        if(Cam0 != null)
            Cam0.SetActive(false);
        if(Cam1 != null)
            Cam1.SetActive(false);
        if(Cam2 != null)
            Cam2.SetActive(false);
        if(Cam3 != null)
            Cam3.SetActive(false);
        if(Cam4 != null)
            Cam4.SetActive(false);
        if(Cam5 != null)
            Cam5.SetActive(false);
    }


}
