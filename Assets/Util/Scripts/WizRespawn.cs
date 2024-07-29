using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizRespawn : MonoBehaviour {

    public void RespawnWiz1(){
        PlayerController.Instance.SetGravityToOne();
    }

    public void RespawnWiz2(){
        Invoke("EnsureIt", 0.3f);
        PlayerController.Instance.EnablePlayerAttack();
        PlayerController.Instance.EnablePlayerControls();
        PlayerController.Instance.State.Respawning = false;
    }

    void EnsureIt()
    {
        PlayerController.Instance.EnablePlayerAttack();
        PlayerController.Instance.EnablePlayerControls();
        PlayerController.Instance.State.Respawning = false;
    }

}
