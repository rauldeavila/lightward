using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellySpikes : MonoBehaviour
{
    private Animator _anim;
    public bool SpikesAreActive = false;

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void ActivateSpikes()
    {
        if(!SpikesAreActive)
        {
            SpikesAreActive = true;
            _anim.Play("skelly_spikes_activating");
            FMODUnity.RuntimeManager.PlayOneShot("event:/game/02_graveyard/skely/skely_spikes", transform.position);
        }
    }


    public void DeactivateSpikes()
    {
        if(SpikesAreActive)
        {
            SpikesAreActive = false;
            _anim.Play("skelly_spikes_deactivating");
        }
    }
}
