using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepViewTransformUpdated : MonoBehaviour
{
    public Transform RenderQuadTransform;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, RenderQuadTransform.position.z);
    }
    
}
