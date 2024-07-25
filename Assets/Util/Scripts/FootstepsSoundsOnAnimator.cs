using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSoundsOnAnimator : MonoBehaviour {


    public string Dirt = "event:/char/wiz/footstep_dirt";
    public string Wood = "event:/char/wiz/footstep_wood";
    public string Stone = "event:/char/wiz/footstep_stone";
    public string Grass = "event:/char/wiz/footstep_grass";

    public void PlayFootstep()
    {
        if(PlayerState.Instance.OnGrass)
        {
            SFXController.Instance.Play(Grass);
        }
        else if(PlayerState.Instance.OnStone)
        {
            SFXController.Instance.Play(Stone);
        }
        else if(PlayerState.Instance.OnWood)
        {
            SFXController.Instance.Play(Wood);
        }
        else
        {
            SFXController.Instance.Play(Dirt);
        }
    }

}
