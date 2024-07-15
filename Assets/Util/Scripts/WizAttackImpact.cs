using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizAttackImpact : MonoBehaviour {

    public GameObject PrefabImpactLeft;
    public GameObject PrefabImpactRight;
    public GameObject PrefabImpactDown;

    // ------------------------ \/ front ---------
    public GameObject Pos1;
    public GameObject Pos2;
    public GameObject Pos3; // priority (farther)
    public GameObject Pos4;
    public GameObject Pos5;
    // ------------------------ \/ down ---------
    public GameObject Pos6;
    public GameObject Pos7;
    public GameObject Pos8; // priority (farther)
    public GameObject Pos9;
    public GameObject Pos10;
    // ------------------------- \/ up ---------
    public GameObject Pos11;
    public GameObject Pos12;
    public GameObject Pos13; // priority (farther)
    public GameObject Pos14;
    public GameObject Pos15;

    private PlayerState _state;

    public static WizAttackImpact Instance;

    void Awake(){
        // singleton
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
        _state = FindObjectOfType<PlayerState>();
    }

    // Called on Attack
    public void CheckImpactPositionAndGenerate(int attackType, Vector3? position = null){ // attackType = 1 -> front attack    // attackType = 2 -> down attack // attackType = 3 -> up attack
        if (position == null) {
            if(attackType == 1){
                if(Pos1.GetComponent<ImpactPositionListener>().active){
                    if(_state.FacingRight){
                        Instantiate(PrefabImpactRight, Pos1.transform.position , Quaternion.identity);
                    } else{
                        Instantiate(PrefabImpactLeft, Pos1.transform.position , Quaternion.identity);
                    }
                } else if(Pos5.GetComponent<ImpactPositionListener>().active){
                    if(_state.FacingRight){
                        Instantiate(PrefabImpactRight, Pos5.transform.position , Quaternion.identity);
                    } else{
                        Instantiate(PrefabImpactLeft, Pos5.transform.position , Quaternion.identity);
                    }
                } else if(Pos4.GetComponent<ImpactPositionListener>().active){
                    if(_state.FacingRight){
                        Instantiate(PrefabImpactRight, Pos4.transform.position , Quaternion.identity);
                    } else{
                        Instantiate(PrefabImpactLeft, Pos4.transform.position , Quaternion.identity);
                    }
                } else if(Pos2.GetComponent<ImpactPositionListener>().active){
                    if(_state.FacingRight){
                        Instantiate(PrefabImpactRight, Pos2.transform.position , Quaternion.identity);
                    } else{
                        Instantiate(PrefabImpactLeft, Pos2.transform.position , Quaternion.identity);
                    }
                } else if(Pos3.GetComponent<ImpactPositionListener>().active){
                    if(_state.FacingRight){
                        Instantiate(PrefabImpactRight, Pos3.transform.position , Quaternion.identity);
                    } else{
                        Instantiate(PrefabImpactLeft, Pos3.transform.position , Quaternion.identity);
                    }
                }
            } else if(attackType == 2){
                if(Pos8.GetComponent<ImpactPositionListener>().active){
                    Instantiate(PrefabImpactDown, Pos8.transform.position , Quaternion.identity);
                } else if(Pos7.GetComponent<ImpactPositionListener>().active){
                    Instantiate(PrefabImpactDown, Pos7.transform.position , Quaternion.identity);
                } else if(Pos9.GetComponent<ImpactPositionListener>().active){
                    Instantiate(PrefabImpactDown, Pos9.transform.position , Quaternion.identity);
                } else if(Pos6.GetComponent<ImpactPositionListener>().active){
                    Instantiate(PrefabImpactDown, Pos6.transform.position , Quaternion.identity);
                } else if(Pos10.GetComponent<ImpactPositionListener>().active){
                    Instantiate(PrefabImpactDown, Pos10.transform.position , Quaternion.identity);
                }
            } else if(attackType == 3){
                if(Pos13.GetComponent<ImpactPositionListener>().active){
                    Instantiate(PrefabImpactDown, Pos13.transform.position , Quaternion.identity);
                } else if(Pos12.GetComponent<ImpactPositionListener>().active){
                    Instantiate(PrefabImpactDown, Pos12.transform.position , Quaternion.identity);
                } else if(Pos14.GetComponent<ImpactPositionListener>().active){
                    Instantiate(PrefabImpactDown, Pos14.transform.position , Quaternion.identity);
                } else if(Pos11.GetComponent<ImpactPositionListener>().active){
                    Instantiate(PrefabImpactDown, Pos11.transform.position , Quaternion.identity);
                } else if(Pos15.GetComponent<ImpactPositionListener>().active){
                    Instantiate(PrefabImpactDown, Pos15.transform.position , Quaternion.identity);
                }
            }
        } else {
            if(attackType == 1){
                if(_state.FacingRight){
                    Instantiate(PrefabImpactRight, (Vector3)position , Quaternion.identity);
                } else{
                    Instantiate(PrefabImpactLeft, (Vector3)position , Quaternion.identity);
                }
            } else if(attackType == 2){
                Instantiate(PrefabImpactDown, (Vector3)position , Quaternion.identity);
            } else if(attackType == 3){
                Instantiate(PrefabImpactDown, (Vector3)position , Quaternion.identity);
            }
        }
        
    }


}
