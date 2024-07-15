using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Skely : MonoBehaviour {

    [Header("Last attacks control")] 
    public string lastAttack = "";
    public string secondTolastAttack = "";
    private bool blinking = false;
    

    [Header("Instantiates")]
    public GameObject Layer2At1;
    public GameObject Layer2At0;
    public GameObject LeftSkullHeadRegular;
    public GameObject LeftSkullHeadFaster;
    public GameObject RightSkullHeadRegular;
    public GameObject RightSkullHeadFaster;
    public GameObject SpikesBase;
    public GameObject Spike1;
    public GameObject Spike2;
    public GameObject Spike3;
    public GameObject Minion;
    public GameObject GroundParticlesTelegraph;

    public BoolValue boss1;
    public GameObject headThrowPos;
    public GameObject fog;
    public GameObject fmodZeroTrigger;
    public GameObject ParticlesAndKilledSFX;
    
    public GameObject forceFieldArea;
    public GameObject camInfluence;
    public GameObject particlesBurst;
    public GameObject smokeBurst;
    public GameObject battleTrigger;
    public BoolValue pickedKey;
    private bool awake = false;
    private Animator animator;
    private Rigidbody2D rb;
    private TakeDamage damage;
    private TakeHit hit;

    public GameObject forceField1;
    public GameObject forceField2;
    
    public GameObject target1Left;
    public GameObject target2Left;
    public GameObject target3Left;
    public GameObject target4Left;
    public GameObject target5Left;
    public GameObject target6Left;
    public GameObject target7Left;
    public GameObject target8Left;

    public GameObject target1Right;
    public GameObject target2Right;
    public GameObject target3Right;
    public GameObject target4Right;
    public GameObject target5Right;
    public GameObject target6Right;
    public GameObject target7Right;
    public GameObject target8Right;


    public GameObject mPos1;
    public GameObject mPos2;
    public GameObject mPos3;
    public GameObject mPos4;

    public GameObject sPos1;
    public GameObject sPos2;
    public GameObject sPos3;
    public GameObject sPos4;


    [FMODUnity.EventRef]
    public string SkelyPreIntro = "";
    [FMODUnity.EventRef]
    public string SkelyIntro = "";
    [FMODUnity.EventRef]
    public string SkelyHeadThrow = "";
    [FMODUnity.EventRef]
    public string SkelySpikesAntecipation = "";
    [FMODUnity.EventRef]
    public string SkelySpikes = "";
    [FMODUnity.EventRef]
    public string SkelyDeath = "";

    public bool canAttack = false;

    public float colorDuration = 0.5f;
    private EnemyBaseScript enemy;
    private BlinkSpriteRenderer blink;


    
    void Awake() {
        enemy = GetComponent<EnemyBaseScript>();
        animator = GetComponent<Animator>();
        rb = GetComponentInChildren<Rigidbody2D>();
        damage = GetComponentInChildren<TakeDamage>();
        hit = GetComponentInChildren<TakeHit>();
        blink = GetComponentInChildren<BlinkSpriteRenderer>();
        damage.canTakeDamage = false;
        hit.canTakeHit = false;
    }

    public void EnableAttackOnAnimator() {
        canAttack = true;
    }

    public void EnableForceField() {
        forceField1.SetActive(true);
        forceField2.SetActive(true);
    }

    public void DisableForceField() {
        forceField1.GetComponent<ParticleSystem>().Stop();
        forceField1.SetActive(false);
        forceField2.GetComponent<ParticleSystem>().Stop();
        forceField2.SetActive(false);
    }
    
    void FixedUpdate() {
        if(awake){
            enemy.FlipBasedOnWizPosition();
            PickAttack();
            Layer2At1.SetActive(true);
        }
        HandleEndOfBattle();
    }

    void PickAttack() {
        if (canAttack) {
            if (damage.health >= 40) {
                HandleFirstPhase();
            } else {
                HandleSecondPhase();
                if (blinking == false) {
                    blinking = true;
                    BlinkSprite();
                }
            }
        }
    }

    void HandleFirstPhase() {
        canAttack = false;
        if (lastAttack != "" && (lastAttack == secondTolastAttack)) {
            if (lastAttack == "head") {
                animator.Play("summon");
                secondTolastAttack = lastAttack;
                lastAttack = "minions";
            } else if (lastAttack == "minions") {
                animator.Play("head_throw");
                secondTolastAttack = lastAttack;
                lastAttack = "head";
            }
        } else {
            // pick between throwing head and summoning minions
            int attack = Random.Range(1, 3); // random between 1 and 2
            if (attack == 1) {
                animator.Play("head_throw");
                secondTolastAttack = lastAttack;
                lastAttack = "head";
            } else if (attack == 2) {
                animator.Play("summon");
                secondTolastAttack = lastAttack;
                lastAttack = "minions";
            }
            
        }
    }

    public void HeadThrow() { // animator
        if (enemy.facingRight) {
            Instantiate(RightSkullHeadRegular, headThrowPos.transform.position, quaternion.identity);
        } else {
            Instantiate(LeftSkullHeadRegular, headThrowPos.transform.position, quaternion.identity);
        }
    }

    public void Summon() { // animator
        int pos1 = Random.Range(1, 3); // random between 1 and 2
        int pos2 = Random.Range(3, 5); // random between 3 and 4
        if (pos1 == 1) {
            Instantiate(GroundParticlesTelegraph, mPos1.transform.position, quaternion.identity);
            StartCoroutine(SummonMinion(1));
        } else if (pos1 == 2) {
            Instantiate(GroundParticlesTelegraph, mPos2.transform.position, quaternion.identity);
            StartCoroutine(SummonMinion(2));
        }        
        
        if (pos2 == 3) {
            Instantiate(GroundParticlesTelegraph, mPos3.transform.position, quaternion.identity);
            StartCoroutine(SummonMinion(3));
        } else if (pos2 == 4) {
            Instantiate(GroundParticlesTelegraph, mPos4.transform.position, quaternion.identity);
            StartCoroutine(SummonMinion(4));
        }
        
    }

    IEnumerator SummonMinion(int pos) {
        yield return new WaitForSeconds(2f);
        switch (pos) {
            case 1:
                Instantiate(Minion, mPos1.transform.position, quaternion.identity);
                break;
            case 2:
                Instantiate(Minion, mPos2.transform.position, quaternion.identity);
                break;
            case 3:
                Instantiate(Minion, mPos3.transform.position, quaternion.identity);
                break;
            case 4:
                Instantiate(Minion, mPos4.transform.position, quaternion.identity);
                break;
            default:
                break;
        }
        
    }

    void HandleSecondPhase() {
        canAttack = false;
        if (lastAttack != "" && (lastAttack == secondTolastAttack)) {
            if (lastAttack == "faster_head") {
                animator.Play("spikes");
                secondTolastAttack = lastAttack;
                lastAttack = "spikes";
            } else if (lastAttack == "spikes") {
                animator.Play("faster_head_throw");
                secondTolastAttack = lastAttack;
                lastAttack = "faster_head";
            }
        } else {
            // pick between throwing head and spikes from ground
            int attack = Random.Range(1, 3); // random between 1 and 2
            if (attack == 1) {
                animator.Play("faster_head_throw");
                secondTolastAttack = lastAttack;
                lastAttack = "faster_head";
            } else if (attack == 2) {
                animator.Play("spikes");
                secondTolastAttack = lastAttack;
                lastAttack = "spikes";
            }
            
        }
    }

    public void FasterHeadThrow() { // animator
        if (enemy.facingRight) {
            Instantiate(RightSkullHeadFaster, headThrowPos.transform.position, quaternion.identity);
        } else {
            Instantiate(LeftSkullHeadFaster, headThrowPos.transform.position, quaternion.identity);
        }  
    }

    public void Spikes() { // animator
        int pos1 = Random.Range(0, 3); // random between 0 and 2
        int pos2 = Random.Range(3, 5); // random between 3 and 4

        Instantiate(GroundParticlesTelegraph, this.transform.position, quaternion.identity); // for base spikes
        
        if (pos1 == 0) {
            int left = Random.Range(0, 1); // random between 0 and 1 (left and right)
            if (left == 0) {
                Instantiate(GroundParticlesTelegraph, sPos1.transform.position, quaternion.identity);
                Instantiate(GroundParticlesTelegraph, sPos2.transform.position, quaternion.identity);
                StartCoroutine(SpawnSpikes(0, false));
            } else {
                Instantiate(GroundParticlesTelegraph, sPos3.transform.position, quaternion.identity);
                Instantiate(GroundParticlesTelegraph, sPos4.transform.position, quaternion.identity);
                StartCoroutine(SpawnSpikes(0, true));
            }
        } else if (pos1 == 1) {
            Instantiate(GroundParticlesTelegraph, sPos1.transform.position, quaternion.identity);
            StartCoroutine(SpawnSpikes(1, false));
        } else if (pos1 == 2) {
            Instantiate(GroundParticlesTelegraph, sPos2.transform.position, quaternion.identity);
            StartCoroutine(SpawnSpikes(2, false));
        }

        if (pos1 != 0) { // because pos1 = 0 spawns 2 spikes on the same side
            if (pos2 == 3) {
                Instantiate(GroundParticlesTelegraph, sPos3.transform.position, quaternion.identity);
                StartCoroutine(SpawnSpikes(3, false));
            } else if (pos2 == 4) {
                Instantiate(GroundParticlesTelegraph, sPos4.transform.position, quaternion.identity);
                StartCoroutine(SpawnSpikes(4, false));
            }
        }
        
    }

    IEnumerator SpawnSpikes(int pos, bool right) {
        yield return new WaitForSeconds(1.5f);

        Instantiate(SpikesBase, this.transform.position, quaternion.identity);
        
        switch (pos) {
            case 0:
                if (right) {
                    Instantiate(Spike1, sPos3.transform.position, quaternion.identity);
                    Instantiate(Spike3, sPos4.transform.position, quaternion.identity);                    
                } else {
                    Instantiate(Spike1, sPos1.transform.position, quaternion.identity);
                    Instantiate(Spike3, sPos2.transform.position, quaternion.identity);
                }

                break;
            case 1: // pos 1
                int spike1 = Random.Range(1, 4); // random between 1 and 3
                if (spike1 == 1) {
                    Instantiate(Spike1, sPos1.transform.position, quaternion.identity);
                } else if (spike1 == 2) {
                    Instantiate(Spike2, sPos1.transform.position, quaternion.identity);
                } else if (spike1 == 3) {
                    Instantiate(Spike3, sPos1.transform.position, quaternion.identity);
                }
                break;
            case 2: // pos 2
                int spike2 = Random.Range(1, 4); // random between 1 and 3
                if (spike2 == 1) {
                    Instantiate(Spike1, sPos2.transform.position, quaternion.identity);
                } else if (spike2 == 2) {
                    Instantiate(Spike2, sPos2.transform.position, quaternion.identity);
                } else if (spike2 == 3) {
                    Instantiate(Spike3, sPos2.transform.position, quaternion.identity);
                }
                break;
            case 3: // pos 3
                int spike3 = Random.Range(1, 4); // random between 1 and 3
                if (spike3 == 1) {
                    Instantiate(Spike1, sPos3.transform.position, quaternion.identity);
                } else if (spike3 == 2) {
                    Instantiate(Spike2, sPos3.transform.position, quaternion.identity);
                } else if (spike3 == 3) {
                    Instantiate(Spike3, sPos3.transform.position, quaternion.identity);
                }
                break;
            case 4: // pos 4
                int spike4 = Random.Range(1, 4); // random between 1 and 3
                if (spike4 == 1) {
                    Instantiate(Spike1, sPos4.transform.position, quaternion.identity);
                } else if (spike4 == 2) {
                    Instantiate(Spike2, sPos4.transform.position, quaternion.identity);
                } else if (spike4 == 3) {
                    Instantiate(Spike3, sPos4.transform.position, quaternion.identity);
                }
                break;
            default:
                break;
        }
        
    }
    
    void EnableAttackInSeconds(float seconds) {
        Invoke("EnableAttack", seconds);
    }

    public void EnableAttack() {
        canAttack = true;
    }

    public void EnableAttackSoon() {
        Invoke("EnableAttack", 2f);
    }

    public void PlayHeadPoppin() {
        animator.Play("head_poppin");
    }

    public void PlayReturnToHead() {
        animator.Play("return_head");
    } 

    public void StartBattle(){
        print("start battle!");
        if (awake == false) {
            battleTrigger.SetActive(false);
            awake = true;
            animator.SetTrigger("wake");
            UnfreezeRigidbody();
            camInfluence.SetActive(true);
        }
    }

    public void EnableForceFieldColliders(){
        forceFieldArea.SetActive(true);
    }

    private void HandleEndOfBattle() {
        Layer2At1.SetActive(false);
        Layer2At0.SetActive(true);
        if(PlayerState.Instance.Dead){
            animator.Play("skelly_sleepy");
        }
        if (damage.health <= 0) {
            print("END OF BATTLE");
            forceFieldArea.SetActive(false);
            DisableForceField();
            fmodZeroTrigger.SetActive(true);
            ParticlesAndKilledSFX.SetActive(true);
            camInfluence.SetActive(false);
            fog.SetActive(false);
            damage.Destroy();
            boss1.initialValue = true;
        }
    }
    
    void UnfreezeRigidbody(){
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void EnableDamage(){
        hit.canTakeHit = true;
        damage.canTakeDamage = true;
    }

    public void InstantiateBurstParticles(){
        Instantiate(particlesBurst, this.transform.position, Quaternion.identity);
        Instantiate(smokeBurst, this.transform.position, Quaternion.identity);
    }


    private void BlinkSprite() {
        blink.BlinkSpriteEverySeconds(1f);
    }


    public void PlaySFXPreIntro(){
        FMODUnity.RuntimeManager.PlayOneShot(SkelyPreIntro, transform.position);      
    }

    public void PlaySFXIntro(){
        FMODUnity.RuntimeManager.PlayOneShot(SkelyIntro, transform.position);      
    }

    public void PlaySFXHeadThrow(){
        FMODUnity.RuntimeManager.PlayOneShot(SkelyHeadThrow, transform.position);      
    }

    public void PlaySFXSpikesAntecipation(){
        FMODUnity.RuntimeManager.PlayOneShot(SkelySpikesAntecipation, transform.position);      
    }

    public void PlaySFXSpikes(){
        FMODUnity.RuntimeManager.PlayOneShot(SkelySpikes, transform.position);      
    }
    public void PlaySFXDeath(){
        FMODUnity.RuntimeManager.PlayOneShot(SkelyDeath, transform.position);      
    }


}
