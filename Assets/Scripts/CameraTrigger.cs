using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour 
{

    public bool Handheld = false;
    public bool MidZoom = false;
    public bool StrongZoom = false;
    public bool FastZoom = false;
    public bool FixInThisPosition = false;
    public bool FixFast = false;
    public bool ForceQuadrant = false;
    public string QuadrantToForce = "H";
    public float QuadrantCustomY = -191;

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
            
            if(MidZoom)
            {
                CameraSystem.Instance.MidZoom();
            }

            if(StrongZoom)
            {
                CameraSystem.Instance.StrongZoom();
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

            if(StrongZoom || MidZoom)
            {
                CameraSystem.Instance.DefaultZoom();
            }
        }
    }




}
