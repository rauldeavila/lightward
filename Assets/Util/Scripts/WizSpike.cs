using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizSpike : MonoBehaviour {

    public void SpikeWikz(){
        PlayerState.Instance.Respawning = true;
        PlayerController.Instance.DisablePlayerAttack();
        PlayerController.Instance.DisablePlayerControls();
        StartCoroutine(StopWiz());
    }

    IEnumerator StopWiz(){
        yield return new WaitForSeconds(0.2f);
        PlayerController.Instance.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 1f);
        PlayerController.Instance.SetGravityToZero();
    }


}
