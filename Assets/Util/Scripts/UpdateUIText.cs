using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateUIText : MonoBehaviour {

    public bool CanMove;
    public bool CanJump;
    public bool CanWallJump;
    public bool CanDash;
    public bool CanOpenSpellbook;
    public bool CanDashToLight;
    public bool CanDashSoul;
    public bool CanModifyPhysics;

    public bool Gravity;
    public bool HoldingJump;
    public bool JumpTimer;
    public bool YValue;

    public bool RigidbodyVelocity;

    private TextMeshProUGUI _uiText; 
    
    private void Awake() {
        _uiText = GetComponent<TextMeshProUGUI>();
    }

    void Update(){
        if (CanMove){
           _uiText.text = StateController.Instance.CanMove.ToString();
        }
        else if (CanJump){
           _uiText.text = StateController.Instance.CanJump.ToString();
        }
        else if (CanWallJump){
           _uiText.text = StateController.Instance.CanWallJump.ToString();
        }
        else if (CanDash){
           _uiText.text = StateController.Instance.CanDash.ToString();
        }
        else if (CanOpenSpellbook){
           _uiText.text = StateController.Instance.CanOpenSpellbook.ToString();
        }
        else if (CanDashToLight){
           _uiText.text = StateController.Instance.CanDashToLight.ToString();
        }
        else if (CanDashSoul){
           _uiText.text = StateController.Instance.CanDashSoul.ToString();
        }
        else if (CanModifyPhysics){
           _uiText.text = StateController.Instance.CanModifyPhysics.ToString();
        } else if(Gravity){
            _uiText.text = PlayerController.Instance.GetComponent<Rigidbody2D>().gravityScale.ToString();
        } else if(HoldingJump){
            _uiText.text = Inputs.Instance.HoldingJump.ToString();
        } else if(JumpTimer){
            _uiText.text = Jump.Instance.GetTimeAfterJump().ToString();
        } else if(YValue){
            _uiText.text = "y_vel: " + PlayerController.Instance.GetComponent<Rigidbody2D>().velocity.y.ToString();
        } else if(RigidbodyVelocity){
            _uiText.text = "rigidbody velocity: " + PlayerController.Instance.GetComponent<Rigidbody2D>().velocity.ToString();
        }
    }
}
