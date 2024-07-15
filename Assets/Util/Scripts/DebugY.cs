using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugY : MonoBehaviour {

    public Image Up;
    public Image Even;
    public Image Down;

    public static DebugY Instance;

    void Awake() {
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        }
    }

    void Update(){
        if(StateController.Instance.CanModifyPhysics == false){
            DeactivateAll();
        }
    }

    public void DeactivateAll(){
        Up.color = HexToColor("#444444");
        Down.color = HexToColor("#444444");
        Even.color = HexToColor("#444444");
    }

    public void ActivateUp(){
        DeactivateAll();
        Up.color = HexToColor("#FFFFFF");
    }

    public void ActivateDown(){
        DeactivateAll();
        Down.color = HexToColor("#FFFFFF");
    }

    public void ActivateEven(){
        DeactivateAll();
        Even.color = HexToColor("#FFFFFF");
    }

    private Color HexToColor(string hex) {
        Color color = Color.white;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }

}
