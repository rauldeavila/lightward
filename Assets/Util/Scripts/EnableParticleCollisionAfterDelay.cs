using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableParticleCollisionAfterDelay : MonoBehaviour
{
    public float collisionDelay = 2f; // Delay time in seconds before enabling collision

    private float timer = 0f;
    private bool collisionEnabled = false;

    void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var col = ps.collision;
        col.enabled = false;
    }

    void Update()
    {
        if (!collisionEnabled)
        {
            timer += Time.deltaTime;
            if (timer >= collisionDelay)
            {
                ParticleSystem ps = GetComponent<ParticleSystem>();
                var col = ps.collision;
                col.enabled = true;
                collisionEnabled = true;
            }
        }
    }
}
