using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YellowHealthUpdater : MonoBehaviour
{
    private Image _yellowHealth;

    void Awake()
    {
        _yellowHealth = GetComponent<Image>();
    }

    void Start()
    {
        Health.Instance.OnUpdateHealth.AddListener(UpdateYellowBar);
        UpdateYellowBar();
    }
    void UpdateYellowBar()
    {
        // _yellowHealth.fillAmount = Mathf.Clamp01(CurValueTemp);
        _yellowHealth.fillAmount = Mathf.Clamp01(PlayerStats.Instance.GetCurrentHealth()/100f + PlayerStats.Instance.GetYellowHealth()/100f);
    }


}
