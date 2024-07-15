using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransparentImage : MonoBehaviour {


    private Image image;
    private Color32 color = new Color32(0, 0, 0, 255);

    void Awake(){
        image = GetComponent<Image>();
        color = image.color;
    }

    public void SetTextToTransparent(){
        image.color = new Color32(0, 0, 0, 0);
    }

    public void SetTextToRegularColor(){
        image.color = color;
    }



}
