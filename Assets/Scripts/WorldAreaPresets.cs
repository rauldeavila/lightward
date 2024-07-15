using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldAreaPresets : MonoBehaviour {

    public string CurrentArea;
    public string CurrentScene;

    // wiz light: 
    //             WizLightShaker.Instance.SetLight(Intensity, InnerRadius, OuterRadius, Shake, LerpVelocity);

    // SFX
    // SFXController.Instance.SetWindVolume(1f);
    // SFXController.Instance.SetGardensVolume(1f);
    // SFXController.Instance.SetThunderstormVolume(1f);


    // SET WIZ LIGHT
    // SET AMBIENT NOISES
    // SET COLOR FILTERS
    // Particles are set in AmbientParticlesHandler.cs

    void Start(){
        Invoke("UpdateEverything", 0.4f);
    }

    void UpdateEverything(){
        CurrentArea = ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("game_current_area").runTimeValue;
        CurrentScene = ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("game_current_scene").runTimeValue;

        switch (CurrentArea){ // Set on ScenesManager.cs
            case "graveyard":
                print("Graveyard configurations loaded.");
                // WizLightShaker.Instance.SetLight(1f, 3f, 10f, false, 0.5f);
                MainCamera.Instance.GraveyardCam(true);
                SFXController.Instance.SetWindVolume(0f);
                SFXController.Instance.SetGardensVolume(0f);
                SFXController.Instance.SetThunderstormVolume(0.5f);
                break;
            case "forest":
                if(CurrentScene == "forest_graveyard_transition"){
                    print("Forest->Graveyard transition configurations loaded.");
                    // WizLightShaker.Instance.SetLight(1f, 4f, 16f, false, 0.5f);
                    MainCamera.Instance.ForestCam(true);
                    SFXController.Instance.SetWindVolume(0.2f);
                    SFXController.Instance.SetGardensVolume(0.2f);
                    SFXController.Instance.SetThunderstormVolume(0f);
                } else {
                    print("Forest configurations loaded.");
                    // WizLightShaker.Instance.SetLight(1f, 5f, 20f, false, 0.5f);
                    MainCamera.Instance.ForestCam(true);
                    SFXController.Instance.SetWindVolume(0.4f);
                    SFXController.Instance.SetGardensVolume(1f);
                    SFXController.Instance.SetThunderstormVolume(0f);
                }
                break;
            case "catacombs":
                print("Catacombs configurations loaded.");
                // MainCamera.Instance.GraveyardCam(true);
                SFXController.Instance.SetWindVolume(0f);
                SFXController.Instance.SetGardensVolume(0f);
                SFXController.Instance.SetThunderstormVolume(0f);
                break;
            default:
                print("Default configurations loaded (no area assigned to this scene name - Check made on SO: game_current_area");
                break;
        }
    }

}
