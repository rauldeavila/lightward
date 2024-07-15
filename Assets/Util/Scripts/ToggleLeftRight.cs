using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleLeftRight : MonoBehaviour {

    public bool crt;
    public bool rumble;
    public bool language;

    public GameObject OnObject;
    public GameObject OffObject;
    public GameObject EnglishObject;
    public GameObject PortuguesObject;

    private bool _onCRT;
    private bool _onRumble;
    private bool _onLanguage;

    private bool canPress = true;

    [FMODUnity.EventRef]
    private string ChangeSelection = "event:/game/00_ui/ui_changeselection";

    private PlayerController controller;

    private void Awake() {

        controller = FindObjectOfType<PlayerController>();

        if(crt){
            if(PlayerPrefs.GetInt("CRT") == 0){
                OnObject.SetActive(false);
                OffObject.SetActive(true);
                GameState.Instance.CRT = false;
                PlayerPrefs.SetInt("CRT", 0);
                GameManager.Instance.ToggleCRT();
            } else if(PlayerPrefs.GetInt("CRT") == 1){
                OffObject.SetActive(false);
                OnObject.SetActive(true);
                GameState.Instance.CRT = true;
                PlayerPrefs.SetInt("CRT", 1);
                GameManager.Instance.ToggleCRT();
            }
        }

        if(rumble){
            if(PlayerPrefs.GetInt("Rumble") == 0){
                OnObject.SetActive(false);
                OffObject.SetActive(true);
                GameState.Instance.Rumble = false;
            } else if(PlayerPrefs.GetInt("Rumble") == 1){
                OffObject.SetActive(false);
                OnObject.SetActive(true);
                GameState.Instance.Rumble = true;
            }
        }

        if(language){
            if(PlayerPrefs.GetInt("Language") == 0){
                PortuguesObject.SetActive(false);          
                EnglishObject.SetActive(true);
            } else if(PlayerPrefs.GetInt("Language") == 1){
                EnglishObject.SetActive(false);
                PortuguesObject.SetActive(true);          
            }
        }

    }

    private void Start() {
        controller.Controls.Game.MoveRight.performed += ctx => RightPressed();
        controller.Controls.Game.MoveLeft.performed += ctx => LeftPressed();
    }

    private void RightPressed(){
        if(canPress && GameState.Instance.Paused){
            canPress = false;
            Invoke("EnablePress", 0.2f);
            if(crt && _onCRT){
                CRTRight();
            } else if(rumble && _onRumble){
                RumbleRight();
            } else if(language && _onLanguage){
                LanguageRight();
            }
        }
    }

    private void LeftPressed(){
        if(canPress && GameState.Instance.Paused){
            canPress = false;
            Invoke("EnablePress", 0.2f);
            if(crt && _onCRT){
                CRTLeft();
            } else if(rumble && _onRumble){
                RumbleLeft();
            }  else if(language && _onLanguage){
                LanguageLeft();
            }
        }
    }

    void OnEnable(){
        EnablePress();

        if(crt){
            if(PlayerPrefs.GetInt("CRT") == 0){
                OnObject.SetActive(false);
                OffObject.SetActive(true);
            } else if(PlayerPrefs.GetInt("CRT") == 1){
                OffObject.SetActive(false);
                OnObject.SetActive(true);
            }
        }

        if(rumble){
            if(PlayerPrefs.GetInt("Rumble") == 0){
                OnObject.SetActive(false);
                OffObject.SetActive(true);
            } else if(PlayerPrefs.GetInt("Rumble") == 1){
                OffObject.SetActive(false);
                OnObject.SetActive(true);
            }
        }
    }

    void OnDisable(){
        canPress = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void EnablePress(){
        canPress = true;
    }

    void CRTRight(){
        FMODUnity.RuntimeManager.PlayOneShot(ChangeSelection, transform.position);
        if(OnObject.activeInHierarchy){
            OnObject.SetActive(false);
            OffObject.SetActive(true);
            PlayerPrefs.SetInt("CRT", 0);
            GameState.Instance.CRT = false;
            GameManager.Instance.ToggleCRT();
        } else{
            OffObject.SetActive(false);
            OnObject.SetActive(true);
            PlayerPrefs.SetInt("CRT", 1);
            GameState.Instance.CRT = true;
            GameManager.Instance.ToggleCRT();
        }
    }

    void CRTLeft(){
        FMODUnity.RuntimeManager.PlayOneShot(ChangeSelection, transform.position);
        if(OnObject.activeInHierarchy){
            OnObject.SetActive(false);
            OffObject.SetActive(true);
            PlayerPrefs.SetInt("CRT", 0);
            GameState.Instance.CRT = false;
            GameManager.Instance.ToggleCRT();
        } else{
            OffObject.SetActive(false);
            OnObject.SetActive(true);
            PlayerPrefs.SetInt("CRT", 1);
            GameState.Instance.CRT = true;
            GameManager.Instance.ToggleCRT();
        }
    }

    void RumbleRight(){
        FMODUnity.RuntimeManager.PlayOneShot(ChangeSelection, transform.position);
        if(OnObject.activeInHierarchy){
            OnObject.SetActive(false);
            OffObject.SetActive(true);
            PlayerPrefs.SetInt("Rumble", 0);
            GameState.Instance.Rumble = false;
        } else{
            OffObject.SetActive(false);
            OnObject.SetActive(true);
            PlayerPrefs.SetInt("Rumble", 1);
            GameState.Instance.Rumble = true;
        }
    }

    void RumbleLeft(){
        FMODUnity.RuntimeManager.PlayOneShot(ChangeSelection, transform.position);
        if(OnObject.activeInHierarchy){
            OnObject.SetActive(false);
            OffObject.SetActive(true);
            PlayerPrefs.SetInt("Rumble", 0);
            GameState.Instance.Rumble = false;
        } else{
            OffObject.SetActive(false);
            OnObject.SetActive(true);
            PlayerPrefs.SetInt("Rumble", 1);
            GameState.Instance.Rumble = true;
        }
    }

    void LanguageRight(){
        FMODUnity.RuntimeManager.PlayOneShot(ChangeSelection, transform.position);
        if(EnglishObject.activeInHierarchy){
            EnglishObject.SetActive(false);
            PortuguesObject.SetActive(true);
            LanguageController.Instance.SetLanguage(LanguageController.Language.Portuguese);
            PlayerPrefs.SetInt("Language", 1);
        } else{
            PortuguesObject.SetActive(false);
            EnglishObject.SetActive(true);
            LanguageController.Instance.SetLanguage(LanguageController.Language.English);
            PlayerPrefs.SetInt("Language", 0);
        }
    }

    void LanguageLeft(){
        FMODUnity.RuntimeManager.PlayOneShot(ChangeSelection, transform.position);
        if(EnglishObject.activeInHierarchy){
            EnglishObject.SetActive(false);

            PortuguesObject.SetActive(true);
            PlayerPrefs.SetInt("Language", 1);
        } else{
            PortuguesObject.SetActive(false);
            
            EnglishObject.SetActive(true);
            PlayerPrefs.SetInt("Language", 0);
        }
        
    }


    void Update(){
        if(EventSystem.current.currentSelectedGameObject.name == "CRT"){
            _onLanguage = false;
            _onRumble = false;
            _onCRT = true;
        } else if(EventSystem.current.currentSelectedGameObject.name == "Rumble"){
            _onLanguage = false;
            _onCRT = false;
            _onRumble = true;
        } else if(EventSystem.current.currentSelectedGameObject.name == "Language"){
            _onRumble = false;
            _onCRT = false;
            _onLanguage = true;
        }else{
            _onCRT = false;
            _onLanguage = false;
            _onRumble = false;
            // not a toggle button
            CancelInvoke();
            canPress = true;
        }
    }

    
}
