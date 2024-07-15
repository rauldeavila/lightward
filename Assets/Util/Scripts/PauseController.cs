using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {

    public static PauseController Instance;

    public bool ConsoleIsOpened = false;

    private PlayerController controller;
    private GameManager gameManager;
    private bool pressedStart = false;
    private bool pressedInventory = false;
    private string bugReport = "https://forms.gle/wEq1eNSCoLjQomh58";

    FMOD.Studio.Bus Music;

    public bool resumed = false; // player landing sets this to false

    public GameObject pausePanel, settingsPanel, statsPanel;
    public GameObject settingsFirstButton,statsFirstButton, inventoryFirstButton;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        controller = FindObjectOfType<PlayerController>();
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/music");

        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }

    private void Start() {
        if(SceneManager.GetActiveScene().name != "intro"){
            controller.Controls.Game.Start.performed += ctx => StartPressed();

        }
    }

    private void StartPressed(){
        if(!ConsoleIsOpened){
            pressedStart = true;
        }
    }



    void Update() {
    

        if (pressedStart){
            pressedStart = false;
            if(gameManager.State.Paused){
                if(gameManager.State.SettingsOpened){
                    CloseSettings();
                    Resume();
                } if(gameManager.State.StatsOpened){
                    CloseStats();
                    Resume();
                } 
            } else {
                if(!gameManager.State.OnCutscene){
                    Pause();
                }
            }
        }
    }

    // panel inside options
    public void OpenSettings(){
        settingsPanel.SetActive(true);
        gameManager.State.SettingsOpened = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsFirstButton);
    }

    // panel inside options
    public void CloseSettings(){
        settingsPanel.SetActive(false);
        gameManager.State.SettingsOpened = false;
    }


    public void OpenStats(){
        statsPanel.SetActive(true);
        gameManager.State.StatsOpened = true;
        EventSystem.current.SetSelectedGameObject(statsFirstButton);
    }

    public void CloseStats(){
        statsPanel.SetActive(false);
        gameManager.State.StatsOpened = false;
    }


    public void Pause(){
        Inputs.Instance.ResetAllButtonPress();
        DisableAllPanelsAndStates();
        gameManager.State.Paused = true;
        gameManager.MuteSFXBus();
        pausePanel.SetActive(true);

        Time.timeScale = 0f;

        OpenSettings();
    }

    private void SetResumedToFalse(){
        resumed = false;
    }

    public void Resume(){
        Inputs.Instance.ResetAllButtonPress();
        TimeManager.Instance.BeginTimer();
        Time.timeScale = 1f;
        resumed = true; // set false on Player LAnding to prevent wiz from spawning particles when resuming game
        Invoke("SetResumedToFalse", 0.5f);
        gameManager.UnnuteSFXBus();
        EventSystem.current.SetSelectedGameObject(null);
        DisableAllPanelsAndStates();
        pausePanel.SetActive(false);
        gameManager.State.Resuming = true;
        Invoke("ResumingReset", 0.2f);
        gameManager.State.Paused = false;
        gameManager.State.MapOpened = false;
        gameManager.State.InventoryOpened = false;

    }

    public void ReturnToMainMenu(){
        Time.timeScale = 1f;
        Music.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame(){
        Application.Quit();
    }

    private void ResumingReset(){
        gameManager.State.Resuming = false;
    }

    public void OpenBugReport(){
        Application.OpenURL(bugReport);
    }

    public void DisableAllPanelsAndStates(){
        // gameManager.State.InventoryOpened = false;
        // gameManager.State.MapOpened = false;
        // gameManager.State.MinimapOpened = false;
        // gameManager.State.SettingsOpened = false;
        // gameManager.State.StatsOpened = false;

        // if(settingsPanel){
        //     settingsPanel.SetActive(false);
        // }
        // if(statsPanel){
        //     statsPanel.SetActive(false);
        // }
    }


    public void ConsoleOn(){
        GameState.Instance.ConsoleOn = true;
        ConsoleIsOpened = true;
    }

    public void ConsoleOff(){
        GameState.Instance.ConsoleOn = false;
        DisableAllPanelsAndStates();
        ConsoleIsOpened = false;
    }

}
