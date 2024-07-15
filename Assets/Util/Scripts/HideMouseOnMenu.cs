using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMouseOnMenu : MonoBehaviour {
    
    public static HideMouseOnMenu Instance;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
        StartCoroutine(DelayedLockMouse());
    }
   
    IEnumerator DelayedLockMouse(){
        yield return new WaitForSeconds(0.1f);
        Cursor.visible = false;
        Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void HideMouse(){
        Cursor.visible = false;
        Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowMouse(){
        Cursor.visible = true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); 
        Cursor.lockState = CursorLockMode.None;
    }


}
