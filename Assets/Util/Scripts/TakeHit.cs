using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;


//  Class that's used across everything that takes hit in the game
// Be it an enemy, money rocks, elevators, hidden passages or breakable objects.
public class TakeHit : MonoBehaviour {
    public UnityEvent WizHitThisObject;
    public UnityEvent EventWhenOtherTagHitObject;
    public UnityEvent EventWhenOtherTagHitObject2;

    [Header("INITIAL SETUP")]
    public bool ImpactPrefab = true;
    public bool KnocksPlayerBack; 
    public bool ShakeScreen;
    [ShowIf("ShakeScreen")]
    public bool SoftShake = true;
    [ShowIf("ShakeScreen")]
    public bool MediumShake = false;
    [ShowIf("ShakeScreen")]
    public bool HardShake = false;
    public bool FlashSprite;
    public bool PlaySFX;
    [ShowIf("PlaySFX")]
    public string SFXPath;
    public bool DoesPlayerKnocksItBack = false;
    [ShowIf("DoesPlayerKnocksItBack")]
    public float KnockBackStrength = 2f;

    private float _delayToListenAgain = 0.3f;
    private bool _canListen = true;

    [HideInInspector] public bool canTakeHit = true;

    [Header("Specific Hit Stuff")]
    public bool ground;
    public bool lever;
    [ShowIf("lever")]
    public bool stopLeverShineWhenHit;
    [ShowIf("lever")]
    public Sprite leverOn;
    [ShowIf("lever")]
    public Sprite leverOff;

    public bool matOnEnemy = false;
    public EnemyBaseScript enemy;

    private SpriteFlashTool flashTool;
    private SpriteRenderer sprite;
    private bool canKnockBackWiz = true;

    public string TagToListen = "Laser";
    public string TagToListen2 = "Ground";



    private void Awake(){

        if(!matOnEnemy){
            flashTool = GetComponentInChildren<SpriteFlashTool>();
        } else {
        	if(enemy != null){
	        	flashTool = enemy.GetComponentInChildren<SpriteFlashTool>();
        	}
        }
        if(lever){
            sprite = GetComponent<SpriteRenderer>();
        }
    }

    private void SetKnockBackWizToTrue(){
        canKnockBackWiz = true;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(canTakeHit){
            if(TagToListen != "")
            {
                if(collider.CompareTag(TagToListen))
                {
                    if(EventWhenOtherTagHitObject != null)
                    {
                        EventWhenOtherTagHitObject.Invoke();
                    }
                }
            }
            if(TagToListen2 != "")
            {
                if(collider.CompareTag(TagToListen2))
                {
                    if(EventWhenOtherTagHitObject2 != null)
                    {
                        EventWhenOtherTagHitObject2.Invoke();
                    }
                }
            }
            if(collider.CompareTag("Player")){
                if(WizHitThisObject != null){
                    WizHitThisObject.Invoke();
                }
                if(canKnockBackWiz){
                    canKnockBackWiz = false;
                    if(ImpactPrefab){
                        if(PlayerController.Instance.AnimatorIsPlaying("attack_up")){
                            WizAttackImpact.Instance.CheckImpactPositionAndGenerate(3);
                        } else if(PlayerController.Instance.AnimatorIsPlaying("attack_down")) {
                            WizAttackImpact.Instance.CheckImpactPositionAndGenerate(2);
                        } else { // regular attack
                            WizAttackImpact.Instance.CheckImpactPositionAndGenerate(1);
                        }
                    }
                    Invoke("SetKnockBackWizToTrue", 0.5f);
                }

                if(PlaySFX){
                    SFXController.Instance.Play(SFXPath);
                }
                
                if(lever){
                    Lever theLever = GetComponent<Lever>();
                    if(theLever.Elevator != null && theLever.Outside == false){
                        if(PlayerState.Instance.OnElevator){
                            theLever.Toggle();
                            if(sprite.sprite == leverOn){
                                sprite.sprite = leverOff;
                            } else{
                                sprite.sprite = leverOn;
                            }
                            if(stopLeverShineWhenHit){
                                this.GetComponent<Animator>().SetBool("off", true);
                            }

                        }
                    } else {
                        theLever.Toggle();
                        if(sprite.sprite == leverOn){
                            sprite.sprite = leverOff;
                        } else{
                            sprite.sprite = leverOn;
                        }
                        if(stopLeverShineWhenHit){
                            this.GetComponent<Animator>().SetBool("off", true);
                        }
                    }
                }
                
                if(ShakeScreen){
                    if(SoftShake){
                        CameraSystem.Instance.ShakeCamera(0);
                    } else if(MediumShake){
                        CameraSystem.Instance.ShakeCamera(1);
                    } else if(HardShake){
                        CameraSystem.Instance.ShakeCamera(2);
                    }
                }

                if(KnocksPlayerBack){
                    KnockPlayerBack();
                }
                if(DoesPlayerKnocksItBack){
                    KnockBack();
                }
                if(FlashSprite){
                    if(flashTool!=null)
                        flashTool.Flash();

                }
            }
        }
    }

    void ReenableListening()
    {
        _canListen = true;
    }
    
    public void KnockPlayerBack(){
        if(!ground){
            if(PlayerController.Instance.AnimatorIsPlaying("attack_down")){
                if(_canListen)
                {
                    _canListen = false;
                    Jump.Instance.Pogo();
                    Invoke("ReenableListening", _delayToListenAgain);
                }
            } else if(PlayerController.Instance.AnimatorIsPlaying("attack")) {
                Vector2 difference = new Vector2(this.transform.position.x, this.transform.position.y) - new Vector2(PlayerController.Instance.transform.position.x, PlayerController.Instance.transform.position.y);
                if (GetComponent<Rigidbody2D>() == null) {
                    return;
                }
                GetComponent<Rigidbody2D>().AddForce(-difference.normalized * 0.25f, ForceMode2D.Impulse);
            }
        } else { // if ground
            if(PlayerController.Instance.AnimatorIsPlaying("attack_down")){ 
                PlayerState.Instance.BeingKnockedBack = true;
                PlayerController.Instance.KnockWizBack(0f, 5f);
            }
            
        }

    }
    
    public void KnockBack(){
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Vector2 difference = new Vector2(this.transform.position.x, this.transform.position.y) - new Vector2(PlayerController.Instance.transform.position.x, PlayerController.Instance.transform.position.y);
        this.GetComponent<Rigidbody2D>().AddForce(difference.normalized * KnockBackStrength, ForceMode2D.Impulse);
    }

}
