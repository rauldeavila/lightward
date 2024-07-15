using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossNameOnUI : MonoBehaviour {

    public float posX;
    public float posY;
    public TextMeshProUGUI bossName;

    public float GreenoXPos;
    public float GreenoYPos;

    private Animator animator;

    private void Awake(){
        animator = GetComponent<Animator>();
    }

    public void ShowBossNameOnUI(string name){
        if(name == "Greeno"){
            bossName.text = "Greeno";
            GetComponent<RectTransform>().anchoredPosition = new Vector2(GreenoXPos, GreenoYPos);
            animator.SetTrigger("show");
        }
    }

}
