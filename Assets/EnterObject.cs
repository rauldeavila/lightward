using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterObject : MonoBehaviour
{
    public GameObject[] GameObjectsToToggle;
    
    void OnEnable()
    {
        GameState.Instance.InsideBuilding = true;
        DisableGameObjects();
    }

    void OnDisable()
    {
        GameState.Instance.InsideBuilding = false;
        EnableGameObjects();
    }


    public void DisableGameObjects()
    {
        foreach(GameObject obj in GameObjectsToToggle)
        {
            obj.SetActive(false);
        }
    }

    public void EnableGameObjects()
    {
        foreach(GameObject obj in GameObjectsToToggle)
        {
            obj.SetActive(true);
        }
    }
    
}
