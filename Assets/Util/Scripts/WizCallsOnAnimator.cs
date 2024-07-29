using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WizCallsOnAnimator : MonoBehaviour {

    private PlayerController wiz;
    private PlayerCombat combatScript;
    private GameObject crossfadePanel;
    private HealthUIController healthPanel;

    public GameObject landingParticles;
    public GameObject rollParticlesLeft;
    public GameObject rollParticlesRight;

    FMOD.Studio.Bus Music;

    [FMODUnity.EventRef]
    public string diveSound = "";

    [FMODUnity.EventRef]
    public string wallSlideSound = "";
    private FMOD.Studio.EventInstance wallSlideInstance;

    [FMODUnity.EventRef]
    public string wakingUpGrassStaff = "";

    [FMODUnity.EventRef]
    public string staffLightTurningOn= "";

    [FMODUnity.EventRef]
    public string wakingUpGrass= "";

    [FMODUnity.EventRef]
    public string dashingLight= "";

    public ParticleSystem landingWater;


    private bool _up10 = false;
    private bool _up15 = false;
    private bool _up8 = false;

    public static WizCallsOnAnimator Instance;

    private void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
        wiz = FindObjectOfType<PlayerController>();
        combatScript = FindObjectOfType<PlayerCombat>();
        healthPanel = FindObjectOfType<HealthUIController>();
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/music");
    }

    public void Update(){
        if(_up10){
            wiz.Rigidbody2D.AddForce(Vector2.up * 0.10f);
        } else if(_up8){
            wiz.Rigidbody2D.AddForce(Vector2.up * 0.8f);
        } else if(_up15){
            wiz.Rigidbody2D.AddForce(Vector2.up * 0.15f);
        }
    }

    public void AcquiringEight(){
        _up15 = false;
        _up10 = false;
        _up8 = true;
    }

    public void AcquiringFifteen(){
        _up8 = false;
        _up10 = false;
        _up15 = true;
    }

    public void AcquiringHeartContainer(){
        _up8 = false;
        _up15 = false;
        _up10 = true;
    }

    public void AcquiringHeartContainerStop(){
        _up8 = false;
        _up15 = false;
        _up10 = false;
    }
    
    public void SetGravityToZero(){
        wiz.SetGravityToZero();
    }

    public void SetGravityToOne(){
        wiz.SetGravityToOne();
    }


    public void OnDisable(){
        StopWallSlideSound();
    }

    public void SetInvulnerabilityToOff(){
        wiz.State.Invulnerable = false;
    }

    public void RestartGameKilled(){
        CrossfadePanelController.Instance.FadeOut();
        SaveSystem.Instance.LoadGame();
        Time.timeScale = 1f;
        if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("game_new_game").runTimeValue){
            SceneManager.LoadScene("forest_0");
        } else {
            ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("game_loading_savefile", true);
            SceneManager.LoadScene(ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("game_saved_scene").initialValue);
        }
    }

    public void CoyoteJumpOn(){
        GroundController.Instance.CoyoteJump = true;
    }

    public void CoyoteJumpOff(){
        GroundController.Instance.CoyoteJump = false;
    }

    public void DisablePlayerCommands(){
        wiz.DisablePlayerAttack();
        wiz.DisablePlayerControls();
        wiz.FreezeRigidbody();
    }

    public void EnablePlayerCommands(){
        wiz.EnablePlayerAttack();
        wiz.EnablePlayerControls();
        wiz.UnfreezeRigidbody();
    }

    public void WizHitCam(){
        CameraSystem.Instance.ShakeCamera(2);
    }

    public void LookDownForFalling(){
        CameraSystem.Instance.LookDownFalling();
    }

    public void FadeOutPanel(){
        crossfadePanel = GameObject. FindGameObjectWithTag("CrossfadePanel");
        crossfadePanel.GetComponent<Animator>().SetTrigger("fadeout");
    }


    public void StopMusicBus(){
        Music.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void FreezeRigidbody(){
        wiz.FreezeRigidbody();
    }

    public void UnfreezeRigidbody(){
        wiz.UnfreezeRigidbody();
    }

    public void PlayLandingWaterParticles(){
        landingWater.Play();
    }

    public void PlayAttackSoundFX(){
        combatScript.PlayAttackSound();
    }

    public void PlayDiveSound(){
        FMODUnity.RuntimeManager.PlayOneShot(diveSound, transform.position);
    }


    public void StartWallSlideSound(){
        wallSlideInstance = FMODUnity.RuntimeManager.CreateInstance(wallSlideSound);
        wallSlideInstance.start();
    }

    public void StopWallSlideSound(){
        wallSlideInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PlayStaffLightSound(){
        FMODUnity.RuntimeManager.PlayOneShot(staffLightTurningOn, transform.position);
    }

    public void PlayDashingLightSound(){
        FMODUnity.RuntimeManager.PlayOneShot(dashingLight, transform.position);
    }

    public void PlayStaffGrassSound(){
        FMODUnity.RuntimeManager.PlayOneShot(wakingUpGrassStaff, transform.position);
    }

    public void PlayWakingUpGrass(){
        FMODUnity.RuntimeManager.PlayOneShot(wakingUpGrass, transform.position);
    }


    public void EnableLook(){
        wiz.canLook = true;
    }

    public void ItsAHardFall(){
        wiz.Animator.SetBool("hardFall", true);
    }

    public void ItsNotAHardFallYet()
    {
        wiz.Animator.SetBool("hardFall", false);
    }

    public void InvulnerableOn(){
        PlayerHealth.Instance.InvulnerableOn();
    }

    public void InvulnerableOff(){
        PlayerHealth.Instance.InvulnerableOff();
    }

    public void RollBeginParticles(){
        if(PlayerState.Instance.FacingRight){
            Instantiate(rollParticlesLeft, new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z), Quaternion.identity);
        } else {
            Instantiate(rollParticlesRight, new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z), Quaternion.identity);
        }
    }

    public void RollMidParticles(){
        if(PlayerState.Instance.FacingRight){
            Instantiate(rollParticlesRight, new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z), Quaternion.identity);
        } else {
            Instantiate(rollParticlesLeft, new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z), Quaternion.identity);
        }
    }

    public void BackdashInitialParticles(){
        if(PlayerState.Instance.FacingRight){
            Instantiate(rollParticlesRight, new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z), Quaternion.identity);
        } else {
            Instantiate(rollParticlesLeft, new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z), Quaternion.identity);
        }
    }

    public void RollEndingParticles(){
        Instantiate(landingParticles, new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z), Quaternion.identity);
    }

    public void DodgeToFallAllowed(){
        wiz.Animator.SetBool("dodgeToFallAllowed", true);
    }

    public void DodgeToFallForbidden(){
        wiz.Animator.SetBool("dodgeToFallAllowed", false);
    }

    public void LandingParticlesAndFeedbacks(){
        PlayerLanding.Instance.PlayLandingParticlesAndFeedbacks();
    }

    public void ResetWallJumpingFromBehaviour(){
        Invoke("SetWallJumpingToFalse", 1f);
    }

    public void SetWallJumpingToFalse(){
        wiz.Animator.SetBool("wallJumping", false);
    }

    public void HardLandingSFX(){
        SFXController.Instance.Play("event:/char/wiz/hard_landing");
    }

    public void RollRumbleController(){
        ControllerRumble.Instance.RollRumble();
    }
    public void LandingRumbleController(){
        ControllerRumble.Instance.LandingRumble();
    }

    public void StartBedtimeCoroutine()
    {
        StartCoroutine(WaitAndShowButtonPrompt());
    }

    private IEnumerator WaitAndShowButtonPrompt()
    {
        yield return new WaitForSeconds(10f);
        if (PlayerController.Instance.Animator.GetCurrentAnimatorStateInfo(0).IsName("bedtime")) // Replace with your animation state name
        {
            DisplayButtonOnScreen.Instance.ShowButtonPrompt("MoveUp", "Wake up");
        }
    }


}
