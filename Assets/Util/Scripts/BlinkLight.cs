using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkLight : MonoBehaviour {
    
    
    // private LightingSystem _LS;
    // private Renderer _spriteToChangeMaterial;
    // private Material _defaultMaterial;
    // private Material _offMaterial;
    // private float _minOnTime;
    // private float _maxOnTime;
    // private float _minOffTime;
    // private float _maxOffTime;
    
    // private bool _flag = true;
    // private bool _blinking = true;

    // private void Awake(){
    //     _LS = GetComponentInParent<LightingSystem>();
    // }
    
    
    // private void Start(){
    //     _spriteToChangeMaterial = _LS.GetComponent<SpriteRenderer>();
    //     _defaultMaterial = _LS.DefaultMaterial;
    //     _offMaterial = _LS.OffMaterial;
    //     _minOnTime = _LS.MinOnTime;
    //     _maxOnTime = _LS.MaxOnTime;
    //     _minOffTime = _LS.MinOffTime;
    //     _maxOffTime = _LS.MaxOffTime;
        
    //     _blinking = false;
    //     EnableBlinking();
    // }
    
    
    // private void BlinkTheLight(){
    //     if(_LS.ShouldBlink){
    //         if(_blinking){
    //             if (_flag) {
    //                 _spriteToChangeMaterial.material = _defaultMaterial;
    //                 _flag = false;
    //                 Invoke("BlinkTheLight", Random.Range(_minOnTime, _maxOnTime));
    //             } else {
    //                 _spriteToChangeMaterial.material = _offMaterial;
    //                 _flag = true;
    //                 Invoke("BlinkTheLight", Random.Range(_minOffTime, _maxOffTime));
    //             }  
    //         }            
    //     } else {
    //         DisableBlinking();
    //     }
    // }
    
    // public void DisableBlinking(){
    //     CancelInvoke();
    //     _blinking = false;
    // }
    
    // public void EnableBlinking(){
    //     if(_LS.ShouldBlink){
    //         if(_blinking == false){
    //             _blinking = true;
    //             BlinkTheLight();            
    //         }            
    //     }
    // }
    
    // public void DisableBlinkingAndTurnTheLightOn(){
    //     _LS.ShouldBlink = false;
    //     _spriteToChangeMaterial.material = _defaultMaterial;
    //     _flag = false;
    // }
    
    // public void ReenableBlinking(){
    //     _LS.ShouldBlink = true;
    //     EnableBlinking();
    // }
    
    
    

}
