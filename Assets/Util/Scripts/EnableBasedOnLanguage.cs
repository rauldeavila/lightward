using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBasedOnLanguage : MonoBehaviour {

    public GameObject en;
    public GameObject ptbr;

    void Update(){
        if(PlayerPrefs.GetInt("Language", 0) == 0){
            if(ptbr!=null){
                ptbr.SetActive(false);  
            }
            if(en!=null){
                en.SetActive(true);
            }
        } else if(PlayerPrefs.GetInt("Language", 0) == 1){
            if(ptbr!=null){
                ptbr.SetActive(true);
            }
            if(en!=null){
                en.SetActive(false);
            }
        }
    }

}
