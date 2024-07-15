using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogHelper : MonoBehaviour {

    private bool _attackingUp = false;
    private bool _hidden1 = false;
    private bool _hidden2 = false;
    private bool _enemy1 = false;

    public void WizAttackingUp(){
        if(!_attackingUp && PlayerController.Instance.AnimatorIsPlaying("attack_up")){
            _attackingUp = true;
            LogSystem.Instance.AppendLog("killed_enemy_attacking_up: true");
        }
    }

    public void EnemyOneHitWiz(){
        if(PlayerController.Instance.AnimatorIsPlaying("jump") || PlayerController.Instance.AnimatorIsPlaying("fall")){
            if(!_enemy1){
                _enemy1 = true;
                LogSystem.Instance.AppendLog("tried_to_jump_on_enemy_1: true");
            }
        }
    }

    void OnEnable(){
        if(SceneManager.GetActiveScene().name == "forest_hidden_1" && !_hidden1){
            _hidden1 = true;
            LogSystem.Instance.AppendLog("forest_hidden_1: true");
        }
        if(SceneManager.GetActiveScene().name == "forest_hidden_2" && !_hidden2){
            _hidden2 = true;
            LogSystem.Instance.AppendLog("forest_hidden_2: true");
        }
    }

    public void HiddenArea1(){
        LogSystem.Instance.AppendLog("hidden_area_1: true");
    }

    void OnDisable(){
        if(SceneManager.GetActiveScene().name == "forest_4"){
            if(_attackingUp == false){
                LogSystem.Instance.AppendLog("killed_enemy_attacking_up: false");
            }
        }
    }

}
