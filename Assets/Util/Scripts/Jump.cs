using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;


public class Jump : MonoBehaviour {

    #region declarations
    
    [Header("Basic Settings")]
    
    public bool JustJumped = false;
    public bool Pogoed = false;
    public float PogoMultiplier = 0.8f;
    public float JumpForce = 9f;
    public float MinJumpDuration = 0.15f;
    public float MinWallJumpDuration = 0.5f;
    public float JumpDelay = 0.25f;
    public GameObject JumpParticles;
    public GameObject JumpGrass;
    public GameObject JumpAutumn;

    [Header("Modify Physics")]
    public float fallMultiplier = 8f;
    public float goingUpGravity = 1f;
    public float lowJumpMultiplier = 50f;
    public float apexGravity = 2f;
    public float fallClamp = -15f;

    [Header("Walljump")]
    public float horizontalJumpForce = 4f;
    public float verticalJumpForce = 20f;
    public float WallJumpDuration = 0.25f; // duration with no control
    public bool JustWallJumped = false;

    private float gravity;
    private float _zeroYVelocity = -1.56f; // usado no jumpbehaviour também
    private bool _canJump = true;
    private float _jumpTimer = 0;
    private float _timeAfterJumped = 0f;
    private bool _canModifyPhysics;

    public static Jump Instance;

    #endregion

    #region unity methods
    void Awake() {
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }

    void Start() {
        gravity = GetComponent<Rigidbody2D>().gravityScale;
    }

    void Update() {
        if(!GameState.Instance.Paused){
            JumpTimeSetter();
        }
    }

    void FixedUpdate() {
        CheckIfOnWall(); 
        ModifyPhysics();  
    }
    #endregion

    #region regular jumping

    private void Jumping(){  
        if(StateController.Instance.CanJump){ 
            StateController.Instance.CanJump = false;
            PlayerState.Instance.Jump = true;
            JustJumped = true;
            StartCoroutine(EnableJumpInSeconds());
            PlayJumpSound();
            PlayJumpParticles();
            PlayerController.Instance.Animator.Play("jump");
            GetComponent<Rigidbody2D>().velocity = Vector2.up * JumpForce;
            StartThisTimerWhenWizJumps();
            _jumpTimer = 0;
        } 
    }

    public void Pogo(){ // ----------------------------- pogo pogo pogo pogo ---------------------
        StateController.Instance.CanJump = false;
        // print("Pogo");
        Pogoed = true;
        StateController.Instance.CanDash = true;
        PlayerState.Instance.Jump = true;
        Inputs.Instance.JumpPerformed();
        StartCoroutine(EnableJumpInSeconds());
        // PlayerController.Instance.Animator.Play("jump");
        GetComponent<Rigidbody2D>().velocity = Vector2.up * JumpForce * PogoMultiplier;
        PlayerState.Instance.Pogo = true;
        StartThisTimerWhenWizJumps();
        _jumpTimer = 0;
    }

    IEnumerator EnableJumpInSeconds(){
        yield return new WaitForSeconds(0.15f);
        StateController.Instance.CanJump = true;
        JustJumped = false;
    }

    private void PlatformDownLeap(){
        GetComponent<Rigidbody2D>().velocity = Vector2.up * 1.5f;
        PlayerController.Instance.Animator.Play("fall");
    }



