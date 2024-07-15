using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


// Objects that has 2 states of animation and may or may not play particles when changing states
// Animator trigger = Break
[RequireComponent(typeof(TakeHit))]
public class Breakable : MonoBehaviour {

    private SpriteRenderer sr;
    private BoxCollider2D boxCollider;

    public bool ControlledByScriptableObject;
    [ShowIf("ControlledByScriptableObject")]
    public BoolValue ScriptableObjectToCheck;

    public bool IgnoreSmokesParticles = false;

    public bool changeMaterial;
    [ShowIf("changeMaterial")]
    public Material newMat;

    public bool isChest = false;

    public bool hasMoney;
    [ShowIf("hasMoney")]
    public int CashTotal = 5;
    [ShowIf("hasMoney")]
    public GameObject coinPrefab;
    [ShowIf("hasMoney")]
    public GameObject coinPrefabAlt;


    private float upForceMin = 2f;
    private float upForceMax = 5f;

    public bool disableColliderOnFinalState;
    [ShowIf("disableColliderOnFinalState")]
    public Collider2D colliderToDisable;

    public bool disableChildOnFinalState;
    public bool enableChildOnFinalState;
    public GameObject childToDisable;
    public GameObject childToEnable;
    public bool twoStates = false;
    public bool threeStates = false;
    public bool fourStates = false;
    public int amountOfStates = 0;

    public bool hasParticles;
    public bool hasFinalParticles;

    public bool dontChangeSprites;
    public bool spriteOnChild;
    public Sprite baseState;
    [ShowIf("twoStates")]
    public Sprite state2;
    [ShowIf("threeStates")]
    public Sprite state3;
    [ShowIf("fourStates")]
    public Sprite state4;

     [ShowIf("hasParticles")]
     public GameObject particles;
     [ShowIf("hasParticles")]
     public GameObject particlesPosition;
     [ShowIf("hasParticles")]
     public GameObject specificParticlesOnState1;
     [ShowIf("hasFinalParticles")]
     public GameObject finalParticles;
    

    public int currentState;

    public bool hasSound = false;
    [ShowIf("hasSound")]
    public string SoundEventPath;

    private GameEvents gameEvents;

    private GameObject HitParticlesLeft;
    private GameObject HitParticlesRight;

    private void Awake() {
        if(spriteOnChild){
            sr = GetComponentInChildren<SpriteRenderer>();
        } else {
            sr = GetComponent<SpriteRenderer>();
        }
        boxCollider = GetComponent<BoxCollider2D>();
        gameEvents = FindObjectOfType<GameEvents>();
        LoadResources();
    }

    void LoadResources(){
        HitParticlesLeft = Resources.Load<GameObject>("Particles/HitParticlesLeft");
        HitParticlesRight = Resources.Load<GameObject>("Particles/HitParticlesRight");
        if(particlesPosition == null){
            particlesPosition = this.gameObject;
        }
    }

    private void Start() {
        if(ScriptableObjectToCheck != null){
            string objName = ScriptableObjectToCheck.name;
            ScriptableObjectToCheck = ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>(objName);
        }
        if(fourStates){
            amountOfStates = 4;
        } else if(threeStates){
            amountOfStates = 3;
        } else if(twoStates){
            amountOfStates = 2;
        }

        if(ControlledByScriptableObject && ScriptableObjectToCheck.runTimeValue == true){
            currentState = amountOfStates;
            ChangeSprite();
            CheckIfFinalState();
        } else {
            currentState = 1;
            if(childToDisable!=null){
                childToDisable.SetActive(true);
            }

            if(childToEnable != null){
                childToEnable.SetActive(false);
            }
            
            boxCollider.enabled = true;
        }

    }

