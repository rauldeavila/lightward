using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject prefabToRespawn;
    public Transform respawnPoint;

    // Call this method to respawn the object
    public void RespawnObject()
    {
        GameObject newObject = Instantiate(prefabToRespawn, respawnPoint.position, respawnPoint.rotation);
    }

    public void RespawnInTime(float _time)
    {
        Invoke("RespawnObject", _time);
    }
}
