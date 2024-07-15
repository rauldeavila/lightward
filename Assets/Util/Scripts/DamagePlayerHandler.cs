using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class DamagePlayerHandler : MonoBehaviour {
    public bool IsSpike = false;
    public UnityEvent OnRespawn;
    public bool IsParticle = false;
    public bool IsAnimatedTile = false;

    private Tilemap tilemap;
    private GameObject crossfadePanel;

    // SpikeController variables
    [HideInInspector]
    public float respawn_x = 0f; // set up on child script SpikeRespawn.cs
    [HideInInspector]
    public float respawn_y = 0f; // set up on child script Spikerespawn.cs
    [HideInInspector]
    public bool respawnFacingRight;

    // AnimatedTilesHandler flags
    private bool _flag0 = false;
    private bool _flag1 = true;
    private bool canHitSpike = true;

    private void Awake() {
        if (IsAnimatedTile) {
            tilemap = GetComponent<Tilemap>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("WizHitBox")) {
            PlayerHealth.Instance.TakeDamage(IsSpike, transform.position);
            if (IsSpike) {
                HandleSpikeCollision();
            }
        }
    }

    private void OnParticleCollision(GameObject collider) {
        if (IsParticle) {
            if (collider.CompareTag("Player")) {
                PlayerHealth.Instance.TakeDamage(IsSpike, transform.position);
            }
        }
    }

    private void Update() {
        if (IsAnimatedTile) {
            UpdateTilemapAnimationFrameRate();
        }
    }

    private void HandleSpikeCollision() {
        if(canHitSpike){
            canHitSpike = false;
            Invoke("CanHitSpikeTrue", 0.3f);
            CameraSystem.Instance.ShakeCamera(1);
            PlayerState.Instance.Jump = false;
            PlayerState.Instance.BeingKnockedBack = true;
            PlayerController.Instance.KnockWizBack(0f, 8f);
            PlayerStats.Instance.DecreaseHealth();

            if (PlayerStats.Instance.GetCurrentHealth() <= 0) {
                StopAllCoroutines();
                PlayerController.Instance.Animator.Play("death");
                return;
            } else {
                StartCoroutine(Fadeout());
            }
        }
    }

    void CanHitSpikeTrue(){
        canHitSpike = true;
    }

    IEnumerator Fadeout() {
        yield return new WaitForSeconds(0.3f);
        crossfadePanel = GameObject.FindGameObjectWithTag("CrossfadePanel");
        crossfadePanel.GetComponent<Animator>().SetTrigger("fadeout");
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn() {
        if(OnRespawn != null){
            OnRespawn.Invoke();
        }
        yield return new WaitForSeconds(0.5f);
        PlayerState.Instance.Grounded = true;

        PlayerController.Instance.Animator.Play("respawn");
        CrossfadePanelController.Instance.FadeIn();
        // crossfadePanel.GetComponent<Animator>().SetTrigger("fadein");
        PlayerController.Instance.SetPlayerPosition(ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("respawn_x").runTimeValue, ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("respawn_y").runTimeValue);
        respawnFacingRight = false;
    }

    private void UpdateTilemapAnimationFrameRate() {
        if (Time.timeScale == 0) {
            if (_flag0) {
                _flag1 = true;
                _flag0 = false;
                tilemap.animationFrameRate = 0;
                tilemap.RefreshAllTiles();
            }
        } else {
            if (_flag1) {
                _flag0 = true;
                _flag1 = false;
                tilemap.animationFrameRate = 1;
            }
        }
    }
}
               
