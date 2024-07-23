using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderstormManager : MonoBehaviour
{
    // Uses RoomConfigurations to load from room data scriptable object
    public event Action<float> OnLightningStrike;

    void Start()
    {
        RoomConfigurations.OnRoomChanged.AddListener(UpdateThunderstormStatus);
        UpdateThunderstormStatus();
    }

    void UpdateThunderstormStatus()
    {
        if (RoomConfigurations.CurrentRoom != null)
        {
            if (RoomConfigurations.CurrentRoom.Data.ThunderstormEffect)
            {
                print("A thunderstorm started.");
                ScheduleLightning(0.5f, 2f);
            }
            else
            {
                CancelLightning();
            }
        }
    }

    void ScheduleLightning(float minWait, float maxWait)
    {
        float delay = UnityEngine.Random.Range(minWait, maxWait);
        Invoke("StrikeLightning", delay);
    }

    void StrikeLightning()
    {
        float thunderDuration = UnityEngine.Random.Range(0.2f, 1f);

        if (OnLightningStrike != null)
        {
            SometimesPlayLightningStrikeSound(thunderDuration);
            OnLightningStrike(thunderDuration);
        }

        ScheduleLightning(3f, 7f);
    }

    void CancelLightning()
    {
        CancelInvoke("StrikeLightning");
        // print("Clear skies.");
    }

    void SometimesPlayLightningStrikeSound(float thunderDuration)
    {
        if (thunderDuration >= 0.9f)
        {
            SFXController.Instance.Play("event:/game/02_graveyard/lightning_strike_strong");
            return;
        }
        else
        {
            int rng = UnityEngine.Random.Range(1, 11); // from 1...10
            if (rng > 7)
            { // 30% chance of playing lightning sound
                int strong = UnityEngine.Random.Range(1, 11);
                if (strong > 5)
                { // 50% each sfx
                    SFXController.Instance.Play("event:/game/02_graveyard/lightning_strike_strong");
                }
                else
                {
                    SFXController.Instance.Play("event:/game/02_graveyard/lightning_strike_weak");
                }
            }
        }
    }
}
