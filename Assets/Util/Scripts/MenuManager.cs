using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {


        public static MenuManager Instance;

        void Awake(){
                if (Instance != null && Instance != this){ 
                        Destroy(this); 
                } else { 
                        Instance = this; 
                }
                PlayerPrefs.SetFloat("MasterVolume", 1f);
                PlayerPrefs.SetFloat("SFXVolume", 1f);
                PlayerPrefs.SetFloat("UISFXVolume", 1f);
                PlayerPrefs.SetFloat("MusicVolume", 1f);
        }

        public void SetEnglishAndLoadMenu(){
                SFXController.Instance.Play("event:/game/00_ui/ui_select");
                PlayerPrefs.SetInt("Language", 0);
                PlayerPrefs.SetInt("CRT", 1);
                PlayerPrefs.SetInt("Rumble", 1);
                SceneManager.LoadScene("disclosure");
        }

        public void SetPortugueseAndLoadMenu(){
                SFXController.Instance.Play("event:/game/00_ui/ui_select");
                PlayerPrefs.SetInt("Language", 1);
                PlayerPrefs.SetInt("CRT", 1);
                PlayerPrefs.SetInt("Rumble", 1);
                SceneManager.LoadScene("disclosure");
        }

}
