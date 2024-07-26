using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireballCasting : MonoBehaviour {

    // Using Fake Holding Jump from dashing light
    [SerializeField] private float _timeBtwFireballs = 0.7f;
    private bool _canCastFireballs = true;
    public Transform fireballFirepoint;
    public GameObject fireballRightPrefab;
    public GameObject fireballLeftPrefab;

    public float cost_fireball = 10f;
    public int fireballDamage = 3;

    public Vector2 FireballRightKnockback;
    public Vector2 FireballAirRightKnockback;
    public Vector2 FireballLeftKnockback;
    public Vector2 FireballAirLeftKnockback;

    public float FallingYMultiplier;
    private string FireballCast = "event:/char/wiz/fireball_casting";

    public static FireballCasting Instance;


    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }



    public void HandleFireballButtonPress(){

        TryFireball();
    }

    private void TryFireball(){
        if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("hero_fireball").runTimeValue == true){
            if(PlayerState.Instance.CanUseSpell(cost_fireball) && _canCastFireballs && StateController.Instance.CanCastFireball){
                _canCastFireballs = false;
                PlayerCombat.Instance.EnterAttackCooldownForSeconds(0.5f);
                PlayerState.Instance.EnterJustCastedFireballForSeconds(0.5f);
                if(PlayerState.Instance.FacingRight){
                    CameraSystem.Instance.FireballRight();
                } else{
                    CameraSystem.Instance.FireballLeft();
                }
                Invoke("GenerateFireball", 0.1f);
                Invoke("ReenableFireballs", _timeBtwFireballs);
            }
        }
    }

    private void GenerateFireball(){
        PlayerStats.Instance.DecreaseMagic(cost_fireball);
        FMODUnity.RuntimeManager.PlayOneShot(FireballCast, transform.position);
        if(PlayerState.Instance.FacingRight){
            PlayerController.Instance.SetVelocityToZero();
            if(PlayerState.Instance.Grounded){
                if(PlayerController.Instance.AnimatorIsPlaying("idle")){
                    PlayerController.Instance.SetVelocityToZero();
                    PlayerController.Instance.KnockWizBack(FireballRightKnockback.x, FireballRightKnockback.y, true);
                } else {
                    PlayerController.Instance.SetVelocityToZero();
                    PlayerController.Instance.KnockWizBack(FireballRightKnockback.x * 4, FireballRightKnockback.y, true);
                }
            } else{
                if(PlayerController.Instance.AnimatorIsPlaying("fall")){
                    PlayerController.Instance.SetVelocityToZero();
                    PlayerController.Instance.KnockWizBack(FireballAirRightKnockback.x, FireballAirRightKnockback.y * FallingYMultiplier, true);
                } else{
                    PlayerController.Instance.SetVelocityToZero();
                    PlayerController.Instance.KnockWizBack(FireballAirRightKnockback.x, FireballAirRightKnockback.y, true);
                }
            }
            Instantiate(fireballRightPrefab, fireballFirepoint.position, Quaternion.identity);
        } else{
            PlayerController.Instance.SetVelocityToZero();
            if(PlayerState.Instance.Grounded){
                if(PlayerController.Instance.AnimatorIsPlaying("idle")){
                    PlayerController.Instance.SetVelocityToZero();
                    PlayerController.Instance.KnockWizBack(FireballLeftKnockback.x, FireballLeftKnockback.y, true);
                } else {
                    PlayerController.Instance.SetVelocityToZero();
                    PlayerController.Instance.KnockWizBack(FireballLeftKnockback.x * 4, FireballLeftKnockback.y, true);
                }
            } else{
                if(PlayerController.Instance.AnimatorIsPlaying("fall")){
                    PlayerController.Instance.SetVelocityToZero();
                    PlayerController.Instance.KnockWizBack(FireballAirLeftKnockback.x, FireballAirLeftKnockback.y * FallingYMultiplier, true);
                } else{
                    PlayerController.Instance.SetVelocityToZero();
                    PlayerController.Instance.KnockWizBack(FireballAirLeftKnockback.x, FireballAirLeftKnockback.y, true);
                }
            }
            Instantiate(fireballLeftPrefab, fireballFirepoint.position, Quaternion.identity);
        }
        DisablePlayerControlsForAMoment();
        DashingLight.Instance.FakeHoldingJump = true;
        Invoke("LetJumpGoFromDashingLight", 0.5f);
        PlayerController.Instance.Animator.Play("jump");
    }
    
    void LetJumpGoFromDashingLight(){
        DashingLight.Instance.LetJumpGo();
    }

    private void ReenableFireballs(){
        _canCastFireballs = true;
    }

    private void DisablePlayerControlsForAMoment(){
        PlayerController.Instance.DisablePlayerControls();
        Invoke("EnablePlayerControls", 0.5f);
    }

    private void EnablePlayerControls(){
        PlayerController.Instance.EnablePlayerControls();
    }

}
