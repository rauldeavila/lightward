using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadLayer : MonoBehaviour {

    public string sortingLayerName = string.Empty; //initialization before the methods
    public int orderInLayer = 0;
    public Renderer MyRenderer;

    private void Start(){
        SetSortingLayer();
    }


    void SetSortingLayer() {
        if (sortingLayerName != string.Empty){
            MyRenderer.sortingLayerName = sortingLayerName;
            MyRenderer.sortingOrder = orderInLayer;
        }
    }

}
