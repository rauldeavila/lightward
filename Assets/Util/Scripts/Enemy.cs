using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;

public class Enemy : MonoBehaviour {

    [GUIColor(0.988f, 0.322f, 0.439f)]
    public int Health = 1;
    [GUIColor(0.325f, 0.988f, 0.569f)]
    public bool FacingRight = false;
    [GUIColor(0.325f, 0.988f, 0.569f)]
    public bool Grounded = false;
    [GUIColor(0.325f, 0.988f, 0.569f)]
    public bool Flyer = false;



    [GUIColor(0, 0.859f, 0.671f)]
    [FoldoutGroup("Change Defaults")]
    public string LoopingSFXPath;


    private GameObject HitParticlesLeft;
    private GameObject HitParticlesRight;
    private GameObject KilledParticles;

    [GUIColor(0.831f, 0.51f, 0.902f)]
    [FoldoutGroup("Change Defaults")]
    public float Bounciness = 1f;

    [GUIColor(0.831f, 0.51f, 0.902f)]
    [FoldoutGroup("Change Defaults")]
    public Vector2 MoveSpeedNotRigidbody;
    [GUIColor(0.831f, 0.51f, 0.902f)]
    [FoldoutGroup("Change Defaults")]
    public bool IgnoreHitSmokes = false;
    [GUIColor(0.831f, 0.51f, 0.902f)]
    [FoldoutGroup("Change Defaults")]
    public bool EnableColliderAfterDelay = false;
    [GUIColor(0.831f, 0.51f, 0.902f)]
    [FoldoutGroup("Change Defaults")]
    public float DelayForEnableColliders = 0f;

    [GUIColor(0.831f, 0.51f, 0.902f)]
    [FoldoutGroup("Change Defaults")]
    public float ReenableMovementInSeconds = 0.2f;

    [GUIColor(0.831f, 0.51f, 0.902f)]
    [FoldoutGroup("Change Defaults")]
    public float DazedDuration = 0.3f;
    [FoldoutGroup("Change Defaults")]
    public bool TakeDamageFromAnyProjectile = false;

    [GUIColor(0.831f, 0.51f, 0.902f)]
    [FoldoutGroup("Change Defaults")]
    public float KnockbackMultiplier = 8f;
       
    [GUIColor(0.831f, 0.51f, 0.902f)]
    [FoldoutGroup("Change Defaults")]
    public float knockWizBackMultiplier = 2f;
    
    [GUIColor(0.831f, 0.51f, 0.902f)]
    [FoldoutGroup("Change Defaults")]
    public bool InstantiateObjectWhenKilled;

    [GUIColor(0.831f, 0.51f, 0.902f)]
    [FoldoutGroup("Change Defaults")]
    public float HitStopDurationWhenKilled = 0.2f;

    [GUIColor(0.831f, 0.51f, 0.902f)]
    [FoldoutGroup("Change Defaults")]
    public bool DontFlipBasedOnWizPos = false;
    [GUIColor(0.831f, 0.51f, 0.902f)]
    [FoldoutGroup("Change Defaults")]
    public bool ForceFlipBasedOnWizOnAwake = false;
    public bool FlipBasedOnVelocity = false;

    [GUIColor(0.831f, 0.51f, 0.902f)]
    [FoldoutGroup("Change Defaults")]
    public float DeathDelayForKnockback = 0.1f;

    [ShowIf("InstantiateObjectWhenKilled")]
    public GameObject ObjectToInstantiateWhenKilled;

    private Material _hitMaterial;
    private bool _flipCooldown = false;
    private bool _canFlip = true;
    private bool _startedFacingRight = false;
    private bool _dazed = false;
    private Animator _anim;
    private SpriteRenderer _sr;
    private Material _originalMaterial;
    private Vector2 knockbackDirection = Vector2.zero;
    public bool CanTakeHit = true;
    public Rigidbody2D Rb;
    public ParticleSystem ParticlesImpact; // enable / disable