    private void ModifyPhysics() {
        if(StateController.Instance.CanModifyPhysics){
            if(PlayerState.Instance.Grounded){
                if(DebugY.Instance != null){
                    DebugY.Instance.ActivateEven();
                }
                PlayerState.Instance.UnaffectedByWind = false;
                GetComponent<Rigidbody2D>().gravityScale = gravity;
            } else{
                if(GetComponent<Rigidbody2D>().velocity.y < _zeroYVelocity){ // CAINDO
                    if(DebugY.Instance != null){
                        DebugY.Instance.ActivateDown();
                    }
                    GetComponent<Rigidbody2D>().gravityScale = fallMultiplier;
                        if(GetComponent<Rigidbody2D>().velocity.y < fallClamp){ // FALL CLAMP
                            GetComponent<Rigidbody2D>().gravityScale = gravity;
                            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, fallClamp);
                            return;
                        }
                    return;
                } else if(PlayerController.Instance.AnimatorIsPlaying("jump") || PlayerState.Instance.Pogo){  // PULANDO
                    if(DebugY.Instance != null){
                        DebugY.Instance.ActivateUp();
                    }
                    if(_timeAfterJumped >= 0.35f){
                        GetComponent<Rigidbody2D>().gravityScale = apexGravity;
                    } else {
                        if(Inputs.Instance.HoldingJump || DashingLight.Instance.FakeHoldingJump){
                        // GetComponent<Rigidbody2D>().gravityScale = gravity;
                            GetComponent<Rigidbody2D>().gravityScale = goingUpGravity;
                        } else {
                            GetComponent<Rigidbody2D>().gravityScale = lowJumpMultiplier;
                        }
                    }
                }
            }
        } else {
            if(PlayerState.Instance.OnElevator){
                GetComponent<Rigidbody2D>().gravityScale = 40f;
            }
        }
    }












    private void StartThisTimerWhenWizJumps(){
        _timeAfterJumped = 0f;
        StartCoroutine(JumpTimer());
    }

    IEnumerator JumpTimer(){
        while(_timeAfterJumped < 10f){
            yield return new WaitForSeconds(0.05f);
            _timeAfterJumped += 0.05f;
            if(PlayerState.Instance.Grounded || PlayerState.Instance.OnWater){
                _timeAfterJumped = 0f;
                yield break;
            }
        }
        _timeAfterJumped = 0f;
    }

    #endregion

    #region wall jump

    private void CheckIfOnWall(){    
        if(PlayerState.Instance.WalledNotGrounded && !PlayerState.Instance.OnElevator && ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("wiz_wall_jump").runTimeValue == true){
            // only wall slide if pressing right or left (wall side)
            if(GetComponent<Rigidbody2D>().velocity.y < 0f){
                if((!PlayerController.Instance.AnimatorIsPlaying("wall_sliding") && (PlayerState.Instance.FacingRight && Move.Instance.GetDirectionXValue() > 0 && GroundController.Instance.onRightWall)) 
                || ((PlayerState.Instance.FacingRight == false && Move.Instance.GetDirectionXValue() < 0 && GroundController.Instance.onLeftWall))){
                    PlayerState.Instance.WallJump = false;
                    PlayerState.Instance.Jump = false;
                    if(GetComponent<Rigidbody2D>().velocity.y < -1f){
                        if(!PlayerController.Instance.AnimatorIsPlaying("wall_sliding")){
                            PlayerController.Instance.Animator.SetBool("onWall", true);
                            PlayerController.Instance.Animator.Play("wall_sliding");
                            PlayerState.Instance.UnaffectedByWind = true;
                        }
                    }
                    if(GetComponent<Rigidbody2D>().velocity.y < -4f){
                        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -4f);
                    }
                } else if(PlayerController.Instance.AnimatorIsPlaying("wall_sliding")){
                    if(GetComponent<Rigidbody2D>().velocity.y < -4f){
                        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -4f);
                    }

                    if((PlayerState.Instance.FacingRight && Inputs.Instance.HoldingLeftArrow) || (!PlayerState.Instance.FacingRight && Inputs.Instance.HoldingRightArrow) ){
                        DropFromWall();
                    } 
                }

            }
        
        } else {
            PlayerController.Instance.Animator.SetBool("onWall", false);
        }
    }

    private void DropFromWall(){
            PlayerState.Instance.WallJump = true;
            PlayerState.Instance.WalledNotGrounded = false;
            PlayerState.Instance.WallJump = false;
            PlayerController.Instance.Animator.SetBool("wallDropping", true);
            StartCoroutine(ResetWallDroppingBool());
            PlayerController.Instance.Animator.Play("wall_drop");
            PlayerController.Instance.Move.Flip();
            PlayerState.Instance.UnaffectedByWind = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
            if(PlayerState.Instance.FacingRight){
                GetComponent<Rigidbody2D>().velocity = new Vector2( 9 , 0);
            } else {
                GetComponent<Rigidbody2D>().velocity = new Vector2( -9, 0);
            }
    }

    IEnumerator ResetWallDroppingBool(){
        yield return new WaitForSeconds(0.1f);
        PlayerController.Instance.Animator.SetBool("wallDropping", false);
    }

    #endregion

    #region aux methods
    
    public void HandleJumpInput(){

        if(PlayerState.Instance.Interacting && InteractionsManager.Instance.Dialogue){
            DialogueManager.Instance.SkipTypeOrAdvance();
            return;
        }

        if(PlayerState.Instance.InsideLight){
            PlayerState.Instance.DashingLight = false;
            // DashingLight.Instance.DropFromLight();
            return;
        }
        AttemptToJump();
    }

    void JumpTimeSetter(){
        if(Inputs.Instance.HoldingJump && PlayerState.Instance.Jump){
            _jumpTimer = Time.time + JumpDelay;
        }
    }

    void AttemptToJump(){ // called on FixedUpdate

        if(StateController.Instance.CanJump){
            if(PlayerState.Instance.OnPlatform && Inputs.Instance.HoldingDownArrow){
                PlatformDownLeap();
            } else {
                Jumping();
            }
        } else if(StateController.Instance.CanWallJump){
            PlayerController.Instance.SetGravityToZero();
            WallJumping();
        }
    }


    private void WallJumping(){
        if(StateController.Instance.CanWallJump){
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StateController.Instance.CanWallJump = false;
            PlayerState.Instance.WallJump = true;
            JustWallJumped = true;
            Debug.ClearDeveloperConsole();
            Invoke("ResetJustWallJumpedVariable", WallJumpDuration);
            PlayJumpSound();
            Move.Instance.Flip();
            PlayerState.Instance.WalledNotGrounded = false;
            
            PlayerController.Instance.Animator.Play("wall_to_jump");
            
            if(PlayerState.Instance.FacingRight){
                GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalJumpForce, verticalJumpForce);
            } else {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-horizontalJumpForce, verticalJumpForce);
            }
            StartThisTimerWhenWizJumps();
            _jumpTimer = 0;
        }
    }

    private void ResetJustWallJumpedVariable(){ // this variable enables movement again
        PlayerState.Instance.UnaffectedByWind = false;
        JustWallJumped = false;
        StateController.Instance.CanModifyPhysics = true;
    }


    #endregion

    #region external scripts uses them


    public bool IsHoldingJump(){
        return Inputs.Instance.HoldingJump;
    }

    public void SetGravityAsZero(){
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public void SetGravityAsDefault(){
        GetComponent<Rigidbody2D>().gravityScale = gravity;
    }

    public void PlayJumpSound(){
        SFXController.Instance.Play("event:/char/wiz/jump");
    }

    public void PlayJumpParticles(){
        // prevent from duplicating
        Instantiate(JumpParticles, this.transform.position, Quaternion.identity);
        if(PlayerState.Instance.OnGrass)
        {
            if(RoomConfigurations.Instance.GetProfileName() == "Autumn")
            {
                Instantiate(JumpAutumn, this.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(JumpGrass, this.transform.position, Quaternion.identity);
            }
        }
    }

    public float GetTimeAfterJump(){
        return _timeAfterJumped;
    }

    #endregion

}
