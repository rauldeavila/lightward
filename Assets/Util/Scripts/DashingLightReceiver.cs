using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LightAnimation))]
public class DashingLightReceiver : MonoBehaviour {
    Coroutine OneSecondCoroutine = null;
    private bool _canEnterLight = true;
    private bool _once = true;
    public bool Inside = false;
    private DashingLight dashingLight;
    private LightAnimation _lightAnimation;
    private ParticleSystem _enteringLightParticles;

    void OnEnable()
    {        
        dashingLight = FindObjectOfType<DashingLight>();
        _lightAnimation = GetComponent<LightAnimation>();
        _enteringLightParticles = Resources.Load<ParticleSystem>("Particles/EnteringLightParticles");
    }

    public void WizInside() // DashingLightTrigger.Cs calls this 
    {
        if(_canEnterLight){
            _canEnterLight = false;
            ReenableEnteringLights();
            if(!Inside){
                HandleWizInside();
            }
            Invoke("DisableCooldown", 0.1f);
        }
    }

    private void HandleWizInside(){
        Instantiate(_enteringLightParticles, transform.position, Quaternion.identity);
        FMODUnity.RuntimeManager.PlayOneShot("event:/char/wiz/entering_light", transform.position);
        _lightAnimation.UpdateWizInside(true);
        Inside = true;
        OneSecondCoroutine = StartCoroutine(OneSecond());
    }

    IEnumerator OneSecond(){
        yield return new WaitForSecondsRealtime(0.1f);
        DropWizWithLongerCoolDown();
    }

    // CHAMADO NO DASHINGLIGHTTRIGGER.CS
    public void WizOutside(){
        _lightAnimation.UpdateWizInside(false);
        Inside = false;
        StopAllCoroutines();
        _once = true;
    }

    public void DropWiz(){ // JUMPED OUT OF LIGHT
        dashingLight.DropFromLight();
        Invoke("DisableCooldown", 0.1f);
    }

    public void DropWizWithLongerCoolDown() // KICKED OUT OF LIGHT
    {
        dashingLight.KickedOutOfLight = true;
        dashingLight.DropFromLight();
        Invoke("DisableCooldown", 0.3f);
    }

    private void DisableCooldown(){
        if(dashingLight.Cooldown == true){
            dashingLight.DisableCooldown();
        }
    }

    private void ReenableEnteringLights(){
        Invoke("ToggleBool", 0.3f);
    }

    private void ToggleBool(){
        _canEnterLight = true;
    }


}
