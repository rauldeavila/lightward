using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
// default color: rgba(1.0, 0.914, 0.773, 1.0) 
public class HealthBarController : MonoBehaviour
{
    private bool _tinted = false;
    public List<Image> ImagesToTint;
    private Color _defaultColor = new Color(1.0f, 0.914f, 0.773f, 1.0f); 
    private Color _pinkishColor = new Color(1.0f, 0.498f, 0.545f, 1.0f); 
    private Color _redColor = new Color(0.906f, 0.176f, 0.263f, 1.0f); 

    void Start()
    {
        Health.Instance.OnUpdateHealth.AddListener(CheckIfLowHealth);
        CheckIfLowHealth();
    }

    void CheckIfLowHealth()
    {
        if(PlayerStats.Instance.GetTotalHealth() < 15f)
        {
            TintBarRed();
        }
        else if(PlayerStats.Instance.GetTotalHealth() <= 25f)
        {
            TintBarPink();
        }
        else
        {
            if(_tinted)
            {   
                ReturnBarToOriginalColor();
            }
        }
    }

    void TintBarPink()
    {
        _tinted = true;
        foreach (Image image in ImagesToTint)
        {
            image.color = _pinkishColor;
        }
    }

    void TintBarRed()
    {
        _tinted = true;
        foreach (Image image in ImagesToTint)
        {
            image.color = _redColor;
        }
    }

    void ReturnBarToOriginalColor()
    {
        _tinted = false;
        foreach (Image image in ImagesToTint)
        {
            image.color = _defaultColor;
        }
    }


}

