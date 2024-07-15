
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
public class Health : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnUpdateHealth;    
    public static Health Instance;

    void Awake()
    {
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        }
    }

    public void UpdateHealth()
    {
        // print("UpdateHealth event from Health.cs");
        OnUpdateHealth?.Invoke();
    }

}


