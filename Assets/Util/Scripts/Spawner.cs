using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject ObjToInstantiate;
    public float Rate;
    public bool RespawnWhenLastDestroyed;

    void Start(){
        if (RespawnWhenLastDestroyed) {
            StartCoroutine(SpawnLoopWhenDestroyed());
        } else {
            StartCoroutine(SpawnLoopWithRate());
        }
    }

    IEnumerator SpawnLoopWithRate(){
        while (true) {
            yield return new WaitForSeconds(Rate);
            Instantiate(ObjToInstantiate, transform.position, Quaternion.identity);
        }
    }

    IEnumerator SpawnLoopWhenDestroyed(){
        while (true) {
            GameObject spawnedObject = Instantiate(ObjToInstantiate, transform.position, Quaternion.identity);
            yield return new WaitUntil(() => spawnedObject == null);
        }
    }

}