    private bool _killedByEnemyHitProjectile = false;

    // ground check
    public float raycastLength = 10f;
    public float yVariationR1 = 0f;
    public float yVariationR2 = 0f;
    public float xVariationR1 = 0.3f;
    public float xVariationR2 = 0.3f;
    RaycastHit2D raycast1;
    RaycastHit2D raycast2;
    int groundLayerMask;

    private bool _flipFlag = false;
    private bool _lastColliderWasWiz = false;

    private float lastXPos;

    public UnityEvent HitByPlayerEvent;
    public UnityEvent HitByWizHitboxEvent;
    public UnityEvent KilledEvent;

    void Awake(){
        Rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        _originalMaterial = _sr.material;
        _startedFacingRight = FacingRight;
        LoadResources();
        CanTakeHit = true;
        groundLayerMask = LayerMask.GetMask("Ground");
        if(EnableColliderAfterDelay){ // for bear dumb flyer
            Invoke("EnableColliders", DelayForEnableColliders);
        }
        lastXPos = transform.position.x;
        if(ForceFlipBasedOnWizOnAwake){
            Invoke("DoForceFlipBasedOnWizOnAwake", 0.2f);
        }
    }

    void DoForceFlipBasedOnWizOnAwake(){
        // print("DO FORCE FLIP");
        if(PlayerController.Instance.transform.position.x > this.transform.position.x){ // wiz on the right
            if(!FacingRight){
                FacingRight = !FacingRight;
                if(_startedFacingRight){
                    transform.rotation = Quaternion.Euler(0, FacingRight ? 0 : 180, 0);
                } else {
                    transform.rotation = Quaternion.Euler(0, FacingRight ? 180 : 0, 0);
                }
            }
        } else { // wiz on the left
            if(FacingRight){
                FacingRight = !FacingRight;
                if(_startedFacingRight){
                    transform.rotation = Quaternion.Euler(0, FacingRight ? 0 : 180, 0);
                } else {
                    transform.rotation = Quaternion.Euler(0, FacingRight ? 180 : 0, 0);
                }
            }
        }
    }

    void EnableColliders(){
        GetComponent<BoxCollider2D>().enabled = true;
        if(GetComponent<EBPatrol>() != null){
            GetComponent<EBPatrol>().FlipOnWalls = true;
        }
    }

    void LoadResources(){
        HitParticlesLeft = Resources.Load<GameObject>("Particles/HitParticlesLeft");
        HitParticlesRight = Resources.Load<GameObject>("Particles/HitParticlesRight");
        KilledParticles = Resources.Load<GameObject>("Particles/KilledParticles");
        _hitMaterial = Resources.Load<Material>("Materials/" + gameObject.name);
    }

    void OnSignalRaised(){
        print("RAISED!");
    }

    void Update() {
        MoveIfNotRigidbody();
        HandleGrounded();
        HandleFlip();
        LoadResourcesIfNotLoaded();
    }
    void LoadResourcesIfNotLoaded(){
        if(HitParticlesLeft == null || HitParticlesRight == null || KilledParticles == null){
            LoadResources();
        }
    }    

    void MoveIfNotRigidbody(){
        if(!Rb){
            transform.position += (Vector3)MoveSpeedNotRigidbody * Time.deltaTime;
        }
    }

    public void InstantiateHitParticles(){
        if(IgnoreHitSmokes == false){
            if(PlayerController.Instance.AnimatorIsPlaying("attack_up")){
                WizAttackImpact.Instance.CheckImpactPositionAndGenerate(3);
            } else if(PlayerController.Instance.AnimatorIsPlaying("attack_down")) {
                WizAttackImpact.Instance.CheckImpactPositionAndGenerate(2);
            } else { // regular attack
                WizAttackImpact.Instance.CheckImpactPositionAndGenerate(1);
            }
            if (ParticlesImpact != null) { 
                ParticlesImpact.enableEmission = true; 
                Invoke("DisableEmission", 0.5f);
            }
            if(PlayerState.Instance.FacingRight){
                if(HitParticlesRight) Instantiate(HitParticlesRight, this.transform.position, Quaternion.identity);
            } else {
                if(HitParticlesLeft) Instantiate(HitParticlesLeft, this.transform.position, Quaternion.identity);
            }
        }
    }

