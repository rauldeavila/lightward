using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class Gate : MonoBehaviour 
{
    public bool twoTiles;
    public bool threeTiles;
    public bool lockBackAfterCooldown = false;
    public float cooldown; // time to lock again after unlocking, if SO is null.
    public BoolValue scriptableObject;
    public bool openIfScriptableIsFalse = false;
    public bool checkScriptableOnlyOnce = false;
    public GameObject particlesPos;
    public GameObject closingParticles;
    private bool _flag = false;

    public bool IgnoreUpdateUntilClosed = false;

    private Animator _animator;

    void Awake(){
        _animator = GetComponent<Animator>();
        if(IgnoreUpdateUntilClosed)
        {
            ForceUnlockGate();
        }
    }

    void Update(){        
        if(IgnoreUpdateUntilClosed == false)
        {
            if(scriptableObject != null && _flag == false){
                if(scriptableObject.runTimeValue == true && !(_animator.GetCurrentAnimatorStateInfo(0).IsName("open") || _animator.GetCurrentAnimatorStateInfo(0).IsName("opening"))){
                    OpenGate();
                    if(checkScriptableOnlyOnce){
                        _flag = true;
                    }
                } else if(openIfScriptableIsFalse && scriptableObject.runTimeValue == false &&  !(_animator.GetCurrentAnimatorStateInfo(0).IsName("open") || _animator.GetCurrentAnimatorStateInfo(0).IsName("opening"))){
                    OpenGate();
                    if(checkScriptableOnlyOnce){
                        _flag = true;
                    }
                }
            }
        }
    }

    public void LockGate(){
        if( !(_animator.GetCurrentAnimatorStateInfo(0).IsName("locking") || _animator.GetCurrentAnimatorStateInfo(0).IsName("locked"))){
            _animator.Play("locking");
            IgnoreUpdateUntilClosed = false;
        }
    }

    public void SetScriptableObjectToTrue(){
        scriptableObject.runTimeValue = true;
    }

    public void ForceUnlockGate()
    {
        OpenGate();
    }

    public void UnlockGate(){
        if(scriptableObject != null){
            if(scriptableObject.runTimeValue == true && !(_animator.GetCurrentAnimatorStateInfo(0).IsName("open") || _animator.GetCurrentAnimatorStateInfo(0).IsName("opening"))){
                OpenGate();
            }
        } else {
            if(!(_animator.GetCurrentAnimatorStateInfo(0).IsName("open") || _animator.GetCurrentAnimatorStateInfo(0).IsName("opening"))){
                OpenGate();
            }
        }
        if((scriptableObject == null || scriptableObject.runTimeValue == false) && lockBackAfterCooldown){
            StartCoroutine(LockBack());
        }
    }

    IEnumerator LockBack(){
        yield return new WaitForSeconds(cooldown);
        LockGate();
    }


    public void PlayClosingParticles(){
        Instantiate(closingParticles, particlesPos.transform.position, Quaternion.identity);
    }

    void OpenGate()
    {
        _animator.Play("opening");
        FMODUnity.RuntimeManager.PlayOneShot("event:/game/00_game/gate_opening", transform.position);
    }



}
