using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePosition : MonoBehaviour
{
    public Transform right_top_00;
    public Transform right_mid_00;
    public Transform right_bottom_00;
    public Transform left_top_00;
    public Transform left_mid_00;
    public Transform left_bottom_00;
    public Transform right_top_01;
    public Transform right_mid_01;
    public Transform right_bottom_01;
    public Transform left_top_01;
    public Transform left_mid_01;
    public Transform left_bottom_01;

    public static ParticlePosition Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        { 
            Destroy(this); 
        }
        else
        { 
            Instance = this; 
        }
    }

}
