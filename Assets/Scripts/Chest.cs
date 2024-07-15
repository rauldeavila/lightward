using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    public BoolValue ItemTaken;

    public enum ChestState {
        Closed,
        Opened,
        Empty
    }

    public ChestState CurrentState;

    private InteractController _keyPromptChild;
    
    void Awake(){
        _keyPromptChild = gameObject.GetComponentInChildren<InteractController>(true);
    }

    public void ToggleVisibility(){
        _keyPromptChild.gameObject.SetActive(!_keyPromptChild.gameObject.activeSelf);        

    }

    public void OpenChest(){
        CurrentState = ChestState.Opened;
    }




}