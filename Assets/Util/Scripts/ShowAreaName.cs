using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAreaName : MonoBehaviour {

    // BOX COLLIDER STARTS DISABLED
    public bool StartMusic = false;
    public string SoundPath = "";
    void Start(){
        Invoke("WaitAndRun", 1f);
    }

    void WaitAndRun(){
        GetComponent<BoxCollider2D>().enabled = true;
        if(
        (ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("game_last_area_name_shown").runTimeValue == "")
         || 
         (ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("game_last_area_name_shown").runTimeValue != ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("game_current_area").runTimeValue)
         ){
            return;
        } else {
            this.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("WizHitBox")){
            AreaNameHandler.Instance.ShowAreaName();
            if(StartMusic)
            {
                SFXController.Instance.PlayMusic(SoundPath);
            }
            this.gameObject.SetActive(false);
        }
    }

}
