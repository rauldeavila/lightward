using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeOnAnimator : MonoBehaviour {

    public void CamBreak(){
        CameraSystem.Instance.ShakeCamera(1);
    }
    
    public void SoftZoom(){
        print("HERE THERES NO MORE MEDIUM ZOOM ANYMORE - FIX THIS TOO!");
        CameraSystem.Instance.StrongZoom();
    }
    
    public void StrongZoomLevel1(){
        CameraSystem.Instance.StrongZoom();
    }
    
    public void StrongZoomLevel2(){
        CameraSystem.Instance.StrongZoom();
    }
    
    public void InsideLightShake(){
        CameraSystem.Instance.ShakeCamera(2);
    }
    
    public void ZoomOutFast(){
        CameraSystem.Instance.DefaultZoom();
    }

}