    public void InstantiateKilledParticles(){
        if(KilledParticles && _lastColliderWasWiz) Instantiate(KilledParticles, this.transform.position, Quaternion.identity);
    }


    public void TriggerHitByPlayerEventIfItExists() {
        if(HitByPlayerEvent != null){
            HitByPlayerEvent.Invoke();
        }
    }

    public void TriggerHitByWizHitboxEventIfItExists() {
        if(HitByWizHitboxEvent != null){
            HitByWizHitboxEvent.Invoke();
        }
    }

    public void TriggerKilledEventIfItExists() {
        if(KilledEvent != null){
            KilledEvent.Invoke();
        }
    }

    // HANDLES DAMAGE

    private void HandleHitLogic(int _playerAttackPower, Vector2 knockbackDirection, Collider2D collider){
        if(CanTakeHit){ // to prevent doubled hits
            if(collider.CompareTag("Projectile") && !TakeDamageFromAnyProjectile){ // if not hit by player, ignore projectiles
                if(collider.GetComponent<Projectile>().LastHitByPlayer == false){
                    return;
                }
            }
            SFXController.Instance.Play("event:/game/00_game/enemy_hit");
            CanTakeHit = false;
            Invoke("CanTakeHitTrue", ReenableMovementInSeconds);
            Health -= _playerAttackPower;
            if(Health <= 0){
                TriggerKilledEventIfItExists();
                TimeManager.Instance.Stop(HitStopDurationWhenKilled);
            }
            TakeHit(collider);
        }
    }

    private void DisableEmission(){
        ParticlesImpact.enableEmission = false; // the null check was on the caller. no need to duplicate it here.
    }

    private void CanTakeHitTrue(){
        CanTakeHit = true;
    }

    public void HitCollider(Collider2D collider) {
        // Damage Wiz
        if(collider.CompareTag("WizHitBox")){
            if(CanTakeHit)
            {
                TriggerHitByWizHitboxEventIfItExists();
                PlayerHealth.Instance.TakeDamage(false, transform.position);
                return;
            }
        }

        if(collider.CompareTag("Player")){
            TriggerHitByPlayerEventIfItExists();
            _lastColliderWasWiz = true;
        } else {
            _lastColliderWasWiz = false;
        }

        // Take Damage
        int _playerAttackPower = 0;
        knockbackDirection = Vector2.zero;
        if(collider.CompareTag("Fireball")){
            _playerAttackPower = PlayerStats.Instance.FireballPower;
            knockbackDirection = (transform.position - collider.transform.position).normalized;
        } else if(collider.CompareTag("Player") || collider.CompareTag("Projectile")){
            _playerAttackPower = PlayerStats.Instance.AttackPower;
            knockbackDirection = (transform.position - PlayerController.Instance.transform.position).normalized;
        }
        // if attack power = 0 it means its not a collider we care -> return. Else, handle hit logic.
        if(_playerAttackPower == 0){ return; } else { HandleHitLogic(_playerAttackPower, knockbackDirection, collider); }
    }

