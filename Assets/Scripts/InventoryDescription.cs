using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryDescription : MonoBehaviour
{

    private TextMeshProUGUI _textBox;

    void Awake()
    {
        _textBox = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        _textBox.text = EventSystem.current.currentSelectedGameObject.GetComponent<Item>().GetDescription();
    }
}
