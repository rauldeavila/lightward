using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSaveUI : MonoBehaviour {

    private Animator animator;

    private void Awake(){
        animator = GetComponent<Animator>();
    }

    void Update(){
        if(PlayerState.Instance.Sit == false){
            animator.SetTrigger("deactivate");
        }
    }

    public void ActivateAutoSaveUI(){
        animator.SetTrigger("activate");
    }

    public void DeactivateautoSaveUI(){
        animator.SetTrigger("deactivate");
    }


}
