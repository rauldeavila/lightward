using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QFSW.QC;

public class Commands : MonoBehaviour {


    [Command("restart_game", "Resets all scriptable objects and restarts the game.")]
    public void RestartGame(){
        // restart game
        print("still doing nothing...");
    }

    [Command("give_key", "Gives `x` keys to Wiz. Default = 1.")]
    public void GiveKeys(int amount = 1){
        // modify using the new scriptable objects manager and create:
        // give coin
        // set magic
        // set health
        // 
    }

    [Command("give_coin", "Gives `x` coins to Wiz. Default = 1.")]
    public void GiveCoin(int amount = 1){
        GameEvents.Instance.AddWizIntValue("wiz_coins", amount);
    }

    [Command("set_magic", "Set Wiz magic level form 0 to 100. Default = 100.")]
    public void SetMagic(int amount = 10){
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<IntValue>("wiz_magic", amount);
    }

    [Command("set_health", "Set Wiz health level form 1 to 15. Default = 5.")]
    public void SetHealth(float amount = 5f){
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("wiz_health", amount);
    }

    [Command("set_intvalue", "Set IntValue by name")]
    public void SetIntValue(string scriptableObjectName, int newValue){
        GameEvents.Instance.SetWizIntValue(scriptableObjectName, newValue);
    }

    [Command("dashinglight", "Set Dashing Light spell value")]
    public void SetDashingLight(bool value){
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_dashing_light", value);
    }

    [Command("dashingsoul", "Set Dashing Soul spell value")]
    public void SetDashingSoul(bool value){
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_dashing_soul", value);
    }


    [Command("fireball", "Set Fireball spell value")]
    public void SetFireball(bool value){
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_fireball", value);
    }

    [Command("resetgame", "Reset all scriptables and play forest0")]
    public void ResetGame(){
        ScriptableObjectsManager.Instance.ResetAllForNewGame();
        SceneManager.LoadScene("forest_0");
    }

    [Command("resetgameintro", "Reset all scriptables and play intro")]
    public void ResetGameWithIntro(){
        ScriptableObjectsManager.Instance.ResetAllForNewGame();
        SceneManager.LoadScene("intro");
    }

    [Command("debug", "Reset Values For Debugging")]
    public void SetScriptablesToDebug(){
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("game_new_game", false);
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("wiz_health", 10);
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<IntValue>("wiz_magic", 10);
        ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("wiz_health").maxValue = 10;
        ScriptableObjectsManager.Instance.GetScriptableObject<IntValue>("wiz_magic").maxValue = 10;
    }

    [Command("load", "Load specified scene")]
    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }



    // [Command("cloak", "Set Wiz current cloak")]
    // public void SetCloak(string nameOfCloak){
    //     if((nameOfCloak == "default") || (nameOfCloak == "purple")){
    //         EquipCloak(1);
    //     } else if((nameOfCloak == "red") || (nameOfCloak == "hot") || (nameOfCloak == "fire")){
    //         EquipCloak(2);
    //     } else if((nameOfCloak == "blue") || (nameOfCloak == "cold") || (nameOfCloak == "snow")){
    //         EquipCloak(3);
    //     }
    // }

    // void EquipCloak(int cloakNum){
    //         switch (cloakNum) {
    //             case 1:
    //                 PlayerState.Instance.BlueCloak = false;
    //                 PlayerState.Instance.RedCloak = false;
    //                 PlayerState.Instance.DefaultCloak = true;
    //                 PlayerStats.Instance.cloak3.runTimeValue = false;
    //                 PlayerStats.Instance.cloak2.runTimeValue = false;
    //                 PlayerStats.Instance.cloak1.runTimeValue = true;
    //                 FindObjectOfType<WizCloakController>().SetCloakToDefault();
    //                 FindObjectOfType<WizEyesController>().SetEyesToDefault();
    //                 break;
    //             case 2:
    //                 PlayerState.Instance.DefaultCloak = false;
    //                 PlayerState.Instance.BlueCloak = false;
    //                 PlayerState.Instance.RedCloak = true;
    //                 PlayerStats.Instance.cloak1.runTimeValue = false;
    //                 PlayerStats.Instance.cloak3.runTimeValue = false;
    //                 PlayerStats.Instance.cloak2.runTimeValue = true;
    //                 FindObjectOfType<WizCloakController>().SetCloakToRed();
    //                 FindObjectOfType<WizEyesController>().SetEyesToRed();
    //                 break;
    //             case 3:
    //                 PlayerState.Instance.DefaultCloak = false;
    //                 PlayerState.Instance.RedCloak = false;
    //                 PlayerState.Instance.BlueCloak = true;
    //                 PlayerStats.Instance.cloak1.runTimeValue = false;
    //                 PlayerStats.Instance.cloak2.runTimeValue = false;
    //                 PlayerStats.Instance.cloak3.runTimeValue = true;
    //                 FindObjectOfType<WizCloakController>().SetCloakToBlue();
    //                 FindObjectOfType<WizEyesController>().SetEyesToBlue();
    //                 break;
    //             default:
    //                 break;
    //         }
    //     }

    [Command("tp", "Teleport Wiz to the specificed position.")]
    public void GoTo(float x, float y){
        PlayerController.Instance.SetPlayerPosition(x, y);
    }

    [Command("move", "Move wiz x and y position relative to its current one")]
    public void Move(float x, float y){
        PlayerController.Instance.SetPlayerPosition(PlayerController.Instance.transform.position.x + x, PlayerController.Instance.transform.position.y + y);
    }











}
