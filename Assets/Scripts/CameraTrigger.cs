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
    public bool Campfire = false;

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if(PlayerState.Instance.JustDodged)
        {
            return;
        }
        if(collider.CompareTag("WizHitBox"))
        {
            if(FixInThisPosition)
            {
                Debug.Log($"FIX IN THIS POSITION: {this.transform.position}");
                CameraSystem.Instance.SetLookAt(this.transform);
            }
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
        if(PlayerState.Instance.JustDodged)
        {
            return;
        }
        if(collider.CompareTag("WizHitBox"))
        {
            if(FixInThisPosition)
            {
                // print("LOOK AT HERO!");
                CameraSystem.Instance.SetLookAtHero();
            }
            if(Handheld)
            {
                CameraSystem.Instance.ToggleHandheld();
            }

            if(NearZoom || FarZoom)
            {
                if(!Campfire)
                {
                    CameraSystem.Instance.DefaultZoom();
                }
            } 
        }
    }




}
