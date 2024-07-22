using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RebindAlert : MonoBehaviour
{
    public Image myImage;
    public TextMeshProUGUI myText;
    public static RebindAlert Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void ShowAlert(string message)
    {
        myText.text = message;
        SetAlpha(myImage, 1f);
        SetAlpha(myText, 1f);
        myText.gameObject.SetActive(true);
        myImage.gameObject.SetActive(true);
    }

    public void HideAlert()
    {
        SetAlpha(myImage, 0f);
        SetAlpha(myText, 0f);
        myText.gameObject.SetActive(false);
        myImage.gameObject.SetActive(false);
    }

    private void SetAlpha(Graphic graphic, float alpha)
    {
        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
}
