using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroundController : MonoBehaviour {

    private bool _WizIsMovingUp = false;
    public float startWalledState = 0.6f;

    private PlayerController controller;
    [HideInInspector]
    public Vector3 groundCheckAdjustmentForNewSprites;
    private FootstepsSoundsOnAnimator footsteps;
    private PlayerLanding landing;
    public ParticleSystem smokesDashingSoul;
    public ParticleSystem smokesDashingSoul2;
    public ParticleSystem smokesDashingSoul3;


    public LayerMask attackLayer;
    public LayerMask groundLayer;
    public LayerMask anyLayer;
    public LayerMask waterLayer;

    public bool RightFootGrounded = false;
    public bool LeftFootGrounded = false;
    public bool onRightWall = false;
    public bool onLeftWall = false;
    public bool downRightWall = false;
    public bool downLeftWall = false;
    public bool upRightWall = false;
    public bool upLeftWall = false;
    public bool onRoof = false;
    private bool onWater = false;


    public bool RollRight = false;
    public bool RollLeft = false;

    public Vector3 RightRollOffset;
    public float RightRollRectWidth;
    public float RightRollRectHeight;

    public Vector3 LeftRollOffset;
    public float LeftRollRectWidth;
    public float LeftRollRectHeight;




    public Vector3 roofOffset;
    public float roofRectWidth;
    public float roofRectHeight;
    public float DashingSoulCircleRadius;

    public Vector3 RightWallOffset;
    public float RightWallRectWidth;
    public float RightWallRectHeight;

    public Vector3 LeftWallOffset;
    public float LeftWallRectWidth;
    public float LeftWallRectHeight;

    public Vector3 DownRightWallOffset;
    public float DownRightWallRectWidth;
    public float DownRightWallRectHeight;

    public Vector3 DownLeftWallOffset;
    public float DownLeftWallRectWidth;
    public float DownLeftWallRectHeight;

    public Vector3 UpRightWallOffset;
    public float UpRightWallRectWidth;
    public float UpRightWallRectHeight;

    public Vector3 UpLeftWallOffset;
    public float UpLeftWallRectWidth;
    public float UpLeftWallRectHeight;


    public Vector3 RightFootOffset;
    public float RightFootRectWidth;
    public float RightFootRectHeight;

    public Vector3 LeftFootOffset;
    public float LeftFootRectWidth;
    public float LeftFootRectHeight;

    public float attackCircleRadius;

    public GameObject impactLeft;
    public GameObject impactRight;
    public GameObject impactDown;

    public Vector3 dashing_soul_left_checker;
    public Vector3 dashing_soul_right_checker;
    public bool dashingSoulWall; // accessed by DashingSoul.cs

    private float _defaultFootDistance = 0f;
    private float _defaultFootSize = 0.3f;
    private float _defaultFootHeight = 0.3f;
    public bool CoyoteJump = false;
    private bool _waitingForIdleToPlayLandingParticles = false;

    public static GroundController Instance;

    private void Awake(){

        _defaultFootSize = RightFootRectWidth;
        _defaultFootDistance = RightFootOffset.x;
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 

        controller = GetComponent<PlayerController>();
        footsteps = FindObjectOfType<FootstepsSoundsOnAnimator>();
        landing = FindObjectOfType<PlayerLanding>();
    }


    private void Update(){
        if(PlayerState.Instance.DashingSoul){
            onLeftWall = false;
            onRightWall = false;
        } else if(PlayerState.Instance.DashingLight){
            onLeftWall = false;
            onRightWall = false;
            PlayerState.Instance.Grounded = false;
            LeftFootGrounded = false;
            RightFootGrounded = false;
        } else {
            GroundAndPlatformController();
        }
        AdjustGroundCheckerOffset();
        WallCheckForDashingSoul();
    }

    void AdjustGroundCheckerOffset(){
         if(PlayerController.Instance.AnimatorIsPlaying("jump")
        || PlayerController.Instance.AnimatorIsPlaying("jump_noParticles")){
            SetFeetToMinus5();
        } else if(PlayerController.Instance.AnimatorIsPlaying("fall")){
            if(PlayerController.Instance.Animator.GetBool("hardFall")){
                // print("Hard Fall!");
                SetFeetToHardFall();
            } else {
                SetFeetToFall();
            }
        } else if(PlayerController.Instance.AnimatorIsPlaying("dash")){
            SetFeetToDash();
        } else {
            if(CoyoteJump == false){
                SetFeetToDefault();
            } else {
                SetFeetToCoyote();
            }
        }
    }

    void WallCheckForDashingSoul(){
        RightFootGrounded = Physics2D.OverlapBox(transform.position + RightFootOffset, new Vector2 (RightFootRectWidth, RightFootRectHeight), 0f, groundLayer);
        LeftFootGrounded = Physics2D.OverlapBox(transform.position + LeftFootOffset, new Vector2 (LeftFootRectWidth, LeftFootRectHeight), 0f, groundLayer);

        if(LeftFootGrounded || RightFootGrounded){
            PlayerState.Instance.Grounded = true;
        } else {
            PlayerState.Instance.Grounded = false;
        }
        if(controller.State.FacingRight){
            dashingSoulWall = Physics2D.OverlapCircle(transform.position + dashing_soul_right_checker, DashingSoulCircleRadius, attackLayer);
            if(controller.State.DashingSoul){
                if(PlayerState.Instance.Grounded && controller.AnimatorIsPlaying("dashingsoul_dashing")){
                    smokesDashingSoul.Play();
                    smokesDashingSoul2.Play();
                    smokesDashingSoul3.Play();
                } else{
                    smokesDashingSoul.Stop();
                    smokesDashingSoul2.Stop();
                    smokesDashingSoul3.Stop();
                }
            } else{
                smokesDashingSoul.Stop();
                smokesDashingSoul2.Stop();
                smokesDashingSoul3.Stop();
            }
        } else {
            dashingSoulWall = Physics2D.OverlapCircle(transform.position + dashing_soul_left_checker, DashingSoulCircleRadius, attackLayer);
            if(controller.State.DashingSoul){
                if(PlayerState.Instance.Grounded && controller.AnimatorIsPlaying("dashingsoul_dashing")){
                    smokesDashingSoul.Play();
                    smokesDashingSoul2.Play();
                    smokesDashingSoul3.Play();
                } else{
                    smokesDashingSoul.Stop();
                    smokesDashingSoul2.Stop();
                    smokesDashingSoul3.Stop();
                }
            } else{
                    smokesDashingSoul.Stop();
                    smokesDashingSoul2.Stop();
                    smokesDashingSoul3.Stop();
            }
        }
    }

    // If not dashing soul or dashing light - loop.
    public void GroundAndPlatformController(){

        bool wasGrounded = (RightFootGrounded || LeftFootGrounded);

        onRoof = Physics2D.OverlapBox(transform.position + roofOffset, new Vector2(roofRectWidth, roofRectHeight), 0f, groundLayer);
        onRightWall = Physics2D.OverlapBox(transform.position + RightWallOffset, new Vector2 (RightWallRectWidth, RightWallRectHeight), 0f, groundLayer);
        downRightWall = Physics2D.OverlapBox(transform.position + DownRightWallOffset, new Vector2 (DownRightWallRectWidth, DownRightWallRectHeight), 0f, anyLayer);
        downLeftWall = Physics2D.OverlapBox(transform.position + DownLeftWallOffset, new Vector2 (DownLeftWallRectWidth, DownLeftWallRectHeight), 0f, anyLayer);
        upRightWall = Physics2D.OverlapBox(transform.position + UpRightWallOffset, new Vector2 (UpRightWallRectWidth, UpRightWallRectHeight), 0f, groundLayer);
        upLeftWall = Physics2D.OverlapBox(transform.position + UpLeftWallOffset, new Vector2 (UpLeftWallRectWidth, UpLeftWallRectHeight), 0f, groundLayer);
        RollRight = Physics2D.OverlapBox(transform.position + RightRollOffset, new Vector2 (RightRollRectWidth, RightRollRectHeight), 0f, groundLayer);
        RollLeft = Physics2D.OverlapBox(transform.position + LeftRollOffset, new Vector2 (LeftRollRectWidth, LeftRollRectHeight), 0f, groundLayer);
        onLeftWall = Physics2D.OverlapBox(transform.position + LeftWallOffset, new Vector2 (LeftWallRectWidth, LeftWallRectHeight), 0f, groundLayer);

        if(PlayerController.Instance.GetVerticalVelocity() > 0.1f)
        {
            // print("WIZ is moving UP!");
            _WizIsMovingUp = true;
            _waitingForIdleToPlayLandingParticles = true;
            RightFootGrounded = false;
            LeftFootGrounded = false;
            PlayerController.Instance.Animator.SetBool("grounded", false);
            return;
        }
        else
        {
            RightFootGrounded = Physics2D.OverlapBox(transform.position + RightFootOffset, new Vector2 (RightFootRectWidth, RightFootRectHeight), 0f, groundLayer);
            LeftFootGrounded = Physics2D.OverlapBox(transform.position + LeftFootOffset, new Vector2 (LeftFootRectWidth, LeftFootRectHeight), 0f, groundLayer);
            if(RightFootGrounded || LeftFootGrounded)
            {
                PlayerController.Instance.Animator.SetBool("grounded", true);
                if(MathF.Abs(PlayerController.Instance.GetHorizontalVelocity()) > 0f)
                {
                    if(_WizIsMovingUp)
                    {
                        _WizIsMovingUp = false;
                        // if(!PlayerState.Instance.Attack)
                        // {
                        //     print("aqui?");
                        //     PlayerController.Instance.Animator.Play("run_landing");
                        // }
                        if(_waitingForIdleToPlayLandingParticles == true){
                            _waitingForIdleToPlayLandingParticles = false;
                            landing.PlayLandingParticlesAndFeedbacks();
                        }
                    }
                }
                else
                {
                    if(_WizIsMovingUp)
                    {
                        _WizIsMovingUp = false;
                        if(!PlayerState.Instance.Attack)
                        {
                            PlayerController.Instance.Animator.Play("idle_landing");
                        }
                        if(_waitingForIdleToPlayLandingParticles == true){
                            _waitingForIdleToPlayLandingParticles = false;
                            landing.PlayLandingParticlesAndFeedbacks();
                        }
                    }
                }
            }
        }

        if(onLeftWall || onRightWall){
            if(!PlayerState.Instance.Grounded){
                controller.State.WalledNotGrounded = true;
            } else{
                controller.State.WalledNotGrounded = false;
            }
        } else{
            controller.State.WalledNotGrounded = false;
        }


        if(PlayerState.Instance.FacingRight 
        && (!RightFootGrounded && LeftFootGrounded) 
        && (PlayerController.Instance.AnimatorIsPlaying("idle"))
        && !PlayerState.Instance.Jump){
            if(!PlayerController.Instance.AnimatorIsPlaying("entering_edge") && !PlayerController.Instance.AnimatorIsPlaying("edge_loop")){
                PlayerController.Instance.Animator.Play("entering_edge");
                PlayerController.Instance.Animator.SetBool("edge", true);
            }
        } else if(PlayerState.Instance.FacingRight == false 
        && (!LeftFootGrounded && RightFootGrounded)
        && (PlayerController.Instance.AnimatorIsPlaying("idle"))
        && !PlayerState.Instance.Jump){
            if(!PlayerController.Instance.AnimatorIsPlaying("entering_edge") && !PlayerController.Instance.AnimatorIsPlaying("edge_loop")){
                PlayerController.Instance.Animator.Play("entering_edge");
                PlayerController.Instance.Animator.SetBool("edge", true);
            }
        }


        PlayerState.Instance.OnRightWall = Physics2D.OverlapBox(transform.position + RightWallOffset, new Vector2 (RightWallRectWidth, RightWallRectHeight), 0f, groundLayer);
        PlayerState.Instance.OnLeftWall = Physics2D.OverlapBox(transform.position + LeftWallOffset, new Vector2 (LeftWallRectWidth, LeftWallRectHeight), 0f, groundLayer);
        PlayerState.Instance.OnRoof = Physics2D.OverlapBox(transform.position + roofOffset, new Vector2(roofRectWidth, roofRectHeight), 0f, groundLayer);

        if(LeftFootGrounded || RightFootGrounded){
            if(controller.AnimatorIsPlaying("wall_to_jump") || controller.AnimatorIsPlaying("jump")){
                if(Jump.Instance.JustJumped)
                {
                    print("JUST JUMPED");
                    PlayerState.Instance.Grounded = false;
                    _waitingForIdleToPlayLandingParticles = true;
                }
                else
                {
                    print("Landing");
                    PlayerState.Instance.Grounded = true;
                    controller.Animator.SetBool("grounded", true);
                    controller.Animator.Play("idle");
                    return;
                }
            } else {
                if(PlayerState.Instance.Grounded == false){
                    if(_waitingForIdleToPlayLandingParticles == true){
                        _waitingForIdleToPlayLandingParticles = false;
                        landing.PlayLandingParticlesAndFeedbacks();
                    }
                } else {
                    if(_waitingForIdleToPlayLandingParticles == true){
                        _waitingForIdleToPlayLandingParticles = false;
                        landing.PlayLandingParticlesAndFeedbacks();
                        // print("PLAYING LANDING PARTICLES AND FEEDBACKS #2");
                    }
                }
                //controller.State.OnWater = false;
                if(PlayerState.Instance.Pogo){
                    PlayerState.Instance.Pogo = false;
                    Inputs.Instance.JumpCanceled();
                }
                PlayerState.Instance.Grounded = true;
                PlayerState.Instance.LeftLightAndDidntLand = false;
                PlayerState.Instance.HoldingWallJump = false;
                controller.Animator.SetBool("onWater", false);
                controller.Animator.SetBool("grounded", true);
            }

        } else {
            //controller.State.OnWater = false;
            PlayerState.Instance.Grounded = false;
            _waitingForIdleToPlayLandingParticles = true;
            controller.Animator.SetBool("grounded", false);
            controller.Animator.SetBool("onWater", false);
        }

    }


    private void OnTriggerEnter2D(Collider2D collider){
        if(controller.State.OnElevator == false){
            if(collider.CompareTag("Wood")){
                SetAllGroundsToFalse();
                controller.State.OnWood = true;
                SetParticlesToPlay();
                landing.LandingEvent = landing.Wood;
                footsteps.footstepToPlay = footsteps.Wood;
            } else if(collider.CompareTag("Grass")){
                SetAllGroundsToFalse();
                controller.State.OnGrass = true;
                SetParticlesToPlay();
                landing.LandingEvent = landing.Dirt;
                footsteps.footstepToPlay = footsteps.Dirt;
            } else if(collider.CompareTag("Stone")){
                SetAllGroundsToFalse();
                landing.LandingEvent = landing.Stone;
                SetParticlesToPlay();
                footsteps.footstepToPlay = footsteps.Stone;     
            }else if(collider.CompareTag("RegularGround") || collider.CompareTag("Ground")){
                SetAllGroundsToFalse();
                landing.LandingEvent = landing.Dirt;
                SetParticlesToPlay();
                footsteps.footstepToPlay = footsteps.Dirt;     
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collider){
        if(controller.State.OnElevator == false){
            if(collider.CompareTag("Wood")){
                SetAllGroundsToFalse();
                controller.State.OnWood = true;
                SetParticlesToPlay();
                landing.LandingEvent = landing.Wood;
                footsteps.footstepToPlay = footsteps.Wood;
            } else if(collider.CompareTag("Grass")){
                SetAllGroundsToFalse();
                controller.State.OnGrass = true;
                SetParticlesToPlay();
                landing.LandingEvent = landing.Dirt;
                footsteps.footstepToPlay = footsteps.Dirt;
            } else if(collider.CompareTag("Dirt")){
                SetAllGroundsToFalse();
                landing.LandingEvent = landing.Dirt;
                SetParticlesToPlay();
                footsteps.footstepToPlay = footsteps.Dirt;     
            } else if(collider.CompareTag("Stone")){
                SetAllGroundsToFalse();
                landing.LandingEvent = landing.Stone;
                SetParticlesToPlay();
                footsteps.footstepToPlay = footsteps.Stone;     
            }else if(collider.CompareTag("RegularGround")){
                SetAllGroundsToFalse();
                landing.LandingEvent = landing.Dirt;
                SetParticlesToPlay();
                footsteps.footstepToPlay = footsteps.Dirt;     
            }
        }
    }


    private void SetAllGroundsToFalse(){
        controller.State.OnGrass = false;
        controller.State.OnWood = false;
    }

    private void SetParticlesToPlay(){
        if(controller.State.OnGrass){
            landing.SetParticles("grass");
        } else if(PlayerState.Instance.OnWood){
            landing.SetParticles("wood");
        } else if(PlayerState.Instance.OnStone){
            landing.SetParticles("stone");
        } else if(PlayerState.Instance.OnDirt){
            landing.SetParticles("dirt");
        } else {
            landing.SetParticles("regular");
        }
    }

    public void EnableFeetIn02(){
        Invoke("SetFeetToDefault", 0.2f);
    }

    public void DisableFeet(){
        RightFootOffset.x = 0f;
        LeftFootOffset.x = 0f;
        LeftFootRectWidth = 0f;
        RightFootRectHeight = 0f;
        LeftFootRectHeight = 0f;
        RightFootRectHeight = 0f;
    }

    public void SetFeetToZero(){
        RightFootOffset.x= 0f;
        LeftFootOffset.x = 0f;
        LeftFootRectWidth = _defaultFootSize;
        RightFootRectWidth = _defaultFootSize;
        LeftFootRectHeight = _defaultFootHeight;
        RightFootRectHeight = _defaultFootHeight;
    }

    public void SetFeetToDash(){
        RightFootOffset.x = 0.1f;
        LeftFootOffset.x = -0.1f;
        LeftFootRectWidth = 0.1f;
        RightFootRectHeight = 0.1f;
        LeftFootRectHeight = 0.1f;
        RightFootRectHeight = 0.1f;
    }

    public void SetFeetToHardFall(){
        LeftFootRectHeight = 0.5f;
        RightFootRectHeight = 0.5f;
    }

    public void SetFeetToMinus5(){
        RightFootOffset.x= 0.15f;
        LeftFootOffset.x = -0.15f;
        LeftFootRectWidth = _defaultFootSize;
        RightFootRectWidth = _defaultFootSize;
        LeftFootRectHeight = _defaultFootHeight;
        RightFootRectHeight = _defaultFootHeight;
    }

    public void SetFeetToFall(){
        LeftFootRectWidth = _defaultFootSize;
        RightFootRectWidth = _defaultFootSize;
        LeftFootRectHeight = 0.1f;
        RightFootRectHeight = 0.1f;
    }

    public void SetFeetToDefault(){
        RightFootOffset.x = _defaultFootDistance;
        LeftFootOffset.x = -_defaultFootDistance;
        LeftFootRectWidth = _defaultFootSize;
        RightFootRectWidth = _defaultFootSize;
        LeftFootRectHeight = _defaultFootHeight;
        RightFootRectHeight = _defaultFootHeight;
    }

    public void SetFeetToCoyote(){
        if(PlayerState.Instance.FacingRight){
            LeftFootOffset.x = -0.7f;
            LeftFootRectWidth = 1f;
            RightFootOffset.x = _defaultFootDistance;
            RightFootRectWidth = _defaultFootSize;
            LeftFootRectHeight = _defaultFootHeight;
            RightFootRectHeight = _defaultFootHeight;
        } else {
            RightFootOffset.x = 0.7f;
            RightFootRectWidth = 1f;
            LeftFootOffset.x = _defaultFootDistance;
            LeftFootRectHeight = _defaultFootSize;
            LeftFootRectHeight = _defaultFootHeight;
            RightFootRectHeight = _defaultFootHeight;
        }
    }


    private void OnDrawGizmos() {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + roofOffset, new Vector3(roofRectWidth, roofRectHeight, 0));
    
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position + RightFootOffset, new Vector3(RightFootRectWidth, RightFootRectHeight, 0));
        Gizmos.DrawWireCube(transform.position + LeftFootOffset, new Vector3(LeftFootRectWidth, LeftFootRectHeight, 0));
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position + RightWallOffset, new Vector3(RightWallRectWidth, RightWallRectHeight, 0));
        Gizmos.DrawWireCube(transform.position + LeftWallOffset, new Vector3(LeftWallRectWidth, LeftWallRectHeight, 0));
        Gizmos.DrawWireCube(transform.position + DownRightWallOffset, new Vector3(DownRightWallRectWidth, DownRightWallRectHeight, 0));
        Gizmos.DrawWireCube(transform.position + DownLeftWallOffset, new Vector3(DownLeftWallRectWidth, DownLeftWallRectHeight, 0));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + UpRightWallOffset, new Vector3(UpRightWallRectWidth, UpRightWallRectHeight, 0));
        Gizmos.DrawWireCube(transform.position + UpLeftWallOffset, new Vector3(UpLeftWallRectWidth, UpLeftWallRectHeight, 0));


        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + RightRollOffset, new Vector3 (RightRollRectWidth, RightRollRectHeight, 0f));
        Gizmos.DrawWireCube(transform.position + LeftRollOffset, new Vector3 (LeftRollRectWidth, LeftRollRectHeight, 0f));

        // Gizmos.color = Color.blue;
        // Gizmos.DrawWireSphere(transform.position + dashing_soul_left_checker, DashingSoulCircleRadius);
        // Gizmos.DrawWireSphere(transform.position + dashing_soul_right_checker, DashingSoulCircleRadius);
    }


}