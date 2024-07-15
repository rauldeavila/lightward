using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateParticlesOnContact : MonoBehaviour
{
    public bool Dirt = false;
    public bool Stone = false;
    private GameObject explosionPrefab;

    private void Start()
    {
        // Load the explosion prefab from Resources/Particles folder
        explosionPrefab = Resources.Load("Particles/LaserImpact") as GameObject;
    }

    void GenerateDirtParticles()
    {
        // print("Generating Dirt Particles!");
    }

    void GenerateStoneParticles()
    {
        // print("Generating Stone Particles!");
    }
    
    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.CompareTag("Laser"))
        {
            if(explosionPrefab){
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }
            if(Dirt)
            {
                GenerateDirtParticles();
            } else if(Stone)
            {
                GenerateStoneParticles();
            }
        } 
    }
}
