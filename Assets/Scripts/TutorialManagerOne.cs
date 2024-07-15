using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManagerOne : MonoBehaviour {

    public GameObject TutWakeUp;
    public GameObject TutAttack;
    public GameObject TutJump;

    public static TutorialManagerOne Instance;
    private bool _shownWakeUpTut = false;

    private Coroutine jumpCoroutine;
    private Coroutine wakeupCoroutine;
    private Coroutine attackCoroutine;
    private bool breakFromJump = false;

    private bool _wakeupFlag = false;
    private bool _shownWakeTut = false;
    private bool _attackFlag = false;
    private bool _shownAttackTut = false;
    private bool _jumpFlag = false;
    private bool _shownJumpTut = false;


    void Awake() {
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        }

        wakeupCoroutine = StartCoroutine(WakeUpCoRoutine());
    }

    IEnumerator WakeUpCoRoutine() {
        yield return new WaitForSeconds(10f);
        TutWakeUp.SetActive(true);
        LogSystem.Instance.AppendLog("tutorial_wake_up: true");
        _shownWakeUpTut = true;
    }

    void LateUpdate(){
        CheckForWakeUp();
    }

    void CheckForWakeUp(){
        if(!PlayerController.Instance.AnimatorIsPlaying("lay")){
            if(TutWakeUp.activeInHierarchy){
                TutWakeUp.SetActive(false);
            } else {
                if(!_wakeupFlag){
                    _wakeupFlag = true;
                    if(wakeupCoroutine != null){
                        StopCoroutine(wakeupCoroutine);
                    }
                    if(!_shownWakeUpTut){
                        LogSystem.Instance.AppendLog("tutorial_wake_up: false");
                    }
                }
            }
        }
    }

    public void ShowAttackTutIn10Sec(){
        attackCoroutine = StartCoroutine(ShowAttackTutCoroutine());
    }

    IEnumerator ShowAttackTutCoroutine(){
        yield return new WaitForSeconds(10f);
        TutAttack.SetActive(true);
        LogSystem.Instance.AppendLog("tutorial_attack: true");
        _shownAttackTut = true;
    }

    public void HideAttackTut(){
        if(TutAttack.activeInHierarchy){
            TutAttack.SetActive(false);
        } else {
        // print("Cancelando invoke do ataque!");   
            if(!_attackFlag){
                _attackFlag = true;
                if(attackCoroutine != null){
                    StopCoroutine(attackCoroutine);
                }
                if(!_shownAttackTut){
                    LogSystem.Instance.AppendLog("tutorial_attack: false");
                }
            }
        }
    }


    public void ShowJumpTutIn10Sec(){
        jumpCoroutine = StartCoroutine(ShowJumpTutCoroutine());
    }

    IEnumerator ShowJumpTutCoroutine(){
        yield return new WaitForSeconds(10f);
        if(breakFromJump){
            // print("break from jump");
        } else {
            TutJump.SetActive(true);
            LogSystem.Instance.AppendLog("tutorial_jump: true");
            _shownJumpTut = true;
        }
    }

    public void HideJumpTut(){
        if(TutJump.activeInHierarchy){
            TutJump.SetActive(false);
            if(jumpCoroutine != null){
                StopCoroutine(jumpCoroutine);
            }
        } else {
            breakFromJump = true;
            if(!_jumpFlag){
                _jumpFlag = true;
                if(jumpCoroutine != null){
                    StopCoroutine(jumpCoroutine);
                }
                if(!_shownJumpTut){
                    LogSystem.Instance.AppendLog("tutorial_jump: false");
                }
            }
        }
    }

    public void OnDisable(){
        // Tutorial keys to check
        string[] tutorialKeys = new string[] { "tutorial_attack", "tutorial_jump", "tutorial_dodge" };
        bool skippedTuto = false;

        // Check each key and set to false if not found
        foreach (string key in tutorialKeys) {
            if (!LogSystem.Instance.CheckLogForEntry(key)) {
                LogSystem.Instance.AppendLog($"{key}: false");
                skippedTuto = true;
            }
        }
        if(skippedTuto){
            LogSystem.Instance.AppendLog(">>skipped_tutorial: true");
        }
    }


}