using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SelectedButtonVisualController : MonoBehaviour {

    public float TweenForce = 5f;
    public float TweenDuration = 1f;

    public TextMeshProUGUI CRTButton;
    public TextMeshProUGUI CRTState1;
    public TextMeshProUGUI CRTState2;
    public TextMeshProUGUI RumbleButton;
    public TextMeshProUGUI RumbleState1;
    public TextMeshProUGUI RumbleState2;
    public TextMeshProUGUI LanguageButton;
    public TextMeshProUGUI LanguageState1;
    public TextMeshProUGUI LanguageState2;
    public TextMeshProUGUI MusicButton;
    public TextMeshProUGUI SFXButton;
    public TextMeshProUGUI MasterButton;
    public TextMeshProUGUI ExitButton;

    public string currentButton;
    public string previousButton;

    void Awake(){
        previousButton = CRTButton.text;
        currentButton = CRTButton.text;
    }

    void Update(){
        switch(EventSystem.current.currentSelectedGameObject.name) {
            case "CRT":
                ResetAllColors();
                CRTButton.color = new Color(0.97f, 0.03f, 0.15f, 1f); // #F80828
                CRTState1.color = new Color(0.97f, 0.03f, 0.15f, 1f); // #F80828
                CRTState2.color = new Color(0.97f, 0.03f, 0.15f, 1f); // #F80828
                currentButton = "CRT";
                if(previousButton == "Rumble"){
                    ShakeUp();
                    previousButton = "CRT";
                }
                break;
            case "Rumble":
                ResetAllColors();
                RumbleButton.color = new Color(0.97f, 0.03f, 0.15f, 1f); // #F80828
                RumbleState1.color = new Color(0.97f, 0.03f, 0.15f, 1f); // #F80828
                RumbleState2.color = new Color(0.97f, 0.03f, 0.15f, 1f); // #F80828
                currentButton = "Rumble";
                if(previousButton == "Language"){
                    ShakeUp();
                    previousButton = "Rumble";
                } else if(previousButton == "CRT"){
                    ShakeDown();
                    previousButton = "Rumble";
                }
                break;
            case "Language":
                ResetAllColors();
                LanguageButton.color = new Color(0.97f, 0.03f, 0.15f, 1f); // #F80828
                LanguageState1.color = new Color(0.97f, 0.03f, 0.15f, 1f); // #F80828
                LanguageState2.color = new Color(0.97f, 0.03f, 0.15f, 1f); // #F80828
                currentButton = "Language";
                if(previousButton == "Music"){
                    ShakeUp();
                    previousButton = "Language";
                } else if(previousButton == "Rumble"){
                    ShakeDown();
                    previousButton = "Language";
                }
                break;
            case "Music":
                ResetAllColors();
                MusicButton.color = new Color(0.97f, 0.03f, 0.15f, 1f); // #F80828
                currentButton = "Music";
                if(previousButton == "SFX"){
                    ShakeUp();
                    previousButton = "Music";
                } else if(previousButton == "Language"){
                    ShakeDown();
                    previousButton = "Music";
                }
                break;
            case "SFX":
                ResetAllColors();
                SFXButton.color = new Color(0.97f, 0.03f, 0.15f, 1f); // #F80828
                currentButton = "SFX";
                if(previousButton == "Master"){
                    ShakeUp();
                    previousButton = "SFX";
                } else if(previousButton == "Music"){
                    ShakeDown();
                    previousButton = "SFX";
                }
                break;
            case "Master":
                ResetAllColors();
                MasterButton.color = new Color(0.97f, 0.03f, 0.15f, 1f); // #F80828
                currentButton = "Master";
                if(previousButton == "Exit"){
                    ShakeUp();
                    previousButton = "Master";
                } else if(previousButton == "SFX"){
                    ShakeDown();
                    previousButton = "Master";
                }
                break;
            case "Exit":
                ResetAllColors();
                ExitButton.color = new Color(0.97f, 0.03f, 0.15f, 1f); // #F80828
                currentButton = "Exit";
                if(previousButton == "Master"){
                    ShakeDown();
                    previousButton = "Exit";
                }
                break;
            default:
                break;
        }
    } 

public void ShakeDown()
{
    switch(currentButton){
        case "CRT":
            LeanTween.moveLocalY(CRTButton.gameObject, CRTButton.transform.localPosition.y - TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            LeanTween.moveLocalY(CRTState1.gameObject, CRTState1.transform.localPosition.y - TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            LeanTween.moveLocalY(CRTState2.gameObject, CRTState2.transform.localPosition.y - TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            break;
        case "Rumble":
            LeanTween.moveLocalY(RumbleButton.gameObject, RumbleButton.transform.localPosition.y - TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            LeanTween.moveLocalY(RumbleState1.gameObject, RumbleState1.transform.localPosition.y - TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            LeanTween.moveLocalY(RumbleState2.gameObject, RumbleState2.transform.localPosition.y - TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            break;
        case "Language":
            LeanTween.moveLocalY(LanguageButton.gameObject, LanguageButton.transform.localPosition.y - TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            LeanTween.moveLocalY(LanguageState1.gameObject, LanguageState1.transform.localPosition.y - TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            LeanTween.moveLocalY(LanguageState2.gameObject, LanguageState2.transform.localPosition.y - TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            break;
        case "Music":
            LeanTween.moveLocalY(MusicButton.gameObject, MusicButton.transform.localPosition.y - TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            break;
        case "SFX":
            LeanTween.moveLocalY(SFXButton.gameObject, SFXButton.transform.localPosition.y - TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            break;
        case "Master":
            LeanTween.moveLocalY(MasterButton.gameObject, MasterButton.transform.localPosition.y - TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            break;
        case "Exit":
            LeanTween.moveLocalY(ExitButton.gameObject, ExitButton.transform.localPosition.y - TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            break;
        default:
            break;
    }
}

public void ShakeUp()
{
    switch(currentButton){
        case "CRT":
            LeanTween.moveLocalY(CRTButton.gameObject, RumbleButton.transform.localPosition.y + TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            LeanTween.moveLocalY(CRTState1.gameObject, RumbleState1.transform.localPosition.y + TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            LeanTween.moveLocalY(CRTState2.gameObject, RumbleState2.transform.localPosition.y + TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            break;
        case "Rumble":
            LeanTween.moveLocalY(RumbleButton.gameObject, RumbleButton.transform.localPosition.y + TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            LeanTween.moveLocalY(RumbleState1.gameObject, RumbleState1.transform.localPosition.y + TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            LeanTween.moveLocalY(RumbleState2.gameObject, RumbleState2.transform.localPosition.y + TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            break;
        case "Language":
            LeanTween.moveLocalY(LanguageButton.gameObject, RumbleButton.transform.localPosition.y + TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            LeanTween.moveLocalY(LanguageState1.gameObject, RumbleState1.transform.localPosition.y + TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            LeanTween.moveLocalY(LanguageState2.gameObject, RumbleState2.transform.localPosition.y + TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            break;
        case "Music":
            LeanTween.moveLocalY(MusicButton.gameObject, MusicButton.transform.localPosition.y + TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            break;
        case "SFX":
            LeanTween.moveLocalY(SFXButton.gameObject, SFXButton.transform.localPosition.y + TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            break;
        case "Master":
            LeanTween.moveLocalY(MasterButton.gameObject, MasterButton.transform.localPosition.y + TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            break;
        case "Exit":
            LeanTween.moveLocalY(ExitButton.gameObject, ExitButton.transform.localPosition.y + TweenForce, TweenDuration)
                .setEase(LeanTweenType.easeInOutQuad)
                .setLoopPingPong(1);
            break;
        default:
            break;
    }
}

    void ResetAllColors(){
        CRTButton.color = new Color(1f, 0.91f, 0.77f, 1f); // #FFE9C5
        CRTState1.color = new Color(1f, 0.91f, 0.77f, 1f); // #FFE9C5
        CRTState2.color = new Color(1f, 0.91f, 0.77f, 1f); // #FFE9C5
        RumbleButton.color = new Color(1f, 0.91f, 0.77f, 1f); // #FFE9C5
        RumbleState1.color = new Color(1f, 0.91f, 0.77f, 1f); // #FFE9C5
        RumbleState2.color = new Color(1f, 0.91f, 0.77f, 1f); // #FFE9C5
        LanguageButton.color = new Color(1f, 0.91f, 0.77f, 1f); // #FFE9C5
        LanguageState1.color = new Color(1f, 0.91f, 0.77f, 1f); // #FFE9C5
        LanguageState2.color = new Color(1f, 0.91f, 0.77f, 1f); // #FFE9C5
        MusicButton.color = new Color(1f, 0.91f, 0.77f, 1f); // #FFE9C5
        SFXButton.color = new Color(1f, 0.91f, 0.77f, 1f); // #FFE9C5
        MasterButton.color = new Color(1f, 0.91f, 0.77f, 1f); // #FFE9C5
        ExitButton.color = new Color(1f, 0.91f, 0.77f, 1f); // #FFE9C5
    }



}
