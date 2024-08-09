using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Sirenix.OdinInspector; // for fast debugging xD

public class GameManager : MonoBehaviour {

    public GameObject WizHouseForNewGame;
    public GameObject SavePanel;
    public GameObject PausePanel;
    public int targetFrameRate = 60; // Desired frame rate

    public ScriptableRendererFeature feature;

    public GameState State { get => GetComponent<GameState>(); }
    private PlayerController controller;

    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus Master;
    FMOD.Studio.Bus UI_SFX;

    private float SFXVolumeBeforePausing = 1f;
    float MusicVolume = 1f;
    float SFXVolume = 1f;
    float UISFXVolume = 1f;
    float MasterVolume = 1f;

    private Volume _globalVolume;
    private string darkworldProfile = "";
    private string overworldProfile = "";


    private List<UnityEngine.Rendering.VolumeProfile> _profiles;
    private VolumeProfile[] _loadedProfiles;

    private VolumeProfile DarkworldProfile;
    private VolumeProfile DarkworldCRTPRofile;
    private VolumeProfile OverworldProfile;
    private VolumeProfile OverworldCRTProfile;

    [HideInInspector]
    public UnityEvent OnEnterDarkworld;    
    [HideInInspector]
    public UnityEvent OnExitDarkworld;    


    public static GameManager Instance;

    void Awake(){

        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 

        _globalVolume = FindObjectOfType<Volume>(); 
        if(PlayerPrefs.GetInt("CRT") == 1){
            // to-do FIX
        } else {
            // todo - fIX 
        }
        Application.targetFrameRate = targetFrameRate; // Lock the FPS to the desired frame rate
        Screen.SetResolution(1920, 1080, true);
        QualitySettings.vSyncCount = 0; // Enable VSync
        Time.fixedDeltaTime = 1f / targetFrameRate; // Set fixedDeltaTime to match the desired frame rate
        controller = FindObjectOfType<PlayerController>();


        _profiles = new List<UnityEngine.Rendering.VolumeProfile>();
        _loadedProfiles = Resources.LoadAll<VolumeProfile>("Volumes");
        _profiles.AddRange(_loadedProfiles);
    }

    public void SetFPS(int newFPS){
        targetFrameRate = newFPS;
        Application.targetFrameRate = targetFrameRate; // Lock the FPS to the desired frame rate
        QualitySettings.vSyncCount = 0; // Enable VSync
        Time.fixedDeltaTime = 1f / targetFrameRate; // Set fixedDeltaTime to match the desired frame rate
    }

    void Start(){
        if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("game_new_game").runTimeValue){
            WizHouseForNewGame.SetActive(true);
            GameState.Instance.InsideBuilding = true;
        }

