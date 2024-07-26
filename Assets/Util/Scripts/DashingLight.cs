using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DashingLight : MonoBehaviour {

    public bool FakeHoldingJump = false;
    public float yUpvel = 15;
    public float yDownvel = -8;
    public float dashingLightSpeed;
    private Vector3 lastTarget;
    public bool Cooldown = false;
    public bool KickedOutOfLight = false;
    private bool _dashingToLight = false;
    private bool _nearLightAtX = false;
    private bool _nearLightAtY = false;

    public Vector2 DroppingLightVelocity = new Vector2(0, 0);

    public static DashingLight Instance;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }

    void FixedUpdate(){
        if(KickedOutOfLight)
        {
            if(PlayerState.Instance.Grounded)
            {
                KickedOutOfLight = false;
                Cooldown = false;
                return;
            }
            Cooldown = true;
            return;
        }
        _nearLightAtX =  (Mathf.Abs(lastTarget.x - transform.position.x) < 0.6f || Mathf.Abs(transform.position.x - lastTarget.x) < 0.6f);
        _nearLightAtY = (Mathf.Abs(lastTarget.y - transform.position.y) < 0.6f || Mathf.Abs(transform.position.y - lastTarget.y) < 0.6f);
        if(PlayerState.Instance.DashingLight){
            if(_nearLightAtX && _nearLightAtY){ // está dentro da luz
                if (!PlayerState.Instance.InsideLight)
                {
                    Rigidbody2D playerRigidbody = PlayerController.Instance.GetComponent<Rigidbody2D>();
                    Vector2 velocityForExitingLight = playerRigidbody.velocity;
                    // print("Entrou na luz. RB Velocity: " + velocityForExitingLight.ToString());

                    if (velocityForExitingLight.y > 15f)
                    {
                        // print("Limitando velocidade pra cima");
                        velocityForExitingLight.y = yUpvel;
                    }
                    else if (velocityForExitingLight.y < -8f)
                    {
                        // print("Limitando velocidade pra baixo");
                        velocityForExitingLight.y = yDownvel;
                    }
                    // print("New Velocity: " + velocityForExitingLight.ToString());
                    DroppingLightVelocity = velocityForExitingLight;
                    // print("Updated RB Velocity: " + playerRigidbody.velocity.ToString());
                }
                SetToInsideLight();
            } else{
                SetToOutsideLight();
            }
        }
    }

    void SetToInsideLight(){
        _dashingToLight = false;
        PlayerState.Instance.InsideLight = true;
        PlayerController.Instance.Animator.SetBool("atLight", true);
        PlayerController.Instance.FreezeRigidbody();
        PlayerController.Instance.SetVelocityToZero();
    }

    void SetToOutsideLight(){
        _dashingToLight = true;
        PlayerState.Instance.InsideLight = false;
        PlayerController.Instance.Animator.SetBool("atLight", false);
    }

    

    public void HandleDashingLightButtonPress(){
        if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("hero_dashing_light").runTimeValue == true){
            if(StateController.Instance.CanDashToLight && !Cooldown){
                if(!PlayerController.Instance.AnimatorIsPlaying("dash") & !PlayerController.Instance.AnimatorIsPlaying("dodge")){
                    DashToLight();
                }
            }
        }
    }

    private void DashToLight(){
        Cooldown = true;
        _dashingToLight = true;
        PlayerState.Instance.Jump = false;
        PlayerState.Instance.WallJump = false;
        StateController.Instance.CanDashToLight = false;
        if(PlayerController.Instance.Animator.GetBool("atLight")){
            PlayerController.Instance.Animator.SetBool("atLight", false);
            PlayerController.Instance.UnfreezeRigidbody();
        } else{
            PlayerController.Instance.Animator.Play("dashing_light_starting");
        }
        lastTarget = PlayerState.Instance.TargetLightPosition;
        PlayerState.Instance.DashingLight = true;
        Vector3 dir = (PlayerState.Instance.TargetLightPosition - transform.position).normalized * dashingLightSpeed;
        PlayerController.Instance.SetVelocityToZero();
        PlayerController.Instance.SetGravityToZero();
        PlayerController.Instance.SetVelocityToV3(dir);

    }

    public void DropFromLight(){ // called from Jump.cs
        PlayerController.Instance.SetGravityToOne();
        PlayerController.Instance.Animator.SetBool("atLight", false);
        if(DroppingLightVelocity.y > 0){
            FakeHoldingJump = true;
            PlayerController.Instance.Animator.Play("jump");
        } else {
            PlayerController.Instance.Animator.Play("fall");
        }
        PlayerController.Instance.UnfreezeRigidbody();
        PlayerController.Instance.SetVelocityTo(DroppingLightVelocity);
        PlayerState.Instance.DashingLight = false;
        PlayerState.Instance.InsideLight = false;
        StateController.Instance.CanDash = true;
        Invoke("DisableCooldown", 0.2f);
        Invoke("LetJumpGo", 0.5f);
    }

    public void DisableCooldown(){ // called from DashingLightReceiver.cs and StateController.cs
        Cooldown = false;
        KickedOutOfLight = false;
    }

    public void LetJumpGo(){
        FakeHoldingJump = false;
    }
    

}