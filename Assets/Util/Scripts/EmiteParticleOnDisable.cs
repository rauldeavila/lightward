using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmiteParticleOnDisable : MonoBehaviour {

    public GameObject particles;
    
    void OnDisable(){
         if(!this.gameObject.scene.isLoaded) return;
        Instantiate(particles, this.transform.position , Quaternion.identity);
    }    

}
