using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossNameHandler : MonoBehaviour {

    private Animator _animator;
    public TextMeshProUGUI BossName;
    public TextMeshProUGUI Epithet;

    private string _currentAreaText = "";
    private string _textToDisplay = "";
    private float _fadeDuration = 1f;

    public static BossNameHandler Instance;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        }    
        _animator = GetComponent<Animator>();
    }

    public void SetBossName(string name, string epithet){
        BossName.text = name;
        Epithet.text = epithet;
    }

    #region positions
    public void SetNamePositionToMidLeft(){
        _animator.Play("boss_panel_mid_left");
    }
    public void SetNamePositionToMidMid(){
        _animator.Play("boss_panel_mid_left");
    }
    public void SetNamePositionToMidRight(){
        _animator.Play("boss_panel_mid_left");
    }
    public void SetNamePositionToTopLeft(){
        _animator.Play("boss_panel_top_left");
    }
    public void SetNamePositionToTopMid(){
        _animator.Play("boss_panel_top_left");
    }
    public void SetNamePositionToTopRight(){
        _animator.Play("boss_panel_top_left");
    }
    public void SetNamePositionToBottomLeft(){
        _animator.Play("boss_panel_bottom_left");
    }
    public void SetNamePositionToBottomMid(){
        _animator.Play("boss_panel_bottom_left");
    }
    public void SetNamePositionToBottomRight(){
        _animator.Play("boss_panel_bottom_left");
    }
    #endregion

    public void ShowBossName(){
        StartCoroutine(FadeIn(BossName, _fadeDuration));
        StartCoroutine(FadeIn(Epithet, _fadeDuration));
        Invoke("HideBossName", 2f);
    }

    public void HideBossName(){
        StartCoroutine(FadeOut(BossName, _fadeDuration));
        StartCoroutine(FadeOut(Epithet, _fadeDuration));
    }

    private IEnumerator FadeIn(TextMeshProUGUI textMesh, float duration){
        Color targetColor = new Color32(248, 8, 40, 255); // #F80828
        Color startColor = new Color(targetColor.r, targetColor.g, targetColor.b, 0f);
        float timeElapsed = 0f;

        while (timeElapsed < duration){
            float t = timeElapsed / duration;
            textMesh.color = Color.Lerp(startColor, targetColor, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        textMesh.color = targetColor;
    }

    private IEnumerator FadeOut(TextMeshProUGUI textMesh, float duration){
        Color startColor = new Color32(248, 8, 40, 255); // #F80828
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        float timeElapsed = 0f;

        while (timeElapsed < duration){
            float t = timeElapsed / duration;
            textMesh.color = Color.Lerp(startColor, targetColor, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        textMesh.color = targetColor;
    }


}
