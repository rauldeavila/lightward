using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedHealthUpdater : MonoBehaviour
{
    private Image _redHealth;

    void Awake()
    {
        _redHealth = GetComponent<Image>();
    }

    void Start()
    {
        Health.Instance.OnUpdateHealth.AddListener(UpdateRedBar);
        UpdateRedBar();
    }
    void UpdateRedBar()
    {
        // _redHealth.fillAmount = Mathf.Clamp01(CurValueTemp);
        _redHealth.fillAmount = Mathf.Clamp01(PlayerStats.Instance.GetCurrentHealth()/100f);
            
        RectTransform markerRect = HealthMarker.Instance.GetComponent<RectTransform>();
        RectTransform healthBarRect = _redHealth.GetComponent<RectTransform>();

        float minPosition = -4.7f;
        float maxPosition = 194f;
        float currentHealth = Mathf.Clamp01(PlayerStats.Instance.GetCurrentHealth()/100f); 

        // Linear interpolation 
        float markerXPos = Mathf.Lerp(minPosition, maxPosition, currentHealth);

        // Use localPosition to directly modify position
        markerRect.localPosition = new Vector3(markerXPos, markerRect.localPosition.y, markerRect.localPosition.z); 
        if(currentHealth == 0f)
        {
            markerRect.localPosition = new Vector3(-99999f, markerRect.localPosition.y, markerRect.localPosition.z);
        }
            
    }


}
