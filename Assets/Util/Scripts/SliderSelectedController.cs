using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderSelectedController : MonoBehaviour, ISelectHandler, IDeselectHandler
{
     public GameObject thisSlider;
     private Animator animator;

     private void Awake(){
         animator = GetComponent<Animator>();
     }

     //Do this when the selectable UI object is selected.
     public void OnSelect(BaseEventData eventData) {
        animator.SetTrigger("selected");
     }
     public void OnDeselect(BaseEventData eventData) {
        animator.SetTrigger("deselected");
     }

}