        UI_SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/sfxui");
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/sfx");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        UISFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        UpdateProfiles();
    }

    [Button]
    public void ToggleCRT()
    {
        GameState.Instance.CRT = !GameState.Instance.CRT;
        feature.SetActive(GameState.Instance.CRT);
        UpdateProfiles();
    }

    private void UpdateProfiles()
    {
        if (GameState.Instance.CRT)
        {
            darkworldProfile = "lightward_darkworld_crt";
            overworldProfile = "lightward_overworld_crt";
        }
        else
        {
            darkworldProfile = "lightward_darkworld_no_crt";
            overworldProfile = "lightward_overworld_no_crt";
        }
        if(GameState.Instance.Darkworld)
        {
            LoadProfileByName(darkworldProfile);
        }
        else
        {
            LoadProfileByName(overworldProfile);
        }
    }


    [Button()]
    public void TogglePause()
    {
        if(GameState.Instance.Paused == false)
        {
            print("       - PAUSED -        ");
            PausePanel.SetActive(true);
            Time.timeScale = 0f;
            GameState.Instance.Paused = true;
        }
        else
        {
            if(!GameState.Instance.OnRebindPanel)
            {
                PausePanel.SetActive(false);
                Time.timeScale = 1f;
                GameState.Instance.Paused = false;
            }
        }
    }


    [Button("Open Save Panel")]
    public void OpenSavePanel()
    {
        GameState.Instance.Overworld = false;
        SavePanel.SetActive(true);
    }

    [Button("Close Save Panel")]
    public void CloseSavePanel(bool enterDarkworld = false)
    {
        if(enterDarkworld == false)
        {
            GameState.Instance.Overworld = true;
        }
        SavePanel.SetActive(false);
    }

    [Button("Instant Shadow Walk")]
    public void InstantShadowWalk()
    {
        Debug.Log("<color=#0E9C9A>Switching realms. Fasten your seatbelt.</color>");
        if(GameState.Instance.Darkworld)
        {   
            GameState.Instance.Darkworld = false;
            StopSound("event:/char/wiz/shadow_walk");
            LoadProfileByName(overworldProfile);
            OnExitDarkworld?.Invoke();
        }
        else // ENTERING DARK WORLD
        {
            GameState.Instance.Darkworld = true;
            PlaySound("event:/char/wiz/shadow_walk");
            Invoke("DarkWorldBloom", 3f);
            LoadProfileByName(darkworldProfile);
            OnEnterDarkworld?.Invoke();
        }
    }

    public void ShadowWalk()
    {
        if(GameState.Instance.Darkworld)
        {
            ExitDarkworld();
            StopSound("event:/char/wiz/shadow_walk");
        }
        else // ENTERING DARK WORLD
        {
            EnterDarkworld();
            PlaySound("event:/char/wiz/shadow_walk");
            Invoke("DarkWorldBloom", 3f);
        }
        CloseSavePanel(true);
    }

    private void DarkWorldBloom()
    {
        GlobalVolumeShaker.Instance.ToggleDarkWorldBloom();
    }

    private void EnterDarkworld()
    {
        // CameraSystem.Instance.DarkworldTransition();
        StartCoroutine("LoadDarkworldProfile");
        GameState.Instance.Darkworld = true;
        OnEnterDarkworld?.Invoke();
    }

    private void ExitDarkworld()
    {
        // CameraSystem.Instance.DarkworldTransition();
        StartCoroutine("LoadOverworldProfile");
        GameState.Instance.Darkworld = false;
        OnExitDarkworld?.Invoke();
    }

    void Update(){

        // if(!State.Paused){
        //     SFX.setVolume(PlayerPrefs.GetFloat("SFXVolume", 1f));
        // }
        UI_SFX.setVolume(PlayerPrefs.GetFloat("UISFXVolume", 1f));
        Music.setVolume(PlayerPrefs.GetFloat("MusicVolume", 1f));
        Master.setVolume(PlayerPrefs.GetFloat("MasterVolume", 1f));
    }
    

    public void EndGame(){
        if (!State.GameHasEnded){
            State.GameHasEnded = true;    
        }    
    }

    public void MuteSFXBus(){
        SFXVolumeBeforePausing = PlayerPrefs.GetFloat("SFXVolume", 1f);
        SFX.setVolume(0f);
    }



    public void PlaySound(string eventPath)
    {
        SFXController.Instance.Play(eventPath);
    }

    public void StopSound(string eventPath)
    {
        SFXController.Instance.Stop(eventPath);
    }

    public void UnnuteSFXBus(){
        SFX.setVolume(SFXVolumeBeforePausing);
    }

    private void LoadProfileByName(string profileName)
    {
        // Find a matching profile
        VolumeProfile targetProfile = _profiles.FirstOrDefault(profile => profile.name == profileName); 

        // Check if the profile was found
        if (targetProfile != null)
        {
            _globalVolume.profile = targetProfile;
            // Debug.Log("Profile loaded: " + profileName);
        }
        else
        {
            Debug.LogWarning("Profile not found: " + profileName);
        }
    }


    private IEnumerator LoadDarkworldProfile()
    {
        yield return new WaitForSeconds(1f);
        LoadProfileByName(darkworldProfile);
        yield return new WaitForSeconds(0.15f);
        LoadProfileByName(overworldProfile);
        yield return new WaitForSeconds(0.4f);
        LoadProfileByName(darkworldProfile);
        yield return new WaitForSeconds(0.1f);
        LoadProfileByName(overworldProfile);
        yield return new WaitForSeconds(0.1f);
        LoadProfileByName(darkworldProfile);
        yield return new WaitForSeconds(0.2f);
        LoadProfileByName(overworldProfile);
        yield return new WaitForSeconds(0.2f);
        LoadProfileByName(darkworldProfile);
        GameState.Instance.Overworld = true;
    }
    private IEnumerator LoadOverworldProfile()
    {
        yield return new WaitForSeconds(1f);
        LoadProfileByName(overworldProfile);
        yield return new WaitForSeconds(0.15f);
        LoadProfileByName(darkworldProfile);
        yield return new WaitForSeconds(0.4f);
        LoadProfileByName(overworldProfile);
        yield return new WaitForSeconds(0.1f);
        LoadProfileByName(darkworldProfile);
        yield return new WaitForSeconds(0.1f);
        LoadProfileByName(overworldProfile);
        yield return new WaitForSeconds(0.2f);
        LoadProfileByName(darkworldProfile);
        yield return new WaitForSeconds(0.2f);
        LoadProfileByName(overworldProfile);
        GameState.Instance.Overworld = true;
    }
}