    public void ApplyKnockback(Vector2 knockbackForce, Collider2D collider) {
        if(!Rb){
            if(!collider.CompareTag("Projectile")){
                if(PlayerController.Instance.AnimatorIsPlaying("attack_down")){
                    Jump.Instance.Pogo();
                } else {
                    PlayerController.Instance.KnockBackWizWithForce(-knockbackForce * knockWizBackMultiplier);
                }
                return;
            }
            return;
        }
        if(!collider.CompareTag("Projectile")){
            Rb.velocity = Vector2.zero;
            Rb.velocity = new Vector2(knockbackForce.x, knockbackForce.y);
            if(PlayerController.Instance.AnimatorIsPlaying("attack_down")){
                Jump.Instance.Pogo();
            } else {
                PlayerController.Instance.KnockBackWizWithForce(-knockbackForce * knockWizBackMultiplier);
            }
        } else {
            Rb.velocity = Vector2.zero;
            Rb.velocity = new Vector2(knockbackForce.x, knockbackForce.y);
        }
    }

    private void TakeHit(Collider2D collider){
        CameraSystem.Instance.ShakeCamera(1);
        if(!collider.CompareTag("Projectile")){
            InstantiateHitParticles();
        }
        ApplyKnockback(knockbackDirection * KnockbackMultiplier, collider);
        StartCoroutine(FlashSprite());
        DazeEnemy();
        HandleBat();
        if(Health <= 0){
            if(collider.CompareTag("Projectile")){
                if(collider.GetComponent<Projectile>() != null){
                    if(collider.GetComponent<Projectile>().LastHitByPlayer == false){
                        _killedByEnemyHitProjectile = true;
                    }
                }
            }
            Invoke("KillEnemy", DeathDelayForKnockback);
        }
    }

    private IEnumerator FlashSprite(){
        if(_hitMaterial != null){
            _sr.material = _hitMaterial;
        }
        yield return new WaitForSeconds(0.2f);
        _sr.material = _originalMaterial;
    }

   public void DazeEnemy(){
        _dazed = true;
        if(_anim != null && DazedDuration != 0f)
            _anim.speed = 0.1f;

        Invoke("DazedOff", DazedDuration);
    }

    private void DazedOff(){
        _dazed = false;
        if(_anim != null)
            _anim.speed = 1f;
    }

