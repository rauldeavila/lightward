using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectSpawner : MonoBehaviour {

    public GameObject ObjectToSpawn;
    public bool SpawnMultipleTimes = false;
    public float TimeBetweenSpawns = 1.0f;
    public int NumSpawns = 0;
    public float DelayToStart = 0f;
    
    private int _spawnCount = 0;
    private float _spawnTimer = 0f;



    void FixedUpdate() {
        if(DelayToStart > 0f){
            DelayToStart -= Time.deltaTime;
        } else {
            if (!SpawnMultipleTimes) {
                // Spawn object once
                Instantiate(ObjectToSpawn, transform.position, Quaternion.identity);
            } else {
                if(NumSpawns == 0){
                    _spawnTimer += Time.deltaTime;
                    if (_spawnTimer >= TimeBetweenSpawns) {
                        Instantiate(ObjectToSpawn, transform.position, Quaternion.identity);
                        _spawnCount++;
                        _spawnTimer = 0.0f;
                    }
                } else {
                    if (_spawnCount < NumSpawns) {
                        _spawnTimer += Time.deltaTime;
                        if (_spawnTimer >= TimeBetweenSpawns) {
                            Instantiate(ObjectToSpawn, transform.position, Quaternion.identity);
                            _spawnCount++;
                            _spawnTimer = 0.0f;
                        }
                    }
                }
            }
        }
    }

}
