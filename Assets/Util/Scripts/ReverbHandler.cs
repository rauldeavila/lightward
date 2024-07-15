using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbHandler : MonoBehaviour {

    public GameObject rvbCave;
    public GameObject rvbHall;
    public GameObject rvbOff;

    public void ReverbCave(){
        rvbHall.SetActive(false);
        rvbOff.SetActive(false);
        rvbCave.SetActive(true);
    }

    public void ReverbHall(){
        rvbCave.SetActive(false);
        rvbOff.SetActive(false);
        rvbHall.SetActive(true);
    }

    public void DisableReverb(){
        rvbCave.SetActive(false);
        rvbHall.SetActive(false);
        rvbOff.SetActive(true);
    }

}
