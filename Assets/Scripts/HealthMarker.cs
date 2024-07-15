using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMarker : MonoBehaviour
{

    public static HealthMarker Instance;
    public Transform parentTransform; // Assign your main UI's parent in the Inspector

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }
    void LateUpdate() // Or use LateUpdate() if needed
    {
        transform.SetParent(parentTransform); // Ensure the HealthMarker is under the desired parent
        transform.SetAsLastSibling();
    }
}
