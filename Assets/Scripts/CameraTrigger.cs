using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour {

    public bool Handheld = false;
    public bool NearZoom = false;
    public bool FarZoom = false;

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.CompareTag("WizHitBox"))
        {
            if(Handheld)
            {
                CameraSystem.Instance.ToggleHandheld();
            }
            
            if(NearZoom)
            {
                CameraSystem.Instance.StrongZoom();
            } 
            else if(FarZoom)
            {
                CameraSystem.Instance.FarZoom();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        if(collider.CompareTag("WizHitBox"))
        {
            if(Handheld)
            {
                CameraSystem.Instance.ToggleHandheld();
            }

            if(NearZoom || FarZoom)
            {
                CameraSystem.Instance.DefaultZoom();
            } 
        }
    }




}
