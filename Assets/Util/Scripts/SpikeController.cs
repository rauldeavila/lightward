using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour {


    private GameObject crossfadePanel;

    [HideInInspector]
    public float respawn_x = 0f; // set up on child script SpikeRespawn.cs
    [HideInInspector]
    public float respawn_y = 0f; // set up on child script Spikerespawn.cs
    [HideInInspector]
    public bool respawnFacingRight;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){

            CameraSystem.Instance.ShakeCamera(2);

            PlayerState.Instance.Jump = false;
            PlayerState.Instance.BeingKnockedBack = true;
            PlayerController.Instance.KnockWizBack(0f, 8f);
            PlayerStats.Instance.DecreaseHealth();
            if(PlayerStats.Instance.GetCurrentHealth() <= 0){
                PlayerController.Instance.Animator.Play("death");
                return;
            } else {
                StartCoroutine(Fadeout());
            }

        }

    }
    IEnumerator Fadeout(){
        yield return new WaitForSeconds(0.3f);
       crossfadePanel = GameObject. FindGameObjectWithTag("CrossfadePanel");
       crossfadePanel.GetComponent<Animator>().SetTrigger("fadeout");
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn(){
        yield return new WaitForSeconds(0.5f);
        PlayerState.Instance.Grounded = true;
        if(!respawnFacingRight && PlayerState.Instance.FacingRight){
            Move.Instance.Flip();
        } else if(respawnFacingRight && !PlayerState.Instance.FacingRight){
            Move.Instance.Flip();
        }
        PlayerController.Instance.Animator.Play("respawn");
        crossfadePanel.GetComponent<Animator>().SetTrigger("fadein");
        PlayerController.Instance.SetPlayerPosition(respawn_x, respawn_y);
        respawnFacingRight = false;
    }



}
