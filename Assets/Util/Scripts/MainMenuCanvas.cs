using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour {

    private string bugReport = "https://forms.gle/wEq1eNSCoLjQomh58";
    private string feedbackForm = "https://forms.gle/ncvvvskJFMJUCC2U9";
    private string discordLink = "https://discord.gg/W8Bsm3NKsy";
    public GameObject mainMenuPanel, optionsPanel, gamePanel;
    public GameObject menuFirstButton, optionsFirstButton, gameFirstButton, gameFirstButton_PTBR, returnToMenuFromOptionsButton, returnToMenuFromOptionsButton_PTBR, returnToMenuFromGameButton, returnToMenuFromGameButton_PTBR;
    private ToggleObject toggle;
    
    private void Awake(){
        toggle = FindObjectOfType<ToggleObject>();
    }

    public void OpenOptions(){
        toggle.Toggle();
        optionsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void CloseOptions(){
        toggle.Toggle();
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);

        if(PlayerPrefs.GetInt("Language", 0) == 0){
            EventSystem.current.SetSelectedGameObject(returnToMenuFromOptionsButton);
        } else if(PlayerPrefs.GetInt("Language", 0) == 1){
            EventSystem.current.SetSelectedGameObject(returnToMenuFromOptionsButton_PTBR);
        }
    }

    public void OpenGameMenu(){
        toggle.Toggle();
        gamePanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        if(PlayerPrefs.GetInt("Language", 0) == 0){
            EventSystem.current.SetSelectedGameObject(gameFirstButton);
        } else if(PlayerPrefs.GetInt("Language", 0) == 1){
            EventSystem.current.SetSelectedGameObject(gameFirstButton_PTBR);
        }

    }

    public void CloseGameMenu(){
        toggle.Toggle();
        mainMenuPanel.SetActive(true);
        gamePanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        if(PlayerPrefs.GetInt("Language", 0) == 0){
            EventSystem.current.SetSelectedGameObject(returnToMenuFromGameButton);
        } else if(PlayerPrefs.GetInt("Language", 0) == 1){
            EventSystem.current.SetSelectedGameObject(returnToMenuFromGameButton_PTBR);
        }

    }

    public void OpenBugReport(){
        Application.OpenURL(bugReport);
    }

    public void OpenFeedbackForm(){
        Application.OpenURL(feedbackForm);
    }

    public void OpenDiscord(){
        Application.OpenURL(discordLink);
    }

    public void QuitGame(){
        Application.Quit();
    }

}
