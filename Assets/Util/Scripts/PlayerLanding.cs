using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class PlayerLanding : MonoBehaviour {

    public GameObject landingParticles;
    public GameObject regularParticles;
    public GameObject stoneParticles;
    public GameObject dirtParticles;
    public GameObject grassParticles;
    public GameObject autumnParticles;
    public Vector3 spawnParticlePosition;
    public MMFeedbacks landingFeedback;
    private PauseController pauseScript; // to prevent landing particles when resuming

    // FMOD -------
    public string LandingEvent = "";

    // SETADOS NO GroundController.CS no WIZ
    [FMODUnity.EventRef]
    public string Dirt = "";
    [FMODUnity.EventRef]
    public string Wood = "";
    [FMODUnity.EventRef]
    public string Stone = "";

    private bool _canPlay = true;

    private bool canPlayLandingParticles = false; // for avoiding landing particles on new scene enter

    public static PlayerLanding Instance;

    private void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 

        LandingEvent = Dirt;
        pauseScript = FindObjectOfType<PauseController>();
        StartCoroutine(EnableLandingParticlesAndSound());
        EnablePlay();
    }

    IEnumerator EnableLandingParticlesAndSound(){
        yield return new WaitForSeconds(0.5f);
        canPlayLandingParticles = true;
    }

    public void SetParticles(string particles){
        if(particles == "grass" && landingParticles != grassParticles){
            if(RoomConfigurations.Instance.GetProfileName() == "Autumn")
            {
                landingParticles = autumnParticles;
            }
            else
            {
                landingParticles = grassParticles;
            }
        } else if(particles == "stone" && landingParticles != stoneParticles){
            landingParticles = stoneParticles;
        } else if(particles == "dirt" && landingParticles != dirtParticles) {
            landingParticles = dirtParticles;
        } else if(particles == "wood" && landingParticles != regularParticles) {
            landingParticles = regularParticles;
        }else if(particles == "regular" && landingParticles != regularParticles) {
            landingParticles = regularParticles;
        }
    }

    private void EnablePlay(){
        _canPlay = true;
    }

    public void PlayLandingParticlesAndFeedbacks(){ // called from GroundController
        if(canPlayLandingParticles && !PlayerState.Instance.OnWater){
            if(_canPlay){
                _canPlay = false;
                Invoke("EnablePlay", 0.4f);
                ControllerRumble.Instance.LandingRumble();
                landingFeedback?.PlayFeedbacks();
                PlayerState.Instance.Jump = false;
                PlayerState.Instance.WallJump = false;
                FMODUnity.RuntimeManager.PlayOneShot(LandingEvent, transform.position);
                if(landingParticles != null){
                    if(landingParticles == grassParticles){
                        Instantiate(landingParticles, this.transform.position + spawnParticlePosition , Quaternion.identity);
                    } else {
                        Instantiate(landingParticles, this.transform.position , Quaternion.identity);
                    }
            }
            } else{
                return;
            }
        }
    }
}