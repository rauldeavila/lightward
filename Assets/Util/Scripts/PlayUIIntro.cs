using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUIIntro : MonoBehaviour {

    private PlayerController wiz;

    private void Awake(){
        wiz = FindObjectOfType<PlayerController>();
    }

    public void PlayHealthUIIntro(){
        wiz.TriggerHealthUIIntro();
    }

}
