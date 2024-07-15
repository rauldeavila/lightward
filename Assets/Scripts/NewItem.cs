using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewItem : MonoBehaviour
{

    public string ItemToGive = ""; // set from other scripts
    public GameObject KeyHolder;

    public static NewItem Instance;
	private void Awake() {
		
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }

    public void GiveItem(){
        switch(ItemToGive)
        {
            case "key":
                NewKey();
                break;
            default:
                print("no item to give... fix this!");
                break;
        }
    }
    public void NewKey()
    {
        ItemToGive = "";
        SFXController.Instance.Play("event:/game/00_game/pickup_key");
        PlayerState.Instance.SetAllStatesToFalse();
        PlayerState.Instance.Interacting = true;
        InteractionsManager.Instance.Animation = true;
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("npc_maiuiu_key", true);
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<IntValue>("wiz_keys", ScriptableObjectsManager.Instance.GetScriptableObject<IntValue>("wiz_keys").runTimeValue + 1);
        PlayerController.Instance.Animator.Play("new_spell");
        KeyHolder.SetActive(true);
        Invoke("ReturnToGame", 3f);
    }

    void ReturnToGame()
    {
        KeyHolder.SetActive(false);
        PlayerController.Instance.Animator.Play("idle");
        PlayerState.Instance.Interacting = false;
    }

}
