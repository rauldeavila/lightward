using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[RequireComponent(typeof(TakeHit))]
public class TakeDamage : MonoBehaviour {
    

    [Header("Basic Settings")]
    public bool doNothingWhenKilled = false;
    public bool isEnemy;
    [ShowIf("isEnemy")]
    public GameObject EnemyObject;
    [ShowIf("isEnemy")]
    public bool isBat;
    [ShowIf("isEnemy")]
    public bool isSkeleton;
    public int magicDropAmountx10;
    public int health = 1;
    public bool specificKilledParticlesPosition = false;
    public GameObject SpecificKilledParticlesPositionTransform;

    [Header("Sound")]
    public bool hasSound = false;
    [FMODUnity.EventRef]
    public string DestroyingSound = "";


    [Header("Damage / Death settings")]
    public bool setScriptableObjectWhenDestroyed;
    [ShowIf("setScriptableObjectWhenDestroyed")]
    public BoolValue scriptableObjectToSetTrue;
    public bool disableGameObjectWhenKilled;
    [ShowIf("disableGameObjectWhenKilled")]
    public GameObject objectToDisable;
    public bool enableGameObjectWhenKilled;
    [ShowIf("enableGameObjectWhenKilled")]
    public GameObject objectToEnable;
    public bool DontDestroyHasDeathAnimation;
    public bool unlocksGateWhenKilled = false;
    public bool takeDamageWhileDazed = false;

    [Header("Particles")]
    public bool hasParticles = false;
    public bool hasFinalParticles = false;
    public GameObject hitParticles;
    public GameObject finalParticles;
    public GameObject magicParticles;
    

    [HideInInspector]
    public bool canTakeDamage = true;
    [HideInInspector]
    public bool damageFromRight = false;
    private EnemyBaseScript enemy;
    private Animator animator;
    private PlayerController player;

    private void Awake() {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
        if(EnemyObject != null)
            enemy = EnemyObject.GetComponent<EnemyBaseScript>();
    }

    void Update(){
        if(isEnemy){
            this.transform.position = EnemyObject.transform.position;
        }
    }


