using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManagerTwo : MonoBehaviour {

    public GameObject TutDodge;
    private Coroutine _dodgeCoroutine;
    private bool _dodgeFlag = false;
    private bool _shownDodgeTut = false;

    public void ShowDodgeTutIn10Sec(){
        _dodgeCoroutine = StartCoroutine(ShowDodgeTutCoroutine());
    }

    IEnumerator ShowDodgeTutCoroutine(){
        yield return new WaitForSeconds(10f);
        TutDodge.SetActive(true);
        LogSystem.Instance.AppendLog("tutorial_dodge: true");
        _shownDodgeTut = true;
    }

    public void HideDodgeTut(){
        if(TutDodge.activeInHierarchy){
            TutDodge.SetActive(false);
        } else {
            if(!_dodgeFlag){
                _dodgeFlag = true;
                StopCoroutine(_dodgeCoroutine);
                if(!_shownDodgeTut){
                    LogSystem.Instance.AppendLog("tutorial_dodge: false");
                }
            }
        }
    }

}
