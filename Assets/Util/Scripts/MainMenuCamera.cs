using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour {

    public Animator logoAnimator;

    public void DisableLogo(){
        logoAnimator.SetBool("disabled", true);
    }

    public void EnableLogo(){
        logoAnimator.SetBool("disabled", false);
    }

}
