using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientParticlesHandler : MonoBehaviour {

    public GameObject ForestParticles;
    public GameObject GraveyardParticles;

    void DisableAllParticles(){
        if(ForestParticles != null){
            ForestParticles.SetActive(false);
        }
        if(GraveyardParticles != null){
            GraveyardParticles.SetActive(false);
        }
    }

    void Start(){
        DisableAllParticles();
        Invoke("LittleWaitForSettingThingsUp", 0.4f);
    }

    void LittleWaitForSettingThingsUp(){
        switch(ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("game_current_area").runTimeValue){
            case "forest":
                if(ForestParticles != null){
                    ForestParticles.SetActive(true);
                }
                break;
            case "graveyard":
                if(GraveyardParticles != null){
                    GraveyardParticles.SetActive(true);
                }
                break;
            case "city":
                break;
            default:
                break;
        }
    }

}
