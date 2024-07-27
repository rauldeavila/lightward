using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameState : MonoBehaviour {

    public static GameState Instance;

    public UnityEvent OnEnterSafeZone;
    public UnityEvent OnExitSafeZone;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 


        if(PlayerPrefs.GetInt("Rumble") == 1){
            Rumble = true;
        }
        if(PlayerPrefs.GetInt("CRT") == 1){
            CRT = true;
        } else {
            CRT = false;
        }
    }

    void Start()
    {
        Overworld = true;
    }

    void Update(){
        // if(ConsoleOn || Paused || InventoryOpened || MapOpened) {
        //     Overworld = false;
        // } else {
        //     Overworld = true;
        // }
    }

    [SerializeField] private string _currentScene;
    public string CurrentScene { get => _currentScene; set => _currentScene = value; }

    [SerializeField] private string _currentArea;
    public string CurrentArea { get => _currentArea; set => _currentArea = value; }

    [SerializeField] private bool _overworld;
    public bool Overworld {
        get => _overworld;
        set {
            _overworld = value;
            PlayerController.Instance.Animator.SetBool("paused", !_overworld);
        }
    }

    [SerializeField] private bool _darkworld;
    public bool Darkworld { get => _darkworld; set => _darkworld = value; }
    [SerializeField] private bool _atSafeZone;
    public bool AtSafeZone { 
        get => _atSafeZone; 
        set {
            _atSafeZone = value;
            if (_atSafeZone) {
                OnEnterSafeZone?.Invoke();
            } else {
                OnExitSafeZone?.Invoke();
            }
        }
    }



    [SerializeField] private bool _paused;
    public bool Paused { get => _paused; set => _paused = value; }

    [SerializeField] private bool _consoleOn;
    public bool ConsoleOn { get => _consoleOn; set => _consoleOn = value; }

    [SerializeField] private bool _onCutscene;
    public bool OnCutscene { get => _onCutscene; set => _onCutscene = value; }

    [SerializeField] private bool _mapOpened;
    public bool MapOpened { get => _mapOpened; set => _mapOpened = value; }

    [SerializeField] private bool _minimapOpened;
    public bool MinimapOpened { get => _minimapOpened; set => _minimapOpened = value; }

    [SerializeField] private bool _statsOpened;
    public bool StatsOpened { get => _statsOpened; set => _statsOpened = value; }

    [SerializeField] private bool _settingsOpened;
    public bool SettingsOpened { get => _settingsOpened; set => _settingsOpened = value; }

    [SerializeField] private bool _inventoryOpened;
    public bool InventoryOpened { get => _inventoryOpened; set => _inventoryOpened = value; }
    
    [SerializeField] private bool _resuming;
    public bool Resuming { get => _resuming; set => _resuming = value; }

    [SerializeField] private bool _loadingScene;
    public bool LoadingScene { get => _loadingScene; set => _loadingScene = value; }

    [SerializeField] private bool _gameHasEnded;
    public bool GameHasEnded { get => _gameHasEnded; set => _gameHasEnded = value; }

    [SerializeField] private bool _timeStopped;
    public bool TimeStopped { get => _timeStopped; set => _timeStopped = value; }
        
    [SerializeField] private bool _saving;
    public bool Saving { get => _saving; set => _saving = value; }
    
    [SerializeField] private bool _thunder;
    public bool Thunder { get => _thunder; set => _thunder = value; }

    [SerializeField] private bool _darkArea;
    public bool DarkArea { get => _darkArea; set => _darkArea = value; }

    [SerializeField] private bool _crt;
    public bool CRT { get => _crt; set => _crt = value; }
    
    [SerializeField] private bool _rumble;
    public bool Rumble { get => _rumble; set => _rumble = value; }
    [SerializeField] private bool _rebinding;
    public bool Rebinding { get => _rebinding; set => _rebinding = value; }
    [SerializeField] private bool _onRebindPanel;
    public bool OnRebindPanel { get => _onRebindPanel; set => _onRebindPanel = value; }

    [SerializeField] private bool _insideBuilding;
    public bool InsideBuilding { get => _insideBuilding; set => _insideBuilding = value; }

    public void OnCutsceneToFalseInSeconds(float time) {
        Invoke("OnCutsceneFalse", time);
    }

    void OnCutsceneFalse() {
        OnCutscene = false;
	}


}
