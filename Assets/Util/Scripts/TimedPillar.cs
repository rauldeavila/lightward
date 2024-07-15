using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedPillar : MonoBehaviour {

    public float TimeInterval = 3f;  
    public float StartingDelay = 0f;
    public GameObject ParticlesPosition;
    private float _timer;  
    private Animator _animator;
    private GameObject _impactParticles;

    private void Start() {
        _impactParticles = Resources.Load<GameObject>("Particles/ForestTimedPillarImpactParticles");
        _animator = GetComponent<Animator>();
        _timer = TimeInterval;
    }

    private void Update() {
        _timer -= Time.deltaTime;
        if (_timer + StartingDelay <= 0)
        {
            StartingDelay = 0f;
            _animator.SetTrigger("emerge");
            _timer = TimeInterval;
        }
    }

    public void InstantiateImpactParticles(){ // called from animator
        Instantiate(_impactParticles, ParticlesPosition.transform.position, Quaternion.identity);
    }

}
