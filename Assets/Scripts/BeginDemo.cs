using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginDemo : MonoBehaviour {


    public void LoadMenu(){
        SceneManager.LoadScene("menu");
    }
    public void StartNewGame(){
        ScriptableObjectsManager.Instance.ResetAllForNewGame();
        SceneManager.LoadScene("grv1");
    }

}
