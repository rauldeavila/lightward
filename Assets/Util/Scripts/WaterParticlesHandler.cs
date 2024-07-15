using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticlesHandler : MonoBehaviour {

    public GameObject waterJumpPrefab;
    public GameObject waterJumpALTPrefab;
    public GameObject waterLandingPrefab;
    public GameObject waterLandingALTPrefab;
    public Transform waterPosition;
    public Transform waterLandPosition;

    public GameObject WaterFXON;
    public GameObject WaterFXOFF;
    private PlayerState _state;

    public static WaterParticlesHandler Instance;


    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
        _state = FindObjectOfType<PlayerState>();
    }

    void Update(){
        if(_state.OnWater){
            WaterFXOFF.SetActive(false);
            WaterFXON.SetActive(true);
        } else{
            WaterFXON.SetActive(false);
            WaterFXOFF.SetActive(true);
        }
    }

    public void PlayWaterJumpParticles(){
        Instantiate(waterJumpPrefab, waterPosition.position, Quaternion.identity);
        Instantiate(waterJumpALTPrefab, waterPosition.position, Quaternion.identity);
    }

    public void PlayWaterLandParticles(){
        Instantiate(waterLandingPrefab, waterLandPosition.position, Quaternion.identity);
        Instantiate(waterLandingALTPrefab, waterLandPosition.position, Quaternion.identity);
        Instantiate(waterJumpALTPrefab, waterPosition.position, Quaternion.identity);
    }
}
