using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraController : MonoBehaviour {
    private Camera mainCamera; 

    void Start() {
        MainCamera mainCameraScript = FindObjectOfType<MainCamera>();
        mainCamera = mainCameraScript.GetComponent<Camera>();
    }

    void LateUpdate() {
        print("Posição>> " + mainCamera.transform.position);
        transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 10f);
    }


}
