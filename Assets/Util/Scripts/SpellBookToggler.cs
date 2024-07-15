using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBookToggler : MonoBehaviour {

    // public bool bookIsShowing = false;

    // private GameManager gameManager;

    public Animator animator; // book's animator

    // void Awake(){
    //     gameManager = FindObjectOfType<GameManager>();
    // }
    // void Update(){
    //     if(gameManager.State.InventoryOpened == false && bookIsShowing){
    //         animator.Play("book_hidden");
    //     }
    // }

    public void ShowBook(){
        animator.ResetTrigger("hide_book");
        animator.SetTrigger("show_book");
        // bookIsShowing = true;
    }

    public void HideBook(){
        // bookIsShowing = false;
        if(animator != null){
            animator.ResetTrigger("show_book");
            animator.SetTrigger("hide_book");
        }
    }


}
