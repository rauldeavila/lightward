using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WizIconUpdater : MonoBehaviour
{
    private Image _m_Image;
    private Sprite _default;
    private Sprite _blue;
    private Sprite _red;
    private Sprite _black_default;
    private Sprite _black_ancient;
    private Sprite _black_master;
    private Sprite _black_black;

    void Awake()
    {
        _m_Image = GetComponent<Image>();
    }
    void Start()
    {
        InitializeResources();
        InventorySOManager.Instance.OnUpdateCloak.AddListener(UpdateWizIcon);        
        InventorySOManager.Instance.OnUpdateSword.AddListener(UpdateWizIcon);        
        UpdateWizIcon();
    }

    void UpdateWizIcon()
    {
        switch (ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("wiz_cloak").runTimeValue)
        {
            case "default":
                _m_Image.sprite = _default; 
                break;
            case "blue":
                _m_Image.sprite = _blue;
                break;
            case "red":
                _m_Image.sprite = _red;
                break;
            case "black":
                HandleBlack(); // based on sword
                break;
            default:
                break;
        }
    }



    void HandleBlack()
    {
        switch (ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("wiz_sword").runTimeValue)
        {
            case "default":
                _m_Image.sprite = _black_default;
                break;
            case "ancient":
                _m_Image.sprite = _black_ancient;
                break;
            case "master":
                _m_Image.sprite = _black_master;
                break;
            case "black":
                _m_Image.sprite = _black_black;
                break;
            default:
                break;
        }  
    }

    void InitializeResources()
    {
        _default = Resources.Load<Sprite>("Items/wiz_inventory_default");
        _blue = Resources.Load<Sprite>("Items/wiz_inventory_blue");
        _red = Resources.Load<Sprite>("Items/wiz_inventory_red");
        _black_default = Resources.Load<Sprite>("Items/wiz_inventory_black_default");
        _black_ancient = Resources.Load<Sprite>("Items/wiz_inventory_black_ancient");
        _black_master = Resources.Load<Sprite>("Items/wiz_inventory_black_master");
        _black_black = Resources.Load<Sprite>("Items/wiz_inventory_black_black");
    }

}
