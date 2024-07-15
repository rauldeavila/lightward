using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizSpike : MonoBehaviour {

    private PlayerController wiz;

    private void Awake() {
        wiz = FindObjectOfType<PlayerController>();
    }

    public void SpikeWikz(){
        wiz.State.Respawning = true;
        wiz.DisablePlayerAttack();
        wiz.DisablePlayerControls();
        StartCoroutine(StopWiz());
    }

    IEnumerator StopWiz(){
        yield return new WaitForSeconds(0.2f);
        wiz.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 1f);
        wiz.SetGravityToZero();
    }


}
