using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOnAnimator : MonoBehaviour {

    public void DefaultZoom(){
        CameraSystem.Instance.DefaultZoom();
    }
    public void MediumZoom(){
        print("THERES NO MEDIUM ZOOM ANYMORE... FIX THIS!");
        CameraSystem.Instance.StrongZoom();
    }

    public void StrongZoom() {
        CameraSystem.Instance.StrongZoom();
	}

    public void ToggleHandheld() {
        CameraSystem.Instance.ToggleHandheld();
	}

    public void ToggleEarthquake() {
        CameraSystem.Instance.ToggleEarthquake();
	}

    public void ShakeCamera(){
        CameraSystem.Instance.ShakeCamera();
    }

}
