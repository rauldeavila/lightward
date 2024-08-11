using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class ScenesManager : MonoBehaviour {

    public bool MainMenu = false;
    public bool FinishedLoading = false;
    public static ScenesManager Instance;

    private string _sceneNameToLoad;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }

    void Update(){
        if(!MainMenu){
            if(SceneManager.GetActiveScene().name != GameState.Instance.CurrentScene){
                GameState.Instance.CurrentScene = SceneManager.GetActiveScene().name;
                if(GameState.Instance.CurrentScene.StartsWith("forest") || GameState.Instance.CurrentScene.StartsWith("for")){
                    ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("game_current_area", "forest");
                } else if(GameState.Instance.CurrentScene.StartsWith("graveyard") || GameState.Instance.CurrentScene.StartsWith("grv")){
                    ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("game_current_area", "graveyard");
                } else if(GameState.Instance.CurrentScene.StartsWith("cat")){
                    ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("game_current_area", "catacombs");
                }
                ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("game_current_scene", GameState.Instance.CurrentScene);
            }
        }
    }

    void OnEnable() {
        FinishedLoading = false;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        StartCoroutine(LoadCompleteCoroutine());
    }

    IEnumerator LoadCompleteCoroutine() {
        // Debug.Log("Scene loading complete!");
        FinishedLoading = true;
        if(!MainMenu){
            if(CrossfadePanelController.Instance != null)
            {
                CrossfadePanelController.Instance.Default();
            }
        }
        if(Inputs.Instance)
        {
            Inputs.Instance.EnableInputs();
        }
        yield return null;
    }

    [Button]
    public void GoToScene([ValueDropdown("GetSceneNames")] string sceneToLoad)
    {
        // SceneManagement.EditorSceneManager.OpenScene(sceneToLoad);
    }

    private string[] GetSceneNames()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        string[] scenePaths = new string[sceneCount];

        for (int i = 0; i < sceneCount; i++)
        {
            scenePaths[i] = SceneUtility.GetScenePathByBuildIndex(i);
        }

        return scenePaths;
    }


    public void LoadScene(string _name, string _facing, float _posX, float _posY)
    {
        _sceneNameToLoad = _name;
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_new_scene_dashing_soul", false);
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_new_scene_up", false);
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_new_scene_falling", false);
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_new_scene_door", true);
        if (_facing == "left"){
            ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_facing_right", false);
        } else if(_facing == "right"){
            ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_facing_right", true);
        } else {
            ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_facing_right", PlayerState.Instance.FacingRight);
        }

        ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_x").runTimeValue = _posX;
        ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_y").runTimeValue = _posY; 
        ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("game_changing_scene").runTimeValue = true;
        CrossfadePanelController.Instance.FadeOut();
        ControllerRumble.Instance.StopRumble();
        Invoke("LoadNow", 0.5f);
    }
        
    void LoadNow(){
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("game_second_to_last_scene", ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("game_last_scene").runTimeValue);
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("game_last_scene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(_sceneNameToLoad);    
    }


}
