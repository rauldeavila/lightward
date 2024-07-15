using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransparentText : MonoBehaviour {
	
    
    private TextMeshProUGUI text;
    private Color32 color = new Color32(0, 0, 0, 255);

    void Awake(){
        text = GetComponent<TextMeshProUGUI>();
        color = text.color;
    }

    public void SetTextToTransparent(){
        text.color = new Color32(0, 0, 0, 0);
    }

    public void SetTextToRegularColor(){
        text.color = color;
    }





}
