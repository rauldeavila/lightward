using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class PlayerHealth : MonoBehaviour {

    public static PlayerHealth Instance;

    public float InvulnerableDuration = 1.5f;
    public float HitDuration = 1f;

    [HideInInspector]
    public Vector2 difference = Vector2.zero;
    public MMFeedbacks wizHitFeedback;
    public MMFeedbacks damageFeedback;

    public GameObject cloneParticles;

    FMOD.Studio.EventInstance HitEffectAmbience;

    private bool _lowPitchIsOn = false;


    private void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        }
    }

    void Update(){
        if(!PlayerState.Instance.Invulnerable && HitEffectAmbience.isValid()){
            ReturnSoundToNormal();
        }
        if((PlayerStats.Instance.GetCurrentHealth() + PlayerStats.Instance.GetYellowHealth())<= 1f){
            cloneParticles.SetActive(true);
        } else{
            cloneParticles.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("HitParticles")){
            TakeDamage(false, Vector2.zero);
        }
    }

    public void InvulnerableOn(){
        PlayerState.Instance.Invulnerable = true; 
    }

    public void InvulnerableOff(){
        PlayerState.Instance.Invulnerable = false; 
    }

    public void TakeDamage(bool spike, Vector2 hitterPosition) {
        if(!Move.Instance.IsNoClipActive)
        {
            if(PlayerState.Instance.Invulnerable == false){
                PlayerState.Instance.Hit = true;
                PlayerState.Instance.Invulnerable = true;
                Invoke("SetInvulnerabilityBackToFalse", InvulnerableDuration);
                Invoke("SetHitBackToFalse", HitDuration);
                if(_lowPitchIsOn == false){
                    _lowPitchIsOn = true;
                    HitEffectAmbience = FMODUnity.RuntimeManager.CreateInstance("snapshot:/HitEffect");
                    HitEffectAmbience.start();
                    StartCoroutine(ReturnSoundToNormalAfterDelay(1f));
                }
                difference = hitterPosition - new Vector2(transform.position.x, transform.position.y);

                damageFeedback?.PlayFeedbacks();
                if(!spike){
                    wizHitFeedback?.PlayFeedbacks();
                    PlayerStats.Instance.DecreaseHealth();
                    // spikes hitam no spikecontroller;
                }

                if(PlayerStats.Instance.GetCurrentHealth() <= 0){
                    PlayerState.Instance.Dead = true;
                    PlayerController.Instance.Animator.Play("death");
                    return;
                } else{
                    if(!spike){
                        PlayerController.Instance.Animator.SetFloat("vertical", 0.5f);
                        PlayerController.Instance.Animator.Play("hit");
                    } else {
                        PlayerController.Instance.Animator.Play("spike");
                    }
                }
                PlayerState.Instance.DashingSoul = false;
            } else {
                if(PlayerStats.Instance.GetCurrentHealth() <= 0){
                    print("MORREU INVENCIVEL!");
                    PlayerController.Instance.Animator.Play("death");
                    return;
                }
            }
        }
    }

    private IEnumerator ReturnSoundToNormalAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        ReturnSoundToNormal();
    }

    public void ReturnSoundToNormal(){
        HitEffectAmbience.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        HitEffectAmbience.release();
        _lowPitchIsOn = false;
    }

    private void SetInvulnerabilityBackToFalse(){
        PlayerState.Instance.Invulnerable = false;
    }

    private void SetHitBackToFalse(){
        PlayerState.Instance.Hit = false;
    }



}
