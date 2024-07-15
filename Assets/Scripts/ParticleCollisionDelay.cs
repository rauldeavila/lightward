using System;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionDelay : MonoBehaviour
{

    private ParticleSystem _ps;

    void Start(){
        _ps = GetComponent<ParticleSystem>();
        var coll = _ps.collision;
        coll.enabled = false; // Initially disable collision
        Invoke("EnableCol", 1f);
    }
    void EnableCol()
    {        
        var coll = _ps.collision;
        coll.enabled = true; // Enable collision after 1 second
    }

}
