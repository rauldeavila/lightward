using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public string SceneName;
    public float SpawnX;
    public float SpawnY;
    public bool Up = false;
    public bool Down = false;
    public bool FacingRightForDownUp = false;
    public bool FacingLeftForDownUp = false;
    public bool ForceFacingRight = false;
    public bool ForceFacingLeft = false;
    private bool flag = false;

    void Awake(){
        Invoke("ChangeFlag", 0.5f); // for walking animation
    }

    void ChangeFlag(){
        flag =  true;
    } 

    private void OnTriggerEnter2D(Collider2D collider) {
        if(flag == true){
            if(collider.CompareTag("WizHitBox") || collider.CompareTag("WizRoll")){
                if(PlayerState.Instance.DashingSoul){
                    ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_new_scene_dashing_soul", true);
                } else if(Up) {
                    ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_new_scene_up", true);
                    PlayerController.Instance.SetGravityTo(-10f);
                    if(FacingRightForDownUp){
                        PlayerState.Instance.FacingRight = true;
                    } else if(FacingLeftForDownUp){
                        PlayerState.Instance.FacingRight = false;
                    }
                } else if(Down) {
                    ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_new_scene_falling", true);
                    if(FacingRightForDownUp){
                        PlayerState.Instance.FacingRight = true;
                    } else if(FacingLeftForDownUp){
                        PlayerState.Instance.FacingRight = false;
                    }
                } else {
                    ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_new_scene_dashing_soul", false);
                    ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_new_scene_up", false);
                    ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_new_scene_falling", false);
                }
                if(ForceFacingRight){
                    ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_facing_right", true);
                } else if(ForceFacingLeft){
                    ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_facing_right", false);
                } else {
                    ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_facing_right", PlayerState.Instance.FacingRight);
                }
                ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_x").runTimeValue = SpawnX;
                ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_y").runTimeValue = SpawnY;
                ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("game_changing_scene").runTimeValue = true;
                CrossfadePanelController.Instance.FadeOut();
                ControllerRumble.Instance.StopRumble();
                Invoke("LoadNow", 0.5f);
            }
        }
    }

    private void LoadNow(){
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("game_second_to_last_scene", ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("game_last_scene").runTimeValue);
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("game_last_scene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneName);
    }

}
