using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizLight : MonoBehaviour
{
    public GameObject Zero;
    public GameObject Small;
    public GameObject Mid;
    public GameObject Large;

    public static WizLight Instance;
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
        SetWizLightLevelFromOneToThree(RoomConfigurations.Instance.Data.LightIntensity);
        // Get light level of current room from room script and set light up accordingly.
    }

    public void SetWizLightLevelFromOneToThree(int level)
    {
        switch(level)
        {
            case 0:
                Small.SetActive(false);
                Mid.SetActive(false);
                Large.SetActive(false);
                Zero.SetActive(true);
                break;
            case 1:
                Zero.SetActive(false);
                Small.SetActive(true);
                Mid.SetActive(false);
                Large.SetActive(false);
                break;
            case 2:
                Zero.SetActive(false);
                Small.SetActive(false);
                Mid.SetActive(true);
                Large.SetActive(false);
                break;
            case 3:
                Zero.SetActive(false);
                Small.SetActive(false);
                Mid.SetActive(false);
                Large.SetActive(true);
                break;
        }
    }

}
