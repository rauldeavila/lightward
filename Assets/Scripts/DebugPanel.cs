using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugPanel : MonoBehaviour
{
    public TextMeshProUGUI RedHealth;
    public TextMeshProUGUI YellowHealth;

    void Update()
    {
        RedHealth.text = "HP: " + PlayerStats.Instance.GetCurrentHealth();
        YellowHealth.text = "HP: " + PlayerStats.Instance.GetYellowHealth();
    }
}