using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour 
{
    private PlayerController controller;
    public Vector2 direction;
    public float maxSpeed = 7f;
    private Vector3 zeroedVelocity;

    private bool _finishedInitialMovement = false;

    private bool _movingThroughDodge = false;

    public bool IsNoClipActive = false;
    public Collider2D noClipCol1;
    public Collider2D noClipCol2;

    public static Move Instance;

    private void Awake() {

        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 

        controller = GetComponent<PlayerController>();
        controller.State.FacingRight = true;
        zeroedVelocity = Vector3.zero;

    }

    void Start(){
        if(controller.State.FacingRight == false){
            controller.State.FacingRight = true;
            Flip();
        }
        controller.Controls.Game.MoveRight.performed += ctx => SetDirection(ctx, 1);
        controller.Controls.Game.MoveRight.canceled += ctx => ResetDirection(ctx, 1);
        controller.Controls.Game.MoveLeft.performed += ctx => SetDirection(ctx, -1);
        controller.Controls.Game.MoveLeft.canceled += ctx => ResetDirection(ctx, -1);
    }

    private void Update(){
        if(controller.State.Dead){
            StopPlayer();
        }

        if (Keyboard.current.f5Key.wasPressedThisFrame) 
        {
            NoClip();
        }
    }

    private void FixedUpdate(){
        if (IsNoClipActive) {
            HandleNoClipMovement();
            return;
        }
        if(GameState.Instance.Overworld == false)
        {
            return;
        }

        if(GameState.Instance.Paused ){
            StopPlayer();
        }    
        controller.Animator.SetFloat("vertical",GetComponent<Rigidbody2D>().velocity.y);
        if(_movingThroughDodge){
            HandleDodge();
            return;
        }
        
        if(StateController.Instance.CanMove && ScenesManager.Instance.FinishedLoading){
            if(!_finishedInitialMovement && ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("game_changing_scene").runTimeValue){
                if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("wiz_facing_right").runTimeValue == false){
                    FakeMovingLeft();
                } else {
                    FakeMovingRight();
                }
                PlayerController.Instance.Animator.SetBool("holdingLeftOrRight", true);
                Invoke("StopMoving", 0.3f); // wiz anda sozinho enquanto o game_changing_scene for true.
            }

            // STOP WIZ WHEN FACING WALLS
            if(PlayerState.Instance.Grounded && !PlayerController.Instance.AnimatorIsPlaying("jump") &&  !PlayerController.Instance.AnimatorIsPlaying("fall") &&  !PlayerController.Instance.AnimatorIsPlaying("roll")){
                if(GroundController.Instance.downLeftWall && PlayerState.Instance.FacingRight == false && direction.x < 0f){
                    PlayerController.Instance.Animator.SetBool("facingWall", true);
                    StopPlayer();
                    return;
                }

                if(GroundController.Instance.downRightWall && PlayerState.Instance.FacingRight == true && direction.x > 0f){
                    PlayerController.Instance.Animator.SetBool("facingWall", true);
                    StopPlayer();
                    return;
                }
            }

            PlayerController.Instance.Animator.SetBool("facingWall", false);
            controller.Animator.SetFloat("horizontal", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
            MoveCharacter(direction.x);
            if(direction.x != 0)
            {
                PlayerController.Instance.Animator.SetBool("holdingLeftOrRight", true);
            }
            else
            {
                PlayerController.Instance.Animator.SetBool("holdingLeftOrRight", false);
            }
        } else {
            PlayerState.Instance.Run = false;
        }
        float pixelsPerUnit = 16; // Assuming 16 pixels per unit
        Vector3 position = transform.position;
        position.x = Mathf.Round(position.x * pixelsPerUnit) / pixelsPerUnit;
        position.y = Mathf.Round(position.y * pixelsPerUnit) / pixelsPerUnit;
        transform.position = position;
        
    }

    void StopMoving(){
        _finishedInitialMovement = true;
        direction = new Vector2(0f, 0f);
        PlayerController.Instance.Animator.SetBool("holdingLeftOrRight", false);
    }

    void HandleDodge(){
        if(PlayerState.Instance.FacingRight){
            Vector2 targetVelocity = new Vector2(1 * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
            GetComponent<Rigidbody2D>().velocity = Vector3.SmoothDamp(GetComponent<Rigidbody2D>().velocity, targetVelocity, ref zeroedVelocity, 0.05f);
        } else {
            Vector2 targetVelocity = new Vector2(-1 * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
            GetComponent<Rigidbody2D>().velocity = Vector3.SmoothDamp(GetComponent<Rigidbody2D>().velocity, targetVelocity, ref zeroedVelocity, 0.05f);
        }
    }

    void MoveCharacter(float horizontal){
        if(PlayerState.Instance.Dash){
            return;
        }

        if(PlayerState.Instance.Grounded && ((GroundController.Instance.downLeftWall && !controller.State.FacingRight) || (GroundController.Instance.downRightWall && controller.State.FacingRight))){
            if ((horizontal > 0 && !controller.State.FacingRight) || (horizontal < 0 && controller.State.FacingRight)) {
                Flip();
            } else{
                return;
            }
        }

        if ((horizontal > 0 && !controller.State.FacingRight) || (horizontal < 0 && controller.State.FacingRight)) {
            Flip();
        }

        if(horizontal > 0){
            Vector2 targetVelocity = new Vector2(1 * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
            GetComponent<Rigidbody2D>().velocity = Vector3.SmoothDamp(GetComponent<Rigidbody2D>().velocity, targetVelocity, ref zeroedVelocity, 0.05f);
        } else if(horizontal < 0){
            Vector2 targetVelocity = new Vector2(-1 * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
            GetComponent<Rigidbody2D>().velocity = Vector3.SmoothDamp(GetComponent<Rigidbody2D>().velocity, targetVelocity, ref zeroedVelocity, 0.05f);
        } else{
            Vector2 targetVelocity = new Vector2(0 * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
            GetComponent<Rigidbody2D>().velocity = Vector3.SmoothDamp(GetComponent<Rigidbody2D>().velocity, targetVelocity, ref zeroedVelocity, 0.05f);
        }

        if (StateController.Instance.CanMove && (GetComponent<Rigidbody2D>().velocity.x > 0.1 || GetComponent<Rigidbody2D>().velocity.x < -0.1) && PlayerState.Instance.Grounded){
            controller.State.Run = true;
        } else {
            controller.State.Run = false;
        }
    }

    public void Flip(){
        if(controller.AnimatorIsPlaying("attack") || controller.AnimatorIsPlaying("attack1-ending") || controller.AnimatorIsPlaying("attack2") || controller.AnimatorIsPlaying("attack2-ending") || controller.AnimatorIsPlaying("attack3")){
            if(controller.State.Grounded){
                controller.Animator.Play("idle");
            } else{
                controller.Animator.Play("fall");
            }
        }

        controller.State.FacingRight = !controller.State.FacingRight;
        FlipSprite();

        if(controller.State.FacingRight){
            controller.GroundController.groundCheckAdjustmentForNewSprites = new Vector3(0f, 0f, 0f);
        } else {
            controller.GroundController.groundCheckAdjustmentForNewSprites = new Vector3(-0.27f, 0f, 0f);
        }
    }

    public void FlipSprite(){  
        transform.rotation = Quaternion.Euler(0, controller.State.FacingRight ? 0 : 180, 0);
    }

    public void FlipToRight(){
        if(!controller.State.FacingRight){
            Flip();
        }
    }

    public void FlipToLeft(){
        if(controller.State.FacingRight){
            Flip();
        }
    }

    public float GetDirectionXValue(){
        return direction.x;
    }

    public float GetDirectionYValue(){
        return direction.y;
    }

    public int GetVerticalStatus(){
        if(Inputs.Instance.HoldingDownArrow){
            return -1;
        } else if(Inputs.Instance.HoldingUpArrow){
            return 1;
        } else {
            return 0;
        }
    }

    public int GetHorizontalStatus(){
        if(direction.x > 0){
            return 1;
        } else if(direction.x < 0){
            return -1;
        } else{
            return 0;
        }
    }

    public void StopPlayer(){
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        controller.Animator.SetFloat("horizontal", 0);
        controller.Animator.SetFloat("vertical", 0);
        controller.Animator.SetTrigger("idle");
    }

    public void StopPlayerWithoutChangingAnimation(){
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        controller.Animator.SetFloat("horizontal", 0);
        controller.Animator.SetFloat("vertical", 0);
    }

    public void MoveThroughDodge(){
        _movingThroughDodge = true;
    }

    public void StopMoveThroughDodge(){
        _movingThroughDodge = false;
    }

    public void FakeMovingRight(){
        direction = new Vector2(1f, 0f);
    }
    public void FakeMovingLeft(){
        direction = new Vector2(-1f, 0f);
    }

    private void SetDirection(InputAction.CallbackContext ctx, int directionValue){
        direction.x = directionValue;
    }

    private void ResetDirection(InputAction.CallbackContext ctx, int directionValue){
        if (direction.x == directionValue){
            direction.x = 0;
        }
    }

    public void NoClip() {
        IsNoClipActive = !IsNoClipActive;
        if (IsNoClipActive) 
        {
            PlayerController.Instance.SetGravityToZero();
            noClipCol1.isTrigger = true;
            noClipCol2.isTrigger = true;
        } else {
            PlayerController.Instance.SetGravityToOne();
            noClipCol1.isTrigger = false;
            noClipCol2.isTrigger = false;
        }
    }

    private void HandleNoClipMovement() {
        PlayerController.Instance.Animator.Play("idle");
        Vector2 input = new Vector2(0, 0);
        if (Inputs.Instance.HoldingUpArrow) {
            if(Inputs.Instance.HoldingDash)
            {
                input.y += 70;
            }
            else
            {
                input.y += 30;
            }
        }
        if (Inputs.Instance.HoldingDownArrow) {
            if(Inputs.Instance.HoldingDash)
            {
                input.y -= 70;
            }
            else
            {
                input.y -= 30;
            }
        }
        if (Inputs.Instance.HoldingRightArrow) {
            if(Inputs.Instance.HoldingDash)
            {
                input.x += 70;
            }
            else
            {
                input.x += 30;
            }
        }
        if (Inputs.Instance.HoldingLeftArrow) {
            if(Inputs.Instance.HoldingDash)
            {
                input.x -= 70;
            }
            else
            {
                input.x -= 30;
            }
        }
        transform.Translate(input * 0.5f * Time.deltaTime);
    }

}
