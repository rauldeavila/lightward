using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerState : MonoBehaviour {

    public static PlayerState Instance;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
        Respawning = false;
        Interacting = false;
        Grounded = true;
    }

    [Title("Controls")]
    public bool Grounded;
    public bool OnPlatform;
    public bool FacingRight;
    public bool LeftLightAndDidntLand;

    [Title("Basic")]
    [GUIColor(0.988f, 0.6f, 0.439f)]
    public bool Pogo;
    [GUIColor(0.988f, 0.6f, 0.439f)]
    public bool Run;
    [GUIColor(0.988f, 0.6f, 0.439f)]
    public bool Jump;
    [GUIColor(0.988f, 0.6f, 0.439f)]
    public bool WallJump;
    [GUIColor(0.988f, 0.6f, 0.439f)]
    public bool Attack;
    [GUIColor(0.988f, 0.6f, 0.439f)]
    public bool Dash;
    [GUIColor(0.988f, 0.6f, 0.439f)]
    public bool Dodge;
    [GUIColor(0.988f, 0.6f, 0.439f)]
    public bool JustDodged;
    [GUIColor(0.988f, 0.6f, 0.439f)]
    public bool LookingUp;
    [GUIColor(0.988f, 0.6f, 0.439f)]
    public bool LookingDown;
    [GUIColor(0.988f, 0.6f, 0.439f)]
    public bool Invulnerable;
    [GUIColor(0.988f, 0.6f, 0.439f)]
    public bool Roll;

    [Title("Disable Movement")]
    [GUIColor(1f, 0.3f, 0.3f)]
    public bool Dead;
    [GUIColor(1f, 0.3f, 0.3f)]
    public bool Interacting;
    [GUIColor(1f, 0.3f, 0.3f)]
    public bool Hit;
    [GUIColor(1f, 0.3f, 0.3f)]
    public bool Respawning;
    [GUIColor(1f, 0.3f, 0.3f)]
    public bool Sit;
    [GUIColor(1f, 0.3f, 0.3f)]
    public bool BeingKnockedBack; 
  
    [Title("Spells")]
    [GUIColor(0.2f, 1f, 0.5f)]
    public bool DashingLight;
    [GUIColor(0.2f, 1f, 0.5f)]
    public bool InsideLight;
    [GUIColor(0.2f, 1f, 0.5f)]
    public Vector3 TargetLightPosition;
    [GUIColor(0.2f, 1f, 0.5f)]
    public bool JustCastedFireball;
    [GUIColor(0.2f, 1f, 0.5f)]
    public bool DashingSoul;
    [GUIColor(0.2f, 1f, 0.5f)]
    public bool RecoveringHealth;

    [Title("Wall")]
    [GUIColor(0.2f, 1f, 0.8f)]
    public bool HoldingWallJump;
    [GUIColor(0.2f, 1f, 0.8f)]
    public bool WalledNotGrounded;
    [GUIColor(0.2f, 1f, 0.8f)]
    public bool OnLeftWall;
    [GUIColor(0.2f, 1f, 0.8f)]
    public bool OnRightWall;
    [GUIColor(0.2f, 1f, 0.8f)]
    public bool OnRoof;

    [Title("Area")]
    [GUIColor(0.4f, 0.4f, 0.8f)]
    public bool OnGrass; 
    [GUIColor(0.4f, 0.4f, 0.8f)]
    public bool OnDirt;
    [GUIColor(0.4f, 0.4f, 0.8f)]
    public bool OnStone;
    [GUIColor(0.4f, 0.4f, 0.8f)]
    public bool OnWood;
    [GUIColor(0.4f, 0.4f, 0.8f)]
    public bool OnWater;
    [GUIColor(0.4f, 0.4f, 0.8f)]
    public bool OnElevator;

    public bool UnaffectedByWind = false;


    [SerializeField] private bool  _onSaveTrigger;
    public bool OnSaveTrigger {get => _onSaveTrigger; set => _onSaveTrigger = value; }

    [SerializeField] private bool  _freezing;
    public bool Freezing {get => _freezing; set => _freezing = value; }

    [SerializeField] private bool  _melting;
    public bool Melting {get => _melting; set => _melting = value; }



    [SerializeField] private bool  _defaultCloak;
    public bool DefaultCloak {get => _defaultCloak; set => _defaultCloak = value; }

    [SerializeField] private bool  _blueCloak;
    public bool BlueCloak {get => _blueCloak; set => _blueCloak = value; }

    [SerializeField] private bool  _redCloak;
    public bool RedCloak {get => _redCloak; set => _redCloak = value; }

    public void SetAllStatesToFalse(){
        Run = false;
        Jump = false;
        Attack = false;
        Dash = false;
        DashingLight = false;
        DashingSoul = false;
        RecoveringHealth = false;
        Grounded = false;
        WallJump = false;
        Sit = false;
        Dodge = false;
        JustDodged = false;
        BeingKnockedBack = false;
        LookingUp = false;
        LookingDown = false;
        Roll = false;
        Respawning = false;
        Interacting = false;
        Hit = false;
        Dead = false;
        Pogo = false;
        UnaffectedByWind = false;
    }



    public bool CanUseSpell(float cost){
        if(!Interacting && !Sit && !DashingLight && !RecoveringHealth){
            if(PlayerStats.Instance.wiz_magic.runTimeValue >= cost){
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }


    public void EnterJustCastedFireballForSeconds(float seconds){
        JustCastedFireball = true;
        Invoke("CastedFireballOff", seconds);
    }

    public void CastedFireballOff(){
        JustCastedFireball = false;
    }

}
