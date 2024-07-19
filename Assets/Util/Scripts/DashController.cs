using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MoreMountains.Feedbacks;


public class DashController : MonoBehaviour {
    
   public static DashController Instance;

    private GameManager gameManager;
    private PlayerController controller;
    private Move move;
    private PlayerCombat combat;
    private int facingDirection;
    public float dashDistance = 20f;
    public float rollSmallerDistance = 2f;
    public float rollDistance = 15f;
    public float rollBiggerDistance = 25f;
    public float rollMuchBiggerDistance = 29f;
    public float timeBetweenDashes = 1f;
    public float dashDuration = 0.4f;
    public float dodgeDuration = 0.6f;
    public float rollImpulseWaitingTime = 0.2f;
    public ParticleSystem dashParticles;
    public ParticleSystem dashPixelTrail;
    public ParticleSystem dashDustIfGrounded;
    public MMFeedbacks dashFeedback;

    [FMODUnity.EventRef]
    public string DashSound = "";

    private float defaultGravityScale;

    private void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 


        combat = GetComponent<PlayerCombat>();
    }


    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
        controller = GetComponent<PlayerController>();
        move = GetComponent<Move>();
        defaultGravityScale = GetComponent<Rigidbody2D>().gravityScale;
    }



    public void DashPressed(){
        if(!Move.Instance.IsNoClipActive)
        {
            if(StateController.Instance.CanDash){
                StateController.Instance.CanDash = false;
                StartCoroutine("Dash");
            }
        }
    }

    IEnumerator Dash(){
        PlayerState.Instance.LeftLightAndDidntLand = false; // To avoid multiple dash
        if(controller.AnimatorIsPlaying("wall_sliding")){
            move.Flip();
        }

        // TODO: FIX THIS CONDITIONS TO AVOID DASHING WHEN WANTING TO BACKDASH ON LEDGES.
        if(!PlayerController.Instance.AnimatorIsPlaying("dash")
        && !PlayerController.Instance.AnimatorIsPlaying("dodge")
        && !PlayerController.Instance.AnimatorIsPlaying("backdash"))
        {
            if(
            PlayerState.Instance.Grounded && 
            !PlayerState.Instance.Jump &&
            (PlayerState.Instance.FacingRight && GroundController.Instance.RollRight) ||
            (!PlayerState.Instance.FacingRight && GroundController.Instance.RollLeft))
            {
                Jump.Instance.Pogoed = false;
                if(PlayerState.Instance.Grounded && StateController.Instance.CanDodge){  // DODGE

                    if((!PlayerState.Instance.FacingRight && Inputs.Instance.HoldingLeftArrow) || (PlayerState.Instance.FacingRight && Inputs.Instance.HoldingRightArrow))
                    {
                        // front roll
                        PlayerController.Instance.Animator.SetBool("dodge", true);
                        controller.Animator.Play("dodge");
                        controller.State.BeingKnockedBack = false;
                        PlayerState.Instance.Dodge = true;
                        SFXController.Instance.Play("event:/char/wiz/rolling");

                        if(controller.State.FacingRight){
                            facingDirection = 1;
                        } else {
                            facingDirection = -1;
                        }

                        yield return new WaitForSeconds(rollImpulseWaitingTime);

                        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        float gravity = GetComponent<Rigidbody2D>().gravityScale;
                        GetComponent<Rigidbody2D>().gravityScale = 0;
                        if(Raycasts.Instance.StrongerDodge){
                            // print("Stronger Dodge!");
                            GetComponent<Rigidbody2D>().AddForce(new Vector2(rollBiggerDistance * facingDirection, 0f), ForceMode2D.Impulse);
                        } else if(Raycasts.Instance.MuchStrongerDodge){
                            // print("Much Stronger Dodge!");
                            GetComponent<Rigidbody2D>().AddForce(new Vector2(rollMuchBiggerDistance * facingDirection, 0f), ForceMode2D.Impulse);
                        } else if(Raycasts.Instance.LighterDodge){
                            // print("Lighter Dodge!");
                            GetComponent<Rigidbody2D>().AddForce(new Vector2(rollSmallerDistance * facingDirection, 0f), ForceMode2D.Impulse);
                        } else {
                            // print("Default Dodge!");
                            GetComponent<Rigidbody2D>().AddForce(new Vector2(rollDistance * facingDirection, 0f), ForceMode2D.Impulse);
                        }
                        

                        yield return new WaitForSeconds(dodgeDuration);
                        if(PlayerState.Instance.Dodge){
                            controller.State.Dodge = false;
                            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                            GetComponent<Rigidbody2D>().gravityScale = defaultGravityScale;
                        }
                        PlayerController.Instance.Animator.SetBool("dodge", false);
                    }
                    else
                    {

                        PlayerController.Instance.Animator.SetBool("backdash", true);
                        controller.Animator.Play("backdash");
                        controller.State.BeingKnockedBack = false;
                        PlayerState.Instance.Dodge = true;
                        // TODO: BACK DASH SOUND EFFECT
                        // SFXController.Instance.Play("event:/char/wiz/rolling");

                        if(!controller.State.FacingRight){
                            facingDirection = 1;
                        } else {
                            facingDirection = -1;
                        }

                        yield return new WaitForSeconds(0.1f);

                        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        float gravity = GetComponent<Rigidbody2D>().gravityScale;
                        GetComponent<Rigidbody2D>().gravityScale = 0;
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(rollDistance * facingDirection, 0f), ForceMode2D.Impulse);
                        

                        yield return new WaitForSeconds(0.3f );
                        if(PlayerState.Instance.Dodge){
                            controller.State.Dodge = false;
                            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                            GetComponent<Rigidbody2D>().gravityScale = defaultGravityScale;
                        }
                        PlayerController.Instance.Animator.SetBool("backdash", false);
                    }
                } else { // DASH
                    if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("wiz_dash").runTimeValue == true){
                        ControllerRumble.Instance.DashRumble();
                        controller.Animator.Play("dash");
                        controller.State.BeingKnockedBack = false;
                        controller.State.Dash = true;
                        FMODUnity.RuntimeManager.PlayOneShot(DashSound, transform.position);
                        dashFeedback?.PlayFeedbacks();
                        dashPixelTrail.Play();
                        if(controller.State.FacingRight){
                            facingDirection = 1;
                        } else {
                            facingDirection = -1;
                        }
                        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        float gravity = GetComponent<Rigidbody2D>().gravityScale;
                        GetComponent<Rigidbody2D>().gravityScale = 0;
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(dashDistance * facingDirection, 0f), ForceMode2D.Impulse);
                        yield return new WaitForSeconds(dashDuration);
                        dashFeedback?.StopFeedbacks();
                        controller.State.Dash = false;
                        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        GetComponent<Rigidbody2D>().gravityScale = defaultGravityScale;
                    }
                }
            } else { // DASH
                if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("wiz_dash").runTimeValue == true){
                    ControllerRumble.Instance.DashRumble();
                    controller.Animator.Play("dash");
                    controller.State.BeingKnockedBack = false;
                    controller.State.Dash = true;
                    FMODUnity.RuntimeManager.PlayOneShot(DashSound, transform.position);
                    dashFeedback?.PlayFeedbacks();
                    dashPixelTrail.Play();
                    if(controller.State.FacingRight){
                        facingDirection = 1;
                    } else {
                        facingDirection = -1;
                    }
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    float gravity = GetComponent<Rigidbody2D>().gravityScale;
                    GetComponent<Rigidbody2D>().gravityScale = 0;
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(dashDistance * facingDirection, 0f), ForceMode2D.Impulse);
                    yield return new WaitForSeconds(dashDuration);
                    dashFeedback?.StopFeedbacks();
                    controller.State.Dash = false;
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    GetComponent<Rigidbody2D>().gravityScale = defaultGravityScale;
                }
            }

        }
    }

    public void StopDashing(){
        controller.State.Dash = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().gravityScale = defaultGravityScale;
    }
    
}
