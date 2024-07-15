using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossBearSprite : MonoBehaviour {

    private DOTweenAnimation dotweenAnimation;
    
    public GameObject LandingParticlesPosition;
    public GameObject AttackParticlesPosition;
    public GameObject AttackParticlesLeftPosition;
    public GameObject RunParticlesPosition;
    public GameObject DeflectProjectilesLeft;
    public GameObject DeflectProjectilesRight;
    private GameObject LandingParticles;
    private GameObject AttackStrongParticles;
    private GameObject RunStrongParticles;
    private Enemy _enemy;

    public static BossBearSprite Instance;

    void Awake(){

        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        }    
        _enemy = GetComponentInParent<Enemy>();
        LandingParticles = Resources.Load<GameObject>("Particles/bear_landing_particles");
        AttackStrongParticles = Resources.Load<GameObject>("Particles/bear_atk_strong_particles");
        RunStrongParticles = Resources.Load<GameObject>("Particles/bear_run_angry_particles");


    }

    // void Start() {
    //     dotweenAnimation = GetComponent<DOTweenAnimation>();
    // }

    public void PlayTween(){
        DOTween.Play("tackle1");
        // dotweenAnimation.Play("tackle1");
    }
    public void PlayTween2(){
        DOTween.Play("tackle2");
        // dotweenAnimation.Play("tackle2");
    }

    public void AddTackleForce(){
        BossBear.Instance.TackleForce();
    }

    public void Stop(){
        BossBear.Instance.FreezeMovement();
    }


    public void ShakeCamSoft(){
        CameraSystem.Instance.ShakeCamera(0);
    }

    public void ShakeCamHard(){
        CameraSystem.Instance.ShakeCamera(2);
    }

    public void InstantiateDumbFlyerRandomly(){
        BossBear.Instance.SpawnRandomDumbFlyer();
    }

    public void PlayLandingParticles(){
        Instantiate(LandingParticles, LandingParticlesPosition.transform.position, Quaternion.identity);
    }
    public void PlayAttackStrongParticles(){
        Instantiate(AttackStrongParticles, AttackParticlesPosition.transform.position, Quaternion.identity);
    }
    public void PlayAttackStrongLeftParticles(){
        Instantiate(AttackStrongParticles, AttackParticlesLeftPosition.transform.position, Quaternion.identity);
    }
    public void PlayRunStrongParticles(){
        Instantiate(RunStrongParticles, RunParticlesPosition.transform.position, Quaternion.identity);
    }

    public void PlayFootstep(){
        SFXController.Instance.Play3D("event:/game/01_forest/bear_step", this.transform.position);
    }

    public void PlayGrowl(){
        SFXController.Instance.Play3D("event:/game/01_forest/bear_growl", this.transform.position);
    }

    public void PlaySleep(){
        SFXController.Instance.Play3D("event:/game/01_forest/bear_sleeping", this.transform.position);
    }

    public void StopSleep(){
        SFXController.Instance.Stop3D("event:/game/01_forest/bear_sleeping");
    }

    public void PlayBubblePop(){
        SFXController.Instance.Play("event:/game/01_forest/bubble_pop");
    }

    public void PlayTackle(){
        SFXController.Instance.Play3D("event:/game/01_forest/bear_tackle", this.transform.position);
    }

    public void PlayFallToGround(){
        SFXController.Instance.Play("event:/game/01_forest/ground_slam");
    }

    public void PlaySwoosh(){
        SFXController.Instance.Play3D("event:/game/01_forest/big_swoosh", this.transform.position);
    }
    public void PlayHitImpact(){
        SFXController.Instance.Play3D("event:/game/01_forest/big_impact_grass", this.transform.position);
    }

    public void ShowBossBearName(){
        BossNameHandler.Instance.SetNamePositionToMidLeft();
        BossNameHandler.Instance.SetBossName("Petalpaw", "The flowering grizzly");
        BossNameHandler.Instance.ShowBossName();
    }

    public void EnableDeflectProjectilesTriggers(){
        if(_enemy.FacingRight){
            DeflectProjectilesRight.SetActive(true);
        } else {
            DeflectProjectilesLeft.SetActive(true);
        }
    }

    public void DisableDeflectProjectilesTriggers(){
        DeflectProjectilesLeft.SetActive(false);
        DeflectProjectilesRight.SetActive(false);
    }


}
