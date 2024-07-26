using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class HandleInput : MonoBehaviour
{

    public string ActionName = "";
    public string Message = "";
    public string _promptType = "Save";

    public GameObject EnterObject;

    public string SceneName = "";
    public string FacingDirection = "right";
    public float PosX = 0f;
    public float PosY = 0f;
    public bool OnTrigger = false; // Triggered by Event from OnTrigger
    private bool _alreadySaved = false;

    public GameObject ZoomTriggerForSavingPrompt;

    public GameObject SaveMenuPanel;

    public UnityEvent UnlockEvent;
    public UnityEvent DialogueEvent;

    private bool _showingPrompt = false;

    private bool _heroSit = false;

    public void SetOnTrigger(bool _status){
        OnTrigger = _status;
    }

    void FixedUpdate()
    {
        if(OnTrigger)
        {
            SaveMenuPrompt();
            SavePrompt();
            EnterPrompt();
            // UnlockPrompt();
            // SpeakPrompt();
        } 
        HandleUpFromCampfire();
    }

    void SaveMenuPrompt()
    {
        if(_promptType == "SaveMenu" && GameState.Instance.Overworld)
        {
            if(!_showingPrompt && PlayerController.Instance.AnimatorIsPlaying("sit"))
            {
                // print("Menu Prompt displaying now!");
                DisplayButtonOnScreen.Instance.ShowButtonPrompt(ActionName, Message);
                _showingPrompt = true;
            }
            if(Inputs.Instance.HoldingJump)
            {
                // print("holding jump");
                DisplayButtonOnScreen.Instance.HideButtonPrompt();
                _showingPrompt = false;
            }
            if(_showingPrompt && !PlayerController.Instance.AnimatorIsPlaying("sit"))
            {
                _showingPrompt = false;
                SaveMenuPanel.SetActive(false);
                DisplayButtonOnScreen.Instance.HideButtonPrompt();
            }
        }
        else
        {
            if(_showingPrompt)
            {
                _showingPrompt = false;
                DisplayButtonOnScreen.Instance.HideButtonPrompt();
            }
        }
    }

    void SavePrompt()
    {
        if(_promptType == "Save")
        {
            if(PlayerController.Instance.AnimatorIsPlaying("idle"))
            {
                if(DisplayButtonOnScreen.Instance.ShowingPrompt == false)
                {
                    // GetComponent<InputPrompt>().ShowInputPrompt();
                    DisplayButtonOnScreen.Instance.ShowButtonPrompt("MoveDown", "Save");
                    _showingPrompt = true;
                }
                if(Inputs.Instance.HoldingDownArrow && (!PlayerController.Instance.AnimatorIsPlaying("sit") || !PlayerController.Instance.AnimatorIsPlaying("sitting")))
                {
                    // print("SAVING!");
                    Inputs.Instance.HoldingDownArrow = false;
                    Move.Instance.StopPlayer();
                    DisplayButtonOnScreen.Instance.HideButtonPrompt();
                    _showingPrompt = false;
                    if(PlayerController.Instance.transform.position.x < this.transform.position.x){
                        if(PlayerController.Instance.State.FacingRight == false)
                        {
                            Move.Instance.Flip();
                        }
                    } 
                    else if(PlayerController.Instance.transform.position.x > this.transform.position.x)
                    {
                        if(PlayerController.Instance.State.FacingRight == true)
                        {
                            Move.Instance.Flip();
                        }
                    }

                    PlayerController.Instance.Animator.Play("sitting");
                    ZoomTriggerForSavingPrompt.SetActive(true);
                    ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("hero_health_yellow", 0);
                    ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("hero_health", ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("hero_health").maxValue);
                    ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("hero_magic", ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("hero_magic").maxValue);
                    Magic.Instance.UpdateMagic();
                    SaveSystem.Instance.SaveGame();
                    _alreadySaved = true;

                    Invoke("OpenSaveMenuPanel", 0.2f);
                }
            }
        }
    }

    private void OpenSaveMenuPanel()
    {
        // print("Save menu panel");
        SaveMenuPanel.SetActive(true);
    }

    void EnterPrompt()
    {
        if(_promptType == "Enter")
        {
            if(GameState.Instance.InsideBuilding)
            {
                Message = "Exit";
            }
            else
            {
                Message = "Enter";
            }
            if(Inputs.Instance.HoldingUpArrow && (PlayerController.Instance.AnimatorIsPlaying("idle") || PlayerController.Instance.AnimatorIsPlaying("idle_landing") || PlayerController.Instance.AnimatorIsPlaying("run")))
            {

                Inputs.Instance.HoldingUpArrow = false;
                Move.Instance.StopPlayer();
                DisplayButtonOnScreen.Instance.HideButtonPrompt();
                _showingPrompt = false;
                if(EnterObject != null)
                {
                    if(EnterObject.activeInHierarchy)
                    {
                        EnterObject.SetActive(false);
                    }
                    else
                    {
                        EnterObject.SetActive(true);
                    }
                }
                else
                {
                    Debug.Log("Enter Object missing!");
                }
            }
        }
    }

    void UnlockPrompt()
    {
        if(_promptType == "Unlock")
        {
            if(Inputs.Instance.HoldingUpArrow && (PlayerController.Instance.AnimatorIsPlaying("idle") || PlayerController.Instance.AnimatorIsPlaying("idle_landing") || PlayerController.Instance.AnimatorIsPlaying("run")))
            {
                Inputs.Instance.HoldingUpArrow = false;
                Move.Instance.StopPlayer();
                DisplayButtonOnScreen.Instance.HideButtonPrompt();
                _showingPrompt = false;
                if(UnlockEvent != null)
                {
                    UnlockEvent.Invoke();
                }

            }
          
        }
    }

    void SpeakPrompt()
    {
        if(_promptType == "Speak")
        {
            if(Inputs.Instance.HoldingDownArrow && (PlayerController.Instance.AnimatorIsPlaying("idle") || PlayerController.Instance.AnimatorIsPlaying("idle_landing") || PlayerController.Instance.AnimatorIsPlaying("run")))
            {
                Inputs.Instance.HoldingDownArrow = false;
                Move.Instance.StopPlayer();
                DisplayButtonOnScreen.Instance.HideButtonPrompt();
                _showingPrompt = false;
                if(DialogueEvent != null)
                {
                    DialogueEvent.Invoke();
                }
                GetComponent<DialogueTrigger>().TriggerDialogue();
            }
        }
    }

    void HandleUpFromCampfire()
    {
        if(GameState.Instance.Overworld) // exited menu
        {
            if(_alreadySaved && Inputs.Instance.HoldingUpArrow && PlayerController.Instance.AnimatorIsPlaying("sit"))
            {
                _alreadySaved = false;
                // print("Getting up from saving.");
                ZoomTriggerForSavingPrompt.SetActive(false);
            }
        }
    }

}
