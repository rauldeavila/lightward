using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizRespawn : MonoBehaviour {

    private PlayerController wiz;

    private void Awake() {
        wiz = FindObjectOfType<PlayerController>();
    }

    public void RespawnWiz1(){
        wiz.SetGravityToOne();
    }

    public void RespawnWiz2(){
        wiz.EnablePlayerAttack();
        wiz.EnablePlayerControls();
        wiz.State.Respawning = false;
    }

}
