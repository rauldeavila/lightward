using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterObject : MonoBehaviour
{
    public GameObject[] GameObjectsToToggle;
    
    void OnEnable()
    {
        DisableGameObjects();
    }

    void OnDisable()
    {
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
    public void ToggleActive()
    {
        GameState.Instance.InsideBuilding = !GameState.Instance.InsideBuilding;
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }
}
