using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Use this script for any locks on the game like doors and gates (unless the boss gates, they have a specific one)

    A. Animator with "unlock" trigger and 3 states
        1. locked
        2. opening
        3. unlocked

    B.  GameObject with sprite  containing only the unlocked state
    C. Scriptable Object to control the state.

    When scene is lodaded, if the SO is already true, this script disables this game object and
    enables the one containing just the unlocked sprite.

    If it's locked, it keeps checking for updates on the SO.

*/
public class UnlockPath : MonoBehaviour {
   
    public BoolValue unlocker_SO;
    public GameObject unlocked_GameObject;
    private Animator animator;

    void Awake(){
        animator = GetComponent<Animator>();
        if(unlocker_SO.initialValue){
            unlocked_GameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    void Update(){
        if(unlocker_SO.initialValue){
            animator.SetTrigger("unlock");
        }
    }



}
