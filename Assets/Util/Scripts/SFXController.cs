using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SFXController : MonoBehaviour {

    public static SFXController Instance;
    private Dictionary<string, FMOD.Studio.EventInstance> loopInstances = new Dictionary<string, FMOD.Studio.EventInstance>();
    private Dictionary<string, FMOD.Studio.EventInstance> _3dLoopInstances = new Dictionary<string, FMOD.Studio.EventInstance>();
    private Dictionary<string, FMOD.Studio.EventInstance> _2dLoopInstances = new Dictionary<string, FMOD.Studio.EventInstance>();
    private Dictionary<string, FMOD.Studio.EventInstance> musicInstances = new Dictionary<string, FMOD.Studio.EventInstance>(); 

    FMOD.Studio.Bus wind;
    FMOD.Studio.Bus gardens;
    FMOD.Studio.Bus thunderstorm;

    FMOD.Studio.Bus _music;

    public string thunderSFX = "event:/game/00_game/thunder";
    private bool _canPlayThunder = false;
    private float timer = 1f;

    void Awake(){
        // Singleton
        if (Instance != null && Instance != this){
            Destroy(this);
        } else {
            Instance = this;
        }

        BoolValue changing_scene = Resources.Load<BoolValue>("ScriptableObjects/game_changing_scene");

        wind = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/sfx/wind");
        gardens = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/sfx/gardens");
        thunderstorm = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/sfx/thunderstorm");
        _music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Underwater/music");    
        
        if(!changing_scene.runTimeValue){
            wind.setVolume(0f);
            Play("event:/game/00_game/wind_regular");
            gardens.setVolume(0f);
            Play("event:/game/00_game/gardens_atmosphere");
            thunderstorm.setVolume(0f);
            Play("event:/game/00_game/thunderstorm_ambience");
            Play("event:/game/02_graveyard/rain");
        }
    }

    void Start()
    {
        RoomConfigurations.OnRoomChanged.AddListener(UpdateSFXForRoom);
    }
    void UpdateSFXForRoom()
    {
        SetWindVolume(RoomConfigurations.CurrentRoom.Data.SFXWindVolume);
        SetGardensVolume(RoomConfigurations.CurrentRoom.Data.SFXGardensVolume);
        SetThunderstormVolume(RoomConfigurations.CurrentRoom.Data.SFXThunderstormVolume);
    }

    [Button]
    public void SetWindVolume(float newVolume){
        wind.setVolume(newVolume);
    }

    [Button]
    public void SetGardensVolume(float newVolume){
        gardens.setVolume(newVolume);
    }

    [Button]
    public void SetThunderstormVolume(float newVolume){
        thunderstorm.setVolume(newVolume);
    }


    void Update(){
        float thunderCurrentVolume;
        thunderstorm.getVolume(out thunderCurrentVolume);
        if (thunderCurrentVolume > 0f){
            _canPlayThunder = true;
        } else {
            _canPlayThunder = false;
        }

        if(_canPlayThunder){
            timer -= Time.deltaTime;
            if(timer <= 0f){
                FMODUnity.RuntimeManager.PlayOneShot(thunderSFX);
                if(thunderCurrentVolume <= 0.3){
                    timer = 4f;
                } else if(thunderCurrentVolume <= 0.6){
                    timer = 3f;
                } else if(thunderCurrentVolume <= 8){
                    timer = 2f;
                } else{
                    timer = 1f;
                }
            }
        }

    }



    public void Play(string EventPath){
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(EventPath);
        eventInstance.start();
        _2dLoopInstances[EventPath] = eventInstance;
    }
    public void Stop(string EventPath) {
        if (_2dLoopInstances.TryGetValue(EventPath, out FMOD.Studio.EventInstance eventInstance)){
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.release();
            _2dLoopInstances.Remove(EventPath);
        }
    }

    public void Play3D(string EventPath, Vector3 position) {
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(EventPath);
        FMOD.ATTRIBUTES_3D attributes = FMODUnity.RuntimeUtils.To3DAttributes(position);
        eventInstance.set3DAttributes(attributes);
        eventInstance.start();
        
        _3dLoopInstances[EventPath] = eventInstance;
    }

    public void Stop3D(string EventPath) {
        if (_3dLoopInstances.TryGetValue(EventPath, out FMOD.Studio.EventInstance eventInstance)){
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.release();
            _3dLoopInstances.Remove(EventPath);
        }
    }

    public void PlayLoop(string EventPath){
        if (!loopInstances.ContainsKey(EventPath)){
            FMOD.Studio.EventInstance eventInstanceLoop = FMODUnity.RuntimeManager.CreateInstance(EventPath);
            eventInstanceLoop.start();
            loopInstances.Add(EventPath, eventInstanceLoop);
        }
    }

    public void StopLoop(string EventPath){
        if (loopInstances.TryGetValue(EventPath, out FMOD.Studio.EventInstance eventInstanceLoop)){
            eventInstanceLoop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstanceLoop.release();
            loopInstances.Remove(EventPath);
        }
    }

    public void PlayMusic(string EventPath){
        // Check if any music is already playing
        _music.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(EventPath);
        eventInstance.start();
        musicInstances.Add(EventPath, eventInstance);
    }

    public void StopMusic()
    {
        _music.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
