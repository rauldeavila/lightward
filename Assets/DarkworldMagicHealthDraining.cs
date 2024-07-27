using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkworldMagicHealthDraining : MonoBehaviour
{
    public GameObject MagicObject;
    public GameObject HealthObject;

    void Start()
    {
        DarkworldMagicManager.Instance.OnDarkworldConsumingMagicStarted.AddListener(StartDrainingMagic);
        DarkworldMagicManager.Instance.OnDarkworldConsumingMagicEnded.AddListener(StopDrainingMagic);
        DarkworldMagicManager.Instance.OnDarkworldConsumingHealthStarted.AddListener(StartDrainingHealth);
        DarkworldMagicManager.Instance.OnDarkworldConsumingHealthEnded.AddListener(StopDrainingHealth);
    }

    void StartDrainingMagic()
    {
        MagicObject.SetActive(true);
    }
    void StopDrainingMagic()
    {
        MagicObject.SetActive(false);
    }
    void StartDrainingHealth()
    {
        HealthObject.SetActive(true);
    }
    void StopDrainingHealth()
    {
        HealthObject.SetActive(false);
    }
}
