using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSoundsOnAnimator : MonoBehaviour {

    // FMOD -------
    public string footstepToPlay;

    // Setados no GroundController.CS no WIZ
    [FMODUnity.EventRef]
    public string Dirt = "";
    [FMODUnity.EventRef]
    public string Wood = "";
    [FMODUnity.EventRef]
    public string Stone = "";

    private void Awake() {
        footstepToPlay = Dirt;
    }

    public void PlayFootstep(){
        FMODUnity.RuntimeManager.PlayOneShot(footstepToPlay, transform.position);
    }

}
