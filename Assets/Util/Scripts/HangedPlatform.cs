using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangedPlatform : MonoBehaviour {

    public float YPositionTreshHold;
    public GameObject Platform;
    public GameObject Particles;
    public GameObject Suspense;
    public GameObject Grounded;
    public string SFXPath;
    private bool _once = false;

    void Start(){
        if(GetComponentInChildren<Breakable>().ScriptableObjectToCheck != null)
        {
            if(GetComponentInChildren<Breakable>().ScriptableObjectToCheck.runTimeValue == true){
                StartGrounded();
            }
        }
    }

    void StartGrounded(){
        Suspense.SetActive(false);
        Grounded.SetActive(true);
    }

    void Update(){
        if(_once == false){
            if(Platform.transform.localPosition.y <= YPositionTreshHold){
                _once = true;
                SFXController.Instance.Play(SFXPath);
                CameraSystem.Instance.ShakeCamera(2);
                ParticleSystem[] particleSystems = Particles.GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem ps in particleSystems) {
                    ps.Play();
                }
            }
        }

    }

}
