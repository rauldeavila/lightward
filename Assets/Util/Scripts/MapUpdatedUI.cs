using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUpdatedUI : MonoBehaviour
{
    private Animator animator;

    private void Awake(){
        animator = GetComponent<Animator>();
    }

    public void ActivateMapUpdatedUI(){
        animator.SetTrigger("activate");
    }

}