    // void Update(){
    //     if(ControlledByScriptableObject){
    //         print("controlled by scriptable: " + ControlledByScriptableObject);
    //         print("scriptable object: " + ScriptableObjectToCheck.name + " - runTimeValue: " + ScriptableObjectToCheck.runTimeValue);
    //     }
    //     if(ControlledByScriptableObject && ScriptableObjectToCheck.runTimeValue == true){
    //         currentState = amountOfStates;
    //         ChangeSprite();
    //         CheckIfFinalState();
    //     }
    // }


    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Player")){
            Hit();
        }
    }

    private void Hit(){
        currentState += 1;
        PlayParticles();
        ChangeSprite();
        CheckIfFinalState();
    }

    private void PlayParticles(){
        if(!IgnoreSmokesParticles){
            if(PlayerState.Instance.FacingRight){
                if(HitParticlesRight) Instantiate(HitParticlesRight, particlesPosition.transform.position, Quaternion.identity);
            } else {
                if(HitParticlesLeft) Instantiate(HitParticlesLeft, particlesPosition.transform.position, Quaternion.identity);
            }
        }
        
        if(currentState <= amountOfStates){
            if(currentState == 2){
                if(specificParticlesOnState1 != null){
                    Instantiate(specificParticlesOnState1, particlesPosition.transform.position, Quaternion.identity);
                }
            }
            int dif = amountOfStates - currentState;
            if(dif > 0 ){

                if(hasParticles){
                    Instantiate(particles, particlesPosition.transform.position , Quaternion.identity);
                }

                if(hasMoney){
                    for(var i = 0; i < CashTotal; i++){
                        if(coinPrefab != null){

                            int rand = Random.Range(1,3);
                            if (rand == 1) {
                                GameObject coinObject = (GameObject)Instantiate(coinPrefab, new Vector2(this.transform.position.x, this.transform.position.y + 0.2f), transform.rotation * Quaternion.Euler (0f, 180f, 0f));
                                coinObject.transform.rotation = Quaternion.Euler(0, i % 2 == 0 ? 0 : 180, 0);
                                coinObject.GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(upForceMin, upForceMax));
                            } else if (rand == 2) {
                                GameObject coinObject = (GameObject)Instantiate(coinPrefabAlt, new Vector2(this.transform.position.x, this.transform.position.y + 0.2f), transform.rotation * Quaternion.Euler (0f, 180f, 0f));
                                coinObject.transform.rotation = Quaternion.Euler(0, i % 2 == 0 ? 0 : 180, 0);
                                coinObject.GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(upForceMin, upForceMax));
                            }

                        }
                    }

                }

            } else{  // final hit

                if(hasMoney){

                    for(var i = 0; i < CashTotal; i++){
                        if(coinPrefab != null){

                            int rand = Random.Range(1,3);
                            if (rand == 1) {
                                GameObject coinObject = (GameObject)Instantiate(coinPrefab, new Vector2(this.transform.position.x, this.transform.position.y + 0.2f), transform.rotation * Quaternion.Euler (0f, 180f, 0f));
                                coinObject.transform.rotation = Quaternion.Euler(0, i % 2 == 0 ? 0 : 180, 0);
                                coinObject.GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(upForceMin, upForceMax));

                            } else if (rand == 2) {
                                GameObject coinObject = (GameObject)Instantiate(coinPrefabAlt, new Vector2(this.transform.position.x, this.transform.position.y + 0.2f), transform.rotation * Quaternion.Euler (0f, 180f, 0f));
                                coinObject.transform.rotation = Quaternion.Euler(0, i % 2 == 0 ? 0 : 180, 0);
                                coinObject.GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(upForceMin, upForceMax));
                            }

                        }
                    }
                }

                if(ControlledByScriptableObject){
                    ScriptableObjectToCheck.runTimeValue = true;
                }

                if(disableChildOnFinalState){
                    if(childToDisable != null){
                        childToDisable.SetActive(false);
                    }
                }
                if(disableColliderOnFinalState){
                    if(colliderToDisable != null){
                        colliderToDisable.enabled = false;
                    }
                }

                if(enableChildOnFinalState){
                    if(childToEnable != null){
                        childToEnable.SetActive(true);
                    }
                }

                if(hasSound){
                    SFXController.Instance.Play(SoundEventPath);
                }
                
                if(hasFinalParticles){
                    Instantiate(finalParticles, particlesPosition.transform.position, Quaternion.identity);
                } else {
                    if(hasParticles){
                        Instantiate(particles, particlesPosition.transform.position , Quaternion.identity);
                    }
                }
            }
        }
    }

    private void ChangeSprite(){
        if(dontChangeSprites == false){
            if(currentState == 2){
                sr.sprite = state2;
            } else if(currentState == 3){
                sr.sprite = state3;
            } else if(currentState == 4){
                sr.sprite = state4;
            }

        }
    }

    private void CheckIfFinalState(){
        if(currentState == amountOfStates){
            if(disableChildOnFinalState){
                childToDisable.SetActive(false);
            }
            if(enableChildOnFinalState && !isChest){
                childToEnable.SetActive(true);
            }
            
            boxCollider.enabled = false;
            if(changeMaterial){
                sr.material = newMat;
            }
        }
    }

}
