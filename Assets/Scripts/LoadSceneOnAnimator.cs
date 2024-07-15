using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnAnimator : MonoBehaviour {
    
    public string SceneName;

    public void Load(){
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("game_new_game", true);
        SceneManager.LoadScene(SceneName);
    }
}
