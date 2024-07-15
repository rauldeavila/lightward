using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameObjectActive : MonoBehaviour {


    public bool WhenWizEntersTrigger;
    public bool WhenWizLeftsTrigger;
    public bool DisableWhenLeavingTrigger;

    public GameObject object1; 
    public GameObject object2; 
    public GameObject object3; 
    public GameObject object4; 
    public GameObject object5;

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("WizHitBox")){
            if(WhenWizEntersTrigger){
                if(object1 != null){
                    object1.SetActive(true);
                }
                if(object2 != null){
                    object2.SetActive(true);
                }
                if(object3 != null){
                    object3.SetActive(true);
                }
                if(object4 != null){
                    object4.SetActive(true);
                }
                if(object5 != null){
                    object5.SetActive(true);
                }
            }
        }
    } 

    private void OnTriggerExit2D(Collider2D collider){
        if(collider.CompareTag("WizHitBox")){
            if(WhenWizLeftsTrigger){
                if(object1 != null){
                    object1.SetActive(true);
                }
                if(object2 != null){
                    object2.SetActive(true);
                }
                if(object3 != null){
                    object3.SetActive(true);
                }
                if(object4 != null){
                    object4.SetActive(true);
                }
                if(object5 != null){
                    object5.SetActive(true);
                }
            }

            if(DisableWhenLeavingTrigger){
                if(object1 != null){
                    object1.SetActive(false);
                }
                if(object2 != null){
                    object2.SetActive(false);
                }
                if(object3 != null){
                    object3.SetActive(false);
                }
                if(object4 != null){
                    object4.SetActive(false);
                }
                if(object5 != null){
                    object5.SetActive(false);
                }

            }
        }
    } 

}
