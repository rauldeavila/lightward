using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerRumble : MonoBehaviour {
   
    private Gamepad gamepad;

    public static ControllerRumble Instance;
    
    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }
        
    void Start() {
        gamepad = Gamepad.current;
    }

    public void LandingRumble(){
        if(GameState.Instance.Rumble == false){ return; }
        gamepad?.SetMotorSpeeds(0.1f, 0.1f);
        Invoke("StopRumble", 0.2f);
    }

    public void RollRumble(){
        if(GameState.Instance.Rumble == false){ return; }
        gamepad?.SetMotorSpeeds(0.3f, 0.3f);
        Invoke("StopRumble", 0.2f);
    }

    public void DashingLightRumble(){
        if(GameState.Instance.Rumble == false){ return; }
        gamepad?.SetMotorSpeeds(0.4f, 0.4f);
        Invoke("StopRumble", 0.3f);
    }

    public void DashRumble(){
        if(GameState.Instance.Rumble == false){ return; }
        gamepad?.SetMotorSpeeds(0.5f, 0.5f);
        Invoke("StopRumble", 0.4f);
    }

    public void StrongImpactRumble(){
        if(GameState.Instance.Rumble == false){ return; }
        gamepad?.SetMotorSpeeds(0.5f, 0.5f);
        Invoke("StopRumble", 0.3f);
    }

    public void Rumble(){
        if(GameState.Instance.Rumble == false){ return; }
        gamepad?.SetMotorSpeeds(0.5f, 0.5f);
        Invoke("StopRumble", 0.5f);
    }

    private void OnApplicationQuit() {
        StopRumble();
    }

    public void StopRumble() {
        gamepad?.ResetHaptics();
    }

}
