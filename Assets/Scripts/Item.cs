using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    private void OnEnable() {
        GetComponent<Image>().sprite = InventoryDataController.Instance.GetSpriteName(this.gameObject.name);
    }

    public string GetName()
    {
        if (!InventoryDataController.Instance.CheckItemInInventory(this.gameObject.name))
        {
            return "";
        }
        else
        {
            return InventoryDataController.Instance.GetName(this.gameObject.name);
        }
    }

    public string GetDescription()
    {
        if (!InventoryDataController.Instance.CheckItemInInventory(this.gameObject.name))
        {
            return "";
        }
        else
        {
            return InventoryDataController.Instance.GetDescription(this.gameObject.name);
        }
    }

    public bool WizHasItem()
    {
        return InventoryDataController.Instance.CheckItemInInventory(this.gameObject.name);
    }

}

