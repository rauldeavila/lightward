using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

[RequireComponent(typeof(BoxCollider2D))]
public class Door : MonoBehaviour {

    [LabelText("Usage Guide")]
    [InfoBox("Name this object the same as the Scriptable Object you want to load. Location: Resources/ScriptableObjects/Doors")]
    public int hidVar;

    private DoorData _doorData;
    private bool _triggerEnabled = false;

    void Awake(){
        Invoke("EnableTrigger", 0.5f); // for walking animation
    }

    void Start(){
        _doorData = Resources.Load<DoorData>("ScriptableObjects/Doors/" + gameObject.name);
        if(_doorData == null){
            Debug.LogError("DoorData not found for " + gameObject.name);
        }
    }

    void EnableTrigger(){
        _triggerEnabled = true;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(_triggerEnabled == true){
            if(collider.CompareTag("WizHitBox") || collider.CompareTag("WizRoll")){
                ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_new_scene_door", false); // for buildings
                if(PlayerState.Instance.DashingSoul){
                    ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_new_scene_dashing_soul", true);
                } else {
                    if(_doorData.Up){
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_new_scene_up", true);
                        PlayerController.Instance.SetGravityTo(-10f);
                        if(_doorData.Left){
                            PlayerState.Instance.FacingRight = false;
                            ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_facing_right", false);
                        } else if(_doorData.Right){
                            PlayerState.Instance.FacingRight = true;
                            ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_facing_right", true);
                        }
                    } else if(_doorData.Down){
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_new_scene_falling", true);
                        if(_doorData.Left){
                            PlayerState.Instance.FacingRight = false;
                            ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_facing_right", false);
                        } else if(_doorData.Right){
                            PlayerState.Instance.FacingRight = true;
                            ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_facing_right", false);
                        } else{
                            ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_facing_right", PlayerState.Instance.FacingRight);
                        }
                    } else {
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_new_scene_dashing_soul", false);
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_new_scene_up", false);
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_new_scene_falling", false);
                        if (_doorData.Left){
                            ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_facing_right", false);
                        } else if(_doorData.Right){
                            ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_facing_right", true);
                        } else {
                            ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_facing_right", PlayerState.Instance.FacingRight);
                        }
                    }
                    ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_x").runTimeValue = _doorData.SpawnPosition.x;
                    ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_y").runTimeValue = _doorData.SpawnPosition.y;
                    ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("game_changing_scene").runTimeValue = true;
                    CrossfadePanelController.Instance.FadeOut();
                    ControllerRumble.Instance.StopRumble();
                    Invoke("LoadNow", 0.5f);
                }
            }
        }
    }

    void LoadNow(){
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("game_second_to_last_scene", ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("game_last_scene").runTimeValue);
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("game_last_scene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(_doorData.SceneName);    
    }

}
