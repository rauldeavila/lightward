using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private BossNameOnUI bossNameOnUIScript;
    public string bossName;

    #region Shared Components
    public Rigidbody2D Rigidbody2D { get => GetComponentInParent<Rigidbody2D>(); }
    public Animator Animator { get => GetComponentInParent<Animator>(); }
    public EnemyState State { get => GetComponent<EnemyState>(); }
    #endregion

    private void Awake(){
        bossNameOnUIScript = FindObjectOfType<BossNameOnUI>();
    }

    public void DazeEnemy(){
        Animator.SetTrigger("dazed");
    }

    public void KillEnemy(){
        Animator.SetTrigger("dead");
    }

    public void FreezeRigidbody(){
        Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void UnfreezeRigidbody(){
        Rigidbody2D.constraints = RigidbodyConstraints2D.None;
        Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void EnableEnemyToTakeDamage(){
        State.CanTakeDamage = true;
    }

    public void CamBreak(){
        CameraSystem.Instance.ShakeCamera(1);
    }

    public void ShowBossName(){
        if(bossName != null){
            bossNameOnUIScript.ShowBossNameOnUI(bossName);
        }
    }
}


