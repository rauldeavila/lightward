using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour {

    InputDevice lastDevice;

    public Rigidbody2D Rigidbody2D { get => GetComponent<Rigidbody2D>(); }
    public PlayerState State { get => GetComponent<PlayerState>(); }
    public Animator Animator { get => GetComponentInChildren<Animator>(); }
   
    public bool canLook = false; // set on animator;
    private bool _jumpFlag = false;
    private Vector3 zeroedVelocity; 

    public static PlayerController Instance;

    private bool _editor = false;

    #region Unity Events
    private void Awake() {

        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 

        zeroedVelocity = Vector3.zero;


#if UNITY_EDITOR
            _editor = true;
#endif
        if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("game_loading_savefile").runTimeValue){
            ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("game_loading_savefile", false);
            print("Loading save file!");
            SetPlayerPosition(ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_x").runTimeValue, ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_y").runTimeValue);
            if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("wiz_facing_right").runTimeValue  == false){
                Move.Instance.Flip();
            }
            this.Animator.Play("sit");
            StartCoroutine(UIAnimated());
            PlayerState.Instance.Sit = true;
            PlayerState.Instance.Grounded = true;
        } else if(_editor){
            if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("game_changing_scene").runTimeValue){
                SetPlayerPosition(ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_x").runTimeValue, ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_y").runTimeValue);
                if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("wiz_facing_right").runTimeValue  == false){
                    Move.Instance.Flip();
                }
                if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("wiz_new_scene_dashing_soul").runTimeValue){
                    this.Animator.Play("dashingsoul_dashing");
                    this.State.DashingSoul = true;
                    Invoke("StopMoving",0f);
                    TriggerHealthUINOIntro();
                    TriggerMagicUINOIntro();
                } else if (ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("wiz_new_scene_falling").runTimeValue){
                    this.Animator.Play("fall");
                    Invoke("StopMoving",0f);
                    TriggerHealthUINOIntro();
                    TriggerMagicUINOIntro();
                } else if (ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("wiz_new_scene_up").runTimeValue){
                    this.Animator.Play("idle");
                    Invoke("StopMoving",0f);
                    TriggerHealthUINOIntro();
                    TriggerMagicUINOIntro();
                } else {
                    this.Animator.Play("run");
                    Invoke("StopMoving", 0.3f); // wiz anda sozinho enquanto o game_changing_scene for true.
                    // print("not a new game - changed scene");
                    PlayerState.Instance.Sit = false;
                    TriggerHealthUINOIntro();
                    TriggerMagicUINOIntro();
                    PlayerState.Instance.Grounded = true;
                }
            } else {
                if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("game_new_game").runTimeValue){
                    this.Animator.Play("bedtime");
                    // this.Animator.Play("lay");
                    PlayerState.Instance.Sit = true;
                    PlayerState.Instance.Grounded = true;
                    SetPlayerPosition(ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_x").runTimeValue, ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_y").runTimeValue);
                } else {
                    if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("wiz_facing_right")  == false){
                        Move.Instance.Flip();
                    }
                    // configs for game booted
                    this.Animator.Play("idle"); // CHANGE THIS IF WANT TO SIT ON CAMPFIRES -----------------------------------------------------
                    PlayerState.Instance.Sit = false;
                    StartCoroutine(UIAnimated());
                    // print("not a new game - loading from campfire");
                    PlayerState.Instance.Grounded = true;
                }
            }
        } else{
            if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("game_changing_scene").runTimeValue){
                SetPlayerPosition(ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_x").runTimeValue, ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_y").runTimeValue);
                if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("wiz_facing_right")  == false){
                    Move.Instance.Flip();
                }
                if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("wiz_new_scene_dashing_soul").runTimeValue){
                    this.Animator.Play("dashingsoul_dashing");
                    this.State.DashingSoul = true;
                    Invoke("StopMoving",0f);
                    TriggerHealthUINOIntro();
                    TriggerMagicUINOIntro();
                } else if (ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("wiz_new_scene_falling").runTimeValue){
                    this.Animator.Play("fall");
                    Invoke("StopMoving",0f);
                    TriggerHealthUINOIntro();
                    TriggerMagicUINOIntro();
                } else if (ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("wiz_new_scene_up").runTimeValue){
                    this.Animator.Play("idle");
                    Invoke("StopMoving",0f);
                    TriggerHealthUINOIntro();
                    TriggerMagicUINOIntro();
                } else {
                    this.Animator.Play("run");
                    Invoke("StopMoving", 0.3f); // wiz anda sozinho enquanto o game_changing_scene for true.
                    // print("not a new game - changed scene");
                    PlayerState.Instance.Sit = false;
                    TriggerHealthUINOIntro();
                    TriggerMagicUINOIntro();
                    PlayerState.Instance.Grounded = true;
                }
            } else {
                if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("game_new_game").runTimeValue){
                    this.Animator.Play("bedtime");
                    PlayerState.Instance.Sit = true;
                    // print("new game");
                    PlayerState.Instance.Grounded = true;
                } else {
                    if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("wiz_facing_right")  == false){
                        Move.Instance.Flip();
                    }
                    this.Animator.Play("sit");
                    StartCoroutine(UIAnimated());
                    // print("not a new game - loading from campfire");
                    PlayerState.Instance.Sit = true;
                    PlayerState.Instance.Grounded = true;
                }
            }
            SetPlayerPosition(ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_x").runTimeValue, ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_y").runTimeValue);
            if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("wiz_facing_right")  == false){
                Move.Instance.Flip();
            }
        }
    }

    void StopMoving(){
        ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("game_changing_scene").runTimeValue = false;
        Inputs.Instance.LeftArrowCanceled();
        Inputs.Instance.RightArrowCanceled();
    }

    private void Start() {
        Inputs.Instance.Controls.Game.Jump.performed += ctx => lastDevice = ctx.control?.device;
        Inputs.Instance.Controls.Game.Attack.performed += ctx => lastDevice = ctx.control?.device;
        Inputs.Instance.Controls.Game.Dash.performed += ctx => lastDevice = ctx.control?.device;
    }

    void FixedUpdate(){
        if(Animator != null){
            Animator.SetBool("holdingUp", Inputs.Instance.HoldingUpArrow);
        }
    }

    void Update(){
        Animator.speed = Time.timeScale;
        CheckIfCanLook();
    }

    #endregion


    public bool AnimatorIsPlaying(string stateName){
        if(Animator != null){
            return Animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        } else{
            return false;
        }
    }

    public void SetPlayerPosition(float newX, float newY, bool saving = true){
        this.transform.position = new Vector3(newX, newY,  this.transform.position.z);
        if(saving)
        {
            ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_x").runTimeValue = newX; 
            ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_y").runTimeValue = newY;
        }
    }

    public void DisablePlayerControls(){
        Move.Instance.enabled = false;
        StateController.Instance.CanJump = false;
        // DashController.Instance.DisableDash();
        //DashController.enabled = false;
    }

    public void EnablePlayerControls(){
        Animator.SetBool("paused", false);
        Move.Instance.enabled = true;
        StateController.Instance.CanJump = true;
        // DashController.Instance.EnableDash();

        //DashController.enabled = true;
    }

    public void DisablePlayerAttack(){
        PlayerCombat.Instance.enabled = false;
    }

    public void EnablePlayerAttack(){
        PlayerCombat.Instance.enabled = true;
    }

    public void DecreaseTransform(float amount){
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - amount, this.transform.position.z);
    }


    IEnumerator UIAnimated(){
        yield return new WaitForSeconds(0.5f);
        TriggerHealthUIIntro();
        TriggerMagicUIIntro();
    }
    
    public void TriggerHealthUIIntro(){
        // HealthUIController.Instance.SetIntroTrigger();
    }

    public void TriggerMagicUIIntro(){
        // TODO
        // MagicUIController.Instance.SetIntroTrigger();
    }

    public void TriggerHealthUINOIntro(){
        HealthUIController.Instance.SetNoIntroTrigger();
    }

    public void TriggerMagicUINOIntro(){
        // TODO
        // MagicUIController.Instance.SetNoIntroTrigger();
    }

    public void SetGravityToZero(){
        GetComponent<Rigidbody2D>().gravityScale = 0f;
    }

    public void SetGravityToOne(){
        GetComponent<Rigidbody2D>().gravityScale = 1f;
    }

    public void SetGravityTo(float _newGravity){
        GetComponent<Rigidbody2D>().gravityScale =_newGravity;
    }

    public void SetGravityToElevator(){
        GetComponent<Rigidbody2D>().gravityScale = 2000f;
    }

    public void FreezeRigidbody(){
        Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);  
    }

    public void UnfreezeRigidbody(){
        Rigidbody2D.constraints = RigidbodyConstraints2D.None;
        Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }


    public void CheckIfCanLook(){
        if(Inputs.Instance.HoldingLeftArrow || Inputs.Instance.HoldingRightArrow || AnimatorIsPlaying("attack") || AnimatorIsPlaying("attack2") || AnimatorIsPlaying("attack1-ending") || 
        AnimatorIsPlaying("attack2-ending") || AnimatorIsPlaying("run") || AnimatorIsPlaying("run_landing") || 
        AnimatorIsPlaying("sit") || AnimatorIsPlaying("sitting") || AnimatorIsPlaying("lay") ||
        AnimatorIsPlaying("dashingsoul_begin") || AnimatorIsPlaying("dashingsoul_dashing") || AnimatorIsPlaying("dashingsoul_stopping") ||
        AnimatorIsPlaying("attack_up") || GameState.Instance.Paused || GameState.Instance.MapOpened || GameState.Instance.InventoryOpened){
            canLook = false;
        }
        
    }

    public void StopWiz(){
        print("STOP WIZ - Playing Idle");
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Animator.SetFloat("horizontal", 0);
        Animator.SetFloat("vertical", 0);
        Animator.SetTrigger("idle");
        Animator.SetBool("paused", true);
        Animator.Play("idle");
    }

    
    public void StopWizWithoutChangingAnimation(){
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Animator.SetFloat("horizontal", 0);
        Animator.SetFloat("vertical", 0);
    }


    public void KnockBackWizWithForce(Vector2 knockbackForce){
        GetComponent<Rigidbody2D>().AddForce(knockbackForce, ForceMode2D.Impulse);
    }

    public void KnockWizBack(float force_x, float force_y, bool StoppingWiz = false){
        if(StoppingWiz){
            StopWiz();
            SetGravityToOne();
        }

        if(force_x == 0f){
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0f);  
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, force_y);  
        } else if(force_y == 0f){
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, GetComponent<Rigidbody2D>().velocity.y);  
            GetComponent<Rigidbody2D>().velocity = new Vector2(force_x, GetComponent<Rigidbody2D>().velocity.y);  
        } else{
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);  
            GetComponent<Rigidbody2D>().velocity = new Vector2(force_x, force_y);  
        }
    }

    public void SetVelocityToZero(){
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void SetVelocityTo(Vector2 newVelocity){
        GetComponent<Rigidbody2D>().velocity = newVelocity;
    }

    public void SetVelocityToV3(Vector3 newVelocity){
        GetComponent<Rigidbody2D>().velocity = newVelocity;
    }

    public void DashingSoul(float speed){
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        if(PlayerState.Instance.FacingRight){
            Vector2 targetVelocity = new Vector2(speed, 0f);
            GetComponent<Rigidbody2D>().velocity = Vector3.SmoothDamp(GetComponent<Rigidbody2D>().velocity, targetVelocity, ref zeroedVelocity, 0.05f);
        } else{
            Vector2 targetVelocity = new Vector2(-speed, 0f);
            GetComponent<Rigidbody2D>().velocity = Vector3.SmoothDamp(GetComponent<Rigidbody2D>().velocity, targetVelocity, ref zeroedVelocity, 0.05f);
        }
    }

    public float GetVerticalVelocity(){
        return GetComponent<Rigidbody2D>().velocity.y;
    }

    public float GetHorizontalVelocity(){
        return GetComponent<Rigidbody2D>().velocity.x;
    }

    public string GetCurrentAnimation(){
        return Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
    }


    public void SetGravityToOneInSeconds(float time){
        Invoke("SetGravityToOne", time);
    }

    public void AddNewHeartContainerToWiz(){
        Invoke("AddNewHeart", 1f);
    }

private IEnumerator AnimateHeartFilling() {
    FloatValue wizHealth = ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_health");

    float currentHealth = wizHealth.runTimeValue;
    float currentMaxValue = wizHealth.maxValue;
    float totalDuration = 1.5f;  // Total time for the whole sequence
    float elapsed = 0.0f;
    
    while (currentHealth < currentMaxValue) {
        elapsed += Time.deltaTime;
        
        // Cubic Easing Out: y = 1 - (1 - x) * (1 - x) * (1 - x)
        float t = 1 - Mathf.Pow(1 - (elapsed / totalDuration), 3);

        float newHealth = Mathf.Lerp(currentHealth, currentMaxValue, t);
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("wiz_health", newHealth);

        if (newHealth >= currentMaxValue) {
            break;
        }

        yield return null;
    }

    // Add 1 to the max value and final heart animation
    float newMaxValue = currentMaxValue + 1;
    ScriptableObjectsManager.Instance.SetMaxValueForFloatObject("wiz_health", newMaxValue);
    ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("wiz_health", newMaxValue);

    yield return new WaitForSeconds(0.5f);  // Add a slight delay for impact
}


private void AddNewHeart() {
    StartCoroutine(AnimateHeartFilling());
}


}
