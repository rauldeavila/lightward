using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LightingSystem : MonoBehaviour {

    // public bool Darkness = false;

    // [ShowIf("Darkness")]
    // public float DarknessSize;
    
    // [ColorPalette("Wiz")]
    // public Color BaseColor;
    // [ColorPalette("Wiz")]
    // public Color WizInsideColor;
    // public float TilesIntensity;
    // public float RestIntensity;
    // private float _restIntensityDefaults;
    // public float TilesIntensityWiz;
    // public float RestIntensityWiz;
    // public bool ShouldBlink;
    // [ShowIf("ShouldBlink")]
    // public float MinOnTime;
    // [ShowIf("ShouldBlink")]
    // public float MaxOnTime;
    // [ShowIf("ShouldBlink")]
    // public float MinOffTime;
    // [ShowIf("ShouldBlink")]
    // public float MaxOffTime;
    // [ShowIf("ShouldBlink")]
    // public Material OffMaterial;
    
    // public Material DefaultMaterial;
    // public Material WizInsideMaterial;
    
    // private GameObject TargetLightParticles;
    // private RealLightTiles _realLightTiles;
    // private RealLightRest _realLightRest;
    // public Renderer _mRenderer;
    // private GameObject _targetParticles = null;
    // private bool _canPlayParticles = true;
    
    // private UnityEngine.Rendering.Universal.Light2D _tilesLight;
    // private UnityEngine.Rendering.Universal.Light2D _restLight;
    
    
    // private BlinkLight _blinkScript;
    // private DashingLightReceiver _dashingLightScript;
    
    // [HideInInspector]
    // public bool OriginalBlinker;
    
    // private void OnValidate() {
    //     OnEnable();
    //     UpdateSettings();
    // }

    // void OnEnable() {
    //     TargetLightParticles = Resources.Load<GameObject>("Particles/TargetLightParticles");
    //     if(ShouldBlink){
    //         OriginalBlinker = true;
    //     } else {
    //         OriginalBlinker = false;
    //     }
        
    //     _blinkScript = GetComponentInChildren<BlinkLight>();
    //     _dashingLightScript = GetComponentInChildren<DashingLightReceiver>();

    //     _realLightTiles = GetComponentInChildren<RealLightTiles>();
    //     _realLightRest = GetComponentInChildren<RealLightRest>();

    //     if (_realLightTiles != null)
    //         _tilesLight = _realLightTiles.GetComponent<UnityEngine.Rendering.Universal.Light2D>();

    //     if (_realLightRest != null)
    //         _restLight = _realLightRest.GetComponent<UnityEngine.Rendering.Universal.Light2D>();


    //     UpdateSettings();
    // }
    
    // void UpdateSettings(){
    //     if(_restLight != null && _tilesLight != null){
    //         if(Darkness){
    //             _tilesLight.pointLightOuterRadius = DarknessSize;
    //         } else {
    //             _tilesLight.color = BaseColor;
    //             _restLight.color = BaseColor;
    //             _tilesLight.intensity = TilesIntensity;
    //             _restLight.intensity = RestIntensity;
    //             _restIntensityDefaults = RestIntensity;
    //         }
    //     }
    // }

    // public void WizOutsideColors(){
    //     if(_restLight != null && _tilesLight != null){
    //         _tilesLight.color = BaseColor;
    //         _restLight.color = BaseColor;
    //         _tilesLight.intensity = TilesIntensity;
    //         _restLight.intensity = RestIntensity;
    //     }
    // }

    // public void WizInsideColors(){
    //     if(_restLight != null && _tilesLight != null){
    //         _tilesLight.color = WizInsideColor;
    //         _restLight.color = WizInsideColor;
    //         _tilesLight.intensity = TilesIntensityWiz;
    //         _restLight.intensity = RestIntensityWiz;
    //     }
    // }
    
    // public void DisableBlinking(){
    //     _blinkScript.DisableBlinkingAndTurnTheLightOn();
    // }
    
    // public void EnableBlinking(){
    //     _blinkScript.ReenableBlinking();
    // }
    
    // public void SetLightAsTarget(){
    //     float distanceToPlayer = Vector3.Distance(PlayerController.Instance.transform.position,this.transform.position);
    //     if(_canPlayParticles && distanceToPlayer > 2f){
    //         _canPlayParticles = false;
    //         Invoke("EnableParticles", 0.2f);
    //         _targetParticles = Instantiate(TargetLightParticles, this.transform.position, Quaternion.identity);
    //     }
    //     if(_restLight != null){
    //         _restLight.intensity = RestIntensity * 1.3f;
    //     }
    // }

    // void EnableParticles(){
    //     _canPlayParticles = true;
    // }

    // public void ResetLightTarget(){
    //     if(_targetParticles) _targetParticles.GetComponent<ParticleSystem>().Stop();
    //     _targetParticles = null;
    //     if(_restLight != null){
    //         _restLight.intensity = _restIntensityDefaults;
    //     }
    // }
    
    

}
