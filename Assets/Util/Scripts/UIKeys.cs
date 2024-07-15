using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIKeys : MonoBehaviour {

    public GameObject key1;
    public GameObject key2;
    public GameObject key3;
    public GameObject key4;

    void Awake(){
        key1.SetActive(false);
        key2.SetActive(false);
        key3.SetActive(false);
        key4.SetActive(false); 
    }

    void Update(){
        if (PlayerStats.Instance.keys.runTimeValue == 0){
            key1.SetActive(false);
            key2.SetActive(false);
            key3.SetActive(false);
            key4.SetActive(false);
        } else if (PlayerStats.Instance.keys.runTimeValue == 1){
            key1.SetActive(true);
            key2.SetActive(false);
            key3.SetActive(false);
            key4.SetActive(false);
        } else if (PlayerStats.Instance.keys.runTimeValue == 2){
            key1.SetActive(true);
            key2.SetActive(true);
            key3.SetActive(false);
            key4.SetActive(false);
        } else if (PlayerStats.Instance.keys.runTimeValue == 3){
            key1.SetActive(true);
            key2.SetActive(true);
            key3.SetActive(true);
            key4.SetActive(false);
        } else if(PlayerStats.Instance.keys.runTimeValue == 4){
            key1.SetActive(true);
            key2.SetActive(true);
            key3.SetActive(true);
            key4.SetActive(true);
        }
    }

}
