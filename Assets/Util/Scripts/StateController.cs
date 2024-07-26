using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour {

    public static StateController Instance;

    public bool CanMove;
    public bool CanJump;
    public bool CanWallJump;
    public bool CanDash;
    public bool CanDodge;
    public bool CanOpenSpellbook;
    public bool CanDashToLight;
    public bool CanDashSoul;
    public bool CanModifyPhysics;
    public bool CanCastFireball;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        }
        CanMove = true; // MUDAR ISTO DEPOIS!
    }

    void Update(){
        if(PlayerState.Instance.Dead || PlayerState.Instance.Interacting || GameState.Instance.Paused){
            SetAllToFalse();
            return;
        }
        OverworldOrPausedUpdater();
        MoveUpdater();
        JumpUpdater();
        WallJumpUpdater();
        DashUpdater();
        DodgeUpdater();
        KnockbackUpdater();
        SpellbookUpdater();
        ModifyPhysicsUpdater();
        DashingLightUpdater();
        DashingSoulUpdater();
        InteractionsUpdater();
        FireballUpdater();
    }

    private void OverworldOrPausedUpdater(){
        // if(
        // GameState.Instance.Paused == false &&
        // GameState.Instance.InventoryOpened == false &&
        // GameState.Instance.MapOpened == false
        // ){
        //     GameState.Instance.Overworld = true;
        // } else{
        //     GameState.Instance.Overworld = false;
        // } 
    }

    private void JumpUpdater(){
        if(
        CanMove == true &&
        PlayerState.Instance.Grounded == true &&
        PlayerState.Instance.OnElevator == false
        ){
            CanJump = true; 
        } else { 
            CanJump = false; 
        }
    }

    private void WallJumpUpdater(){
        if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("wiz_wall_jump").runTimeValue == true &&
        CanMove == true &&
        PlayerState.Instance.OnElevator == false &&
        PlayerState.Instance.Grounded == false &&
        ((PlayerState.Instance.FacingRight == true && GroundController.Instance.onRightWall) || 
         (PlayerState.Instance.FacingRight == false && GroundController.Instance.onLeftWall)
        )){
            CanWallJump = true;
        } else {
            CanWallJump = false;
        }
    }

    private void DodgeUpdater(){
        if(PlayerController.Instance.AnimatorIsPlaying("roll")){
            CanDodge = false;
        } else {
            CanDodge = true;
        }
    }

    private void DashUpdater(){ // ajustar pros spells
        if(GameState.Instance.Overworld)
        {
            if(!PlayerController.Instance.AnimatorIsPlaying("sit"))
            {
                if(CanModifyPhysics){
                    if(PlayerState.Instance.Dash == false && (PlayerState.Instance.LeftLightAndDidntLand == true || PlayerState.Instance.Grounded == true || PlayerState.Instance.WalledNotGrounded == true)){
                        CanDash = true;
                    }
                } else {
                    if(!Jump.Instance.Pogoed)
                    {
                        CanDash = false;
                    }
                }
            }
            else
            {
                CanDash = false;
            }
        }
        else
        {
            CanDash = false;
        }
    }

    private void KnockbackUpdater(){
        if(PlayerState.Instance.BeingKnockedBack && PlayerState.Instance.Grounded){
            PlayerState.Instance.BeingKnockedBack = false;
        }
    }

    private void MoveUpdater(){
        if(
        Jump.Instance.JustWallJumped ||
        PlayerState.Instance.Interacting ||
        PlayerState.Instance.Hit ||
        PlayerState.Instance.Sit ||
        PlayerState.Instance.Respawning ||
        PlayerState.Instance.Sit ||
        PlayerState.Instance.DashingLight ||
        PlayerState.Instance.DashingSoul ||
        PlayerState.Instance.Dodge ||
        GameState.Instance.Overworld == false ||
        GameState.Instance.ConsoleOn == true
        ){
            CanMove = false;
        } else {
            CanMove = true;
        }
    }
    private void SpellbookUpdater(){
        if(CanMove && PlayerState.Instance.Grounded){
            CanOpenSpellbook = true;
        } else{
            CanOpenSpellbook = false;
        }
    }

    private void FireballUpdater(){
        if(!PlayerController.Instance.AnimatorIsPlaying("hit")){
            CanCastFireball = true;
        } else{
            CanCastFireball = false;
        }
    }
    
    private void ModifyPhysicsUpdater(){
        if(
        PlayerState.Instance.OnElevator == false &&
        PlayerState.Instance.JustCastedFireball == false && 
        PlayerState.Instance.Dash == false &&
        PlayerState.Instance.Roll == false &&
        PlayerState.Instance.DashingLight == false && 
        ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("game_changing_scene").runTimeValue == false &&
        GameState.Instance.InventoryOpened == false
        // PlayerState.Instance.Pogo == false
        ){
            CanModifyPhysics = true;
        } else {
            CanModifyPhysics = false;
        }
    }

    private void DashingLightUpdater(){
        if(GameState.Instance.Overworld == false){
            CanDashToLight = false;
            return;
        }

        if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("hero_dashing_light").runTimeValue == false){
            CanDashToLight = false;
            return;
        }

        if(PlayerState.Instance.TargetLightPosition == Vector3.zero){
            CanDashToLight = false;
            return;
        }

        if(
        (PlayerState.Instance.DashingLight == false && PlayerState.Instance.InsideLight == false)
        ){
            if(!DashingLight.Instance.KickedOutOfLight)
            {
                DashingLight.Instance.Cooldown = false;
            }
        }

        if(
        PlayerState.Instance.Interacting == true ||
        PlayerState.Instance.Respawning == true
        ){
            PlayerState.Instance.DashingLight = false;
            PlayerState.Instance.InsideLight = false;
            CanDashToLight = false;
            return;
        }

        if(!DashingLight.Instance.KickedOutOfLight)
        {
            CanDashToLight = true;
        }

    }

    private void DashingSoulUpdater(){
        if(
        CanMove &&
        PlayerState.Instance.Grounded
        ){
            CanDashSoul = true;
        } else {
            CanDashSoul = false;
        }
    }

    public void CanMoveToFalse(){
        CanMove = false;
    }

    public void CanMoveToTrue(){
        CanMove = true;
    }

    private void InteractionsUpdater(){
        if(PlayerState.Instance.Interacting == false){
            InteractionsManager.Instance.Dialogue = false;
            InteractionsManager.Instance.Animation = false;
            InteractionsManager.Instance.Cutscene = false;
        }
    }

    private void SetAllToFalse(){
        CanDash = false;
        CanDashSoul = false;
        CanDashToLight = false;
        CanJump = false;
        CanModifyPhysics = false;
        CanMove = false;
        CanOpenSpellbook = false;
        CanWallJump = false;
        CanCastFireball = false;
    }


}