    private void OnTriggerEnter2D(Collider2D collider) {
        if(canTakeDamage){
            if(collider.CompareTag("Player")){
                if(health > player.GetComponent<PlayerCombat>().AttackPowerValue){
                    if(isEnemy){
                        if(enemy != null){
                            if(!takeDamageWhileDazed){
                                if(enemy.dazed){
                                    return;
                                } else{
                                    if(hasParticles){
                                        Instantiate(hitParticles, this.transform.position, Quaternion.identity);
                                    }
                                    enemy.DazeEnemy();
                                }
                            } else{
                                if(hasParticles){
                                    Instantiate(hitParticles, this.transform.position, Quaternion.identity);
                                }
                                enemy.DazeEnemy();
                            }

                        }
                        if(isBat){
                            EnemyObject.GetComponent<Bat>().TriggerChasing();
                        }

                        if (isSkeleton) {
                            EnemyObject.GetComponent<Skeleton>().WakeUp();
                        }
                    }
                    health -= player.GetComponent<PlayerCombat>().AttackPowerValue;
                } else { // kill
                    health -= player.GetComponent<PlayerCombat>().AttackPowerValue;

                    if(!doNothingWhenKilled){
                        if(disableGameObjectWhenKilled){
                            objectToDisable.SetActive(false);
                        }
                        if(enableGameObjectWhenKilled){
                            objectToEnable.SetActive(true);
                        }
                        if(DontDestroyHasDeathAnimation){
                            PlayDeathAnimation();
                        } else{
                            Destroy();
                        }
                    }
                }
            } else if(collider.CompareTag("Fireball")){
                if(health > PlayerStats.Instance.FireballPower){
                    if(isEnemy){
                        if(enemy != null){
                            if(!takeDamageWhileDazed){
                                if(enemy.dazed){
                                    return;
                                } else{
                                    if(hasParticles){
                                        Instantiate(hitParticles, this.transform.position, Quaternion.identity);
                                    }
                                    enemy.DazeEnemy();
                                }
                            } else{
                                if(hasParticles){
                                    Instantiate(hitParticles, this.transform.position, Quaternion.identity);
                                }
                                enemy.DazeEnemy();
                            }

                        }
                    }
                    health -= PlayerStats.Instance.FireballPower;
                } else { // kill
                    health -= PlayerStats.Instance.FireballPower;
                    if(!doNothingWhenKilled){
                        if(DontDestroyHasDeathAnimation){
                            PlayDeathAnimation();
                        } else{
                            Destroy();
                        }
                    }
                }
            } else if(collider.CompareTag("Projectile")){
                if(collider.gameObject.GetComponent<EnemyFireball>() != null){
                    if(collider.gameObject.GetComponent<EnemyFireball>().HitByPlayer){
                        if(health > player.GetComponent<PlayerCombat>().AttackPowerValue){
                            if(isEnemy){
                                if(enemy != null){
                                    if(!takeDamageWhileDazed){
                                        if(enemy.dazed){
                                            return;
                                        } else{
                                            if(hasParticles){
                                                Instantiate(hitParticles, this.transform.position, Quaternion.identity);
                                            }
                                            enemy.DazeEnemy();
                                        }
                                    } else{
                                        if(hasParticles){
                                            Instantiate(hitParticles, this.transform.position, Quaternion.identity);
                                        }
                                        enemy.DazeEnemy();
                                    }

                                }
                                if(isBat){
                                    EnemyObject.GetComponent<Bat>().TriggerChasing();
                                }

                                if (isSkeleton) {
                                    EnemyObject.GetComponent<Skeleton>().WakeUp();
                                }
                            }
                            health -= player.GetComponent<PlayerCombat>().AttackPowerValue;
                        } else { // kill
                            health -= player.GetComponent<PlayerCombat>().AttackPowerValue;

                            if(!doNothingWhenKilled){
                                if(disableGameObjectWhenKilled){
                                    objectToDisable.SetActive(false);
                                }
                                if(enableGameObjectWhenKilled){
                                    objectToEnable.SetActive(true);
                                }
                                if(DontDestroyHasDeathAnimation){
                                    PlayDeathAnimation();
                                } else{
                                    Destroy();
                                }
                            }
                        }

                    }
                }

            } 
        }
    }


    public void Destroy(){
        if(hasFinalParticles){
            if(specificKilledParticlesPosition){
                Instantiate(finalParticles, SpecificKilledParticlesPositionTransform.transform.position, Quaternion.identity);
            } else{
                Instantiate(finalParticles, this.transform.position, Quaternion.identity);
            }
        } else if(hasParticles){
            Instantiate(hitParticles, this.transform.position, Quaternion.identity);
        }

        if(magicDropAmountx10 > 0){
            for(int i = 0; i <= magicDropAmountx10; i++){
                Instantiate(magicParticles, this.transform.position, Quaternion.identity); }
        }

        if(hasSound && DestroyingSound != null){
            FMODUnity.RuntimeManager.PlayOneShot(DestroyingSound, transform.position);
        }

        if(setScriptableObjectWhenDestroyed){
            if(scriptableObjectToSetTrue != null){
                scriptableObjectToSetTrue.initialValue = true;
            }
        }

	    this.gameObject.SetActive(false);
	    if(EnemyObject != null){
		    EnemyObject.SetActive(false);
	    }
    }

    private void PlayDeathAnimation(){
        if(hasFinalParticles){
            Instantiate(finalParticles, this.transform.position, Quaternion.identity);
        } else if(hasParticles){
            Instantiate(hitParticles, this.transform.position, Quaternion.identity);
        }
        if(magicDropAmountx10 > 0){
            for(int i = 0; i <= magicDropAmountx10; i++){
                Instantiate(magicParticles, this.transform.position, Quaternion.identity);
            }
        }

        if(hasSound && DestroyingSound != null){
            FMODUnity.RuntimeManager.PlayOneShot(DestroyingSound, transform.position);
        }

        if(unlocksGateWhenKilled){
            GetComponent<UnlocksGate>().UnlockGatesOnGateHolder();
        }

        if(isEnemy){
            enemy.KillEnemy();
        }

    }

    public int GetHealth(){
        return health;
    }


}
