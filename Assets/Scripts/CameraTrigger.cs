using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour 
{

    public bool Handheld = false;
    public bool NearZoom = false;
    public bool FarZoom = false;
    public bool ZoomFast = false;
    public bool FixInThisPosition = false;
    public bool FixFast = false;

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.CompareTag("WizHitBox"))
        {
            if(FixInThisPosition)
            {
                CameraSystem.Instance.FixedXPosition = this.transform.position.x;
                CameraSystem.Instance.FixedYPosition = this.transform.position.y;
                CameraSystem.Instance.SwitchToFixedCam(FixFast);
                // CameraSystem.Instance.FixCamAt(new Vector3(this.transform.position.x, this.transform.position.y, CameraSystem.Instance.transform.position.z), FixFast);
            }
            if(Handheld)
            {
                CameraSystem.Instance.ToggleHandheld();
            }
            
            if(NearZoom)
            {
                CameraSystem.Instance.StrongZoom(ZoomFast);
            } 
            else if(FarZoom)
            {
                CameraSystem.Instance.FarZoom();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        if(FixInThisPosition)
        {
            // Debug.Log("MOVING CAMERA!");
            CameraSystem.Instance.SwitchToWizCam();
        }
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
