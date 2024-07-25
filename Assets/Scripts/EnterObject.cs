using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterObject : MonoBehaviour
{
    public GameObject[] GameObjectsToToggle;
    public bool DoorClosingSFX = false;
    public bool ToggleAmbienceSounds = false;
    
    void OnEnable()
    {
        if(DoorClosingSFX)
        {
            SFXController.Instance.Play("event:/game/00_game/door_closing");
        }
        if(ToggleAmbienceSounds)
        {
            SFXController.Instance.LowerAmbienceVolume();
        }
        GameState.Instance.InsideBuilding = true;
        DisableGameObjects();
    }

    void OnDisable()
    {
        if(DoorClosingSFX)
        {
            SFXController.Instance.Play("event:/game/00_game/door_closing");
        }
        if(ToggleAmbienceSounds)
        {
            SFXController.Instance.IncreaseAmbienceVolume();
        }
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
