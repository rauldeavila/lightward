using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpellBook : MonoBehaviour {

    void Update() {
       if(GameState.Instance.InventoryOpened == true){
            GetComponent<Animator>().SetBool("inventory_opened", true);
        } else {
            GetComponent<Animator>().SetBool("inventory_opened", false);
        }
    }

}
