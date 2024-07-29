using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class FadePanel : MonoBehaviour
{
    public Animator m_Animator;
    public static FadePanel Instance;
    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }

    [Button()]
    public void FadeOut()
    {
        m_Animator.Play("fadeout");
    }
    [Button()]
    public void FadeIn()
    {
        m_Animator.Play("fadein");
    }
}
