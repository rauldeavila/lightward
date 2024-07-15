using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// TODO - Improve the translation...
public class AreaNameHandler : MonoBehaviour {

	private TextMeshProUGUI text;
    private Animator animator;
    public TextMeshProUGUI bigText;
    public TextMeshProUGUI smallText;

    public static AreaNameHandler Instance;
    private string _currentAreaText = "";
    private string _textToDisplay = "";

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        }    
        text = GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
    }

    void Update(){
        if(_currentAreaText != ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("game_current_area").runTimeValue){
            _currentAreaText = ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("game_current_area").runTimeValue;
            switch(_currentAreaText){
                case "forest":
                    _textToDisplay = "Lostwind Woods";
                    break;
                case "graveyard":
                    _textToDisplay = "Soulfields";
                    break;
                case "catacombs":
                    _textToDisplay = "Catacombs";
                    break;
                default:
                    break;
            }
            bigText.text = _textToDisplay;
            smallText.text = _textToDisplay;
        }
    }



    public void ShowAreaName(){
        switch(_currentAreaText){
            case "forest":
                ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("game_last_area_name_shown", "forest");
                if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("game_seen_forest").runTimeValue == true){
                    animator.SetTrigger("show_small");
                } else {
                    animator.SetTrigger("show_big");
                    ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("game_seen_forest", true);
                }
                break;
            case "graveyard":
                ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("game_last_area_name_shown", "graveyard");
                if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("game_seen_graveyard").runTimeValue == true){
                    animator.SetTrigger("show_small");
                } else {
                    animator.SetTrigger("show_big");
                    ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("game_seen_graveyard", true);
                }
                break;
            case "catacombs":
                ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("game_last_area_name_shown", "catacombs");
                if(ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>("game_seen_catacombs").runTimeValue == true){
                    animator.SetTrigger("show_small");
                } else {
                    animator.SetTrigger("show_big");
                    ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("game_seen_catacombs", true);
                }
                break;
            default:
                break;
        }
    }





}
