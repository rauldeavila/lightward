using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomConfigurations : MonoBehaviour 
{
    public RoomData Data;
    public static RoomConfigurations Instance;
    void Awake()
    {
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        }
    }

    void Start()
    {
        if(Data == null)
        {
            Debug.LogError("NO ROOM DATA ASSSIGNED TO THIS ROOM. FIX THIS.");
        }

        SFXController.Instance.SetWindVolume(Data.SFXWindVolume);
        SFXController.Instance.SetGardensVolume(Data.SFXGardensVolume);
        SFXController.Instance.SetThunderstormVolume(Data.SFXThunderstormVolume);
    }

    public string GetProfileName()
    {
        return Data.AreaName;
    }

}
