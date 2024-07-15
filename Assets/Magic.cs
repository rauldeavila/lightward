
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
/* 
 * This event is invoked with any changes to the health.
 * In order to subscribe use: 
 * Health.Instance.OnUpdateHealth.AddListener(MethodToTrigger);  
 */
public class Magic : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnUpdateMagic;    
    public static Magic Instance;

    void Awake()
    {
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        }
    }

    public void UpdateMagic()
    {
        // print("UpdateMagic event from Magic.cs");
        OnUpdateMagic?.Invoke();
    }

}


