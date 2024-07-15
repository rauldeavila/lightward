using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour {
    public Image[] heartsArray;
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;
    public Sprite yellowHeartSprite;
    public Sprite halfRedHeartSprite;
    public Sprite halfYellowHeartSprite;
    public Sprite insideRedOutsideYellowHeartSprite;
    private Animator animator;

    public static HealthUIController Instance;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
        animator = GetComponent<Animator>();
    }

    private void Update(){
        float currentHealth = PlayerStats.Instance.GetCurrentHealth();
        float maxHealth = PlayerStats.Instance.GetMaxHealth();
        float yellowHealth = PlayerStats.Instance.GetYellowHealth();

        for (int i = 0; i < heartsArray.Length; i++){
            if (i < Mathf.FloorToInt(currentHealth)){
                heartsArray[i].sprite = fullHeartSprite;
            } else if (i == Mathf.FloorToInt(currentHealth) && currentHealth % 1f == 0.5f){
                if (yellowHealth >= 0.5f){
                    heartsArray[i].sprite = insideRedOutsideYellowHeartSprite;
                    yellowHealth -= 0.5f;
                } else{
                    heartsArray[i].sprite = halfRedHeartSprite;
                }
            } else{
                if(yellowHealth == 0.5f){
                    heartsArray[i].sprite = halfYellowHeartSprite;
                    yellowHealth = yellowHealth - 0.5f;
                }  else if(yellowHealth >= 1f){
                    yellowHealth = yellowHealth -1f;
                    heartsArray[i].sprite = yellowHeartSprite;
                } else {
                    heartsArray[i].sprite = emptyHeartSprite;
                }
            }


            if (i < maxHealth){
                heartsArray[i].enabled = true;
            } else{
                heartsArray[i].enabled = false;
            }
        }
    }

    public void UpdateUI(){
        animator.Play("hearts-panel-update");
    }

    public void SetIntroTrigger(){
        animator.SetTrigger("intro");
    }

    public void SetNoIntroTrigger(){
        animator.SetTrigger("no-intro");
    }


}