    private void KillEnemy(){
        CancelInvoke("DazedOff");
        Instantiate(KilledParticles, this.transform.position, Quaternion.identity);
        if(InstantiateObjectWhenKilled && ObjectToInstantiateWhenKilled != null){
            GameObject newObject = Instantiate(ObjectToInstantiateWhenKilled, this.transform.position, Quaternion.identity);
            Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }
        }
        if(!_killedByEnemyHitProjectile){
            PlayerStats.Instance.IncreaseYellowHealth();
        }
        if(ParticlesImpact != null){
            ParticlesImpact.transform.SetParent(null);
        }
        this.gameObject.SetActive(false);
    }

    public void PlayAnimationIfExist(string animationName) {
        if (AnimationClipExists(animationName)){
            _anim.Play(animationName);
        }
    }

    private bool AnimationClipExists(string animationName) {
        if (_anim.runtimeAnimatorController == null) {
            return false;
        }
        foreach (AnimationClip clip in _anim.runtimeAnimatorController.animationClips) {
            if (clip.name == animationName) {
                return true;
            }
        }
        return false;
    }

    private void HandleGrounded(){
        raycast1 = Physics2D.Raycast(new Vector3(transform.position.x + xVariationR1, transform.position.y + yVariationR1, 0f), Vector2.down, raycastLength, groundLayerMask);
        raycast2 = Physics2D.Raycast(new Vector3(transform.position.x + xVariationR2, transform.position.y + yVariationR2, 0f), Vector2.down, raycastLength, groundLayerMask);
        if(!Flyer){
            if(raycast1 || raycast2){
                Grounded = true;
                if(_flipFlag){
                    _flipFlag = false;
                    EnableFlip();
                }
            } else {
                if(_flipFlag == false){
                    _flipFlag = true;
                    DisableFlip();
                }
                Grounded = false;
            }
        }
    }

    public void SetRBVelocity(float x, float y){
        if(x == 0 && y == 0){
            Rb.velocity = Vector2.zero;
        } else {
            Rb.velocity =  new Vector2(x, y);
        }
    }

    public void SetVelocityNoRb(float x, float y){
        if(x == 0 && y == 0){
            MoveSpeedNotRigidbody = Vector2.zero;
        } else {
            MoveSpeedNotRigidbody = new Vector2(x, y);
        }
    }


    public void SetRBConstraints(bool x = false, bool y = false, bool z = false) {
        if (!x && !y && !z) {
            Rb.constraints = RigidbodyConstraints2D.None;
            return;
        }
        RigidbodyConstraints2D constraints = RigidbodyConstraints2D.None;
        if (x) {
            constraints |= RigidbodyConstraints2D.FreezePositionX;
        }
        if (y) {
            constraints |= RigidbodyConstraints2D.FreezePositionY;
        }
        if (z) {
            constraints |= RigidbodyConstraints2D.FreezeRotation;
        }
        Rb.constraints = constraints;
    }


    private void HandleFlip(){
        if(FlipBasedOnVelocity){
            float currentXPos = transform.position.x;

            if(currentXPos > lastXPos){
                if(!FacingRight){
                    _canFlip = true;
                    Flip();
                }
            } else if(currentXPos < lastXPos){
                if(FacingRight){
                    _canFlip = true;
                    Flip();
                }
            }

            lastXPos = currentXPos;
            return;
        } else if(!DontFlipBasedOnWizPos){
            if(PlayerController.Instance.transform.position.x > this.transform.position.x){ // wiz on the right
                if(!FacingRight){
                    Flip();
                }
            } else { // wiz on the left
                if(FacingRight){
                    Flip();
                }
            }
        }
    }

    // Called from other AIs

    public void FlipBasedOnWizPos(){
        if(PlayerController.Instance.transform.position.x > this.transform.position.x){ // wiz on the right
            if(!FacingRight){
                Flip();
            }
        } else { // wiz on the left
            if(FacingRight){
                Flip();
            }
        }
    }
    public void Flip(){
        if(!_flipCooldown && _canFlip){
            _flipCooldown = true;
            Invoke("FlipCooldownOff", 0.3f);
            FacingRight = !FacingRight;
            if(_startedFacingRight){
                transform.rotation = Quaternion.Euler(0, FacingRight ? 0 : 180, 0);
            } else {
                transform.rotation = Quaternion.Euler(0, FacingRight ? 180 : 0, 0);
            }
        }
    }

    public void DisableFlip(){
        _canFlip = false;
    }

    public void EnableFlip(){
        _canFlip = true;
    }

    private void FlipCooldownOff(){
        _flipCooldown = false;
    }


    public bool GetDazed(){
        return _dazed;
    }

    public bool AnimatorIsPlaying(string stateName){
        if(_anim != null){
            return _anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        } else{
            return false;
        }
    }

    private void HandleBat(){
        if(GetComponent<Bat>() != null){
            Bat _bat;
            _bat = GetComponent<Bat>();
            if(_bat.Based){
                _bat.StartFlying();
            }
        }
    }

    private void OnDrawGizmos()  {
        RaycastHit2D raycast1Gizmo = Physics2D.Raycast(new Vector3(transform.position.x + xVariationR1, transform.position.y + yVariationR1, 0f), Vector2.down, raycastLength, groundLayerMask);
        RaycastHit2D raycast2Gizmo = Physics2D.Raycast(new Vector3(transform.position.x + xVariationR2, transform.position.y + yVariationR2, 0f), Vector2.down, raycastLength, groundLayerMask);
        
        Gizmos.color = Color.white;
        Gizmos.DrawRay(new Vector3(transform.position.x + xVariationR1, transform.position.y + yVariationR1, 0f), Vector2.down * raycastLength);
        Gizmos.DrawRay(new Vector3(transform.position.x + xVariationR2, transform.position.y + yVariationR2, 0f), Vector2.down * raycastLength);
    }




}
