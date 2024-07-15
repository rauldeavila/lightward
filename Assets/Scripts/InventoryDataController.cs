using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDataController : MonoBehaviour
{
    public static InventoryDataController Instance;
    public List<ItemData> Items;
    ItemData[] loadedItems;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
        LoadInventory();
    }

    public void LoadInventory()
    {
        Items = new List<ItemData>();
        loadedItems = Resources.LoadAll<ItemData>("ScriptableObjects/Items");
        Items.AddRange(loadedItems);
        if (loadedItems.Length > 0)
        {
            foreach (ItemData item in loadedItems)
            {
                item.Initialize();
            }
        }
        else
        {
            Debug.LogWarning("No Items found in the Resources folder!");
        }
    }
    public bool CheckItemInInventory(string itemID)
    {
        ItemData itemData = Items.Find(item => item.ID == itemID);
        if (itemData != null) {
            return itemData.HasItem;
        } else {
            return false; // Item not found
        }
    }

    public Sprite GetSpriteName(string itemID)
    {
        foreach (ItemData item in loadedItems)
        {
            if(itemID == item.ID) {
                return item.GetSprite();
            }
        }
        return null;
    }
    public string GetName(string itemID)
    {
        foreach (ItemData item in loadedItems)
        {
            if(itemID == item.ID) {
                return item.GetName();
            }
        }
        return "";
    }
    public string GetDescription(string itemID)
    {
        foreach (ItemData item in loadedItems)
        {
            if(itemID == item.ID) {
                return item.GetDescription();
            }
        }
        return "";
    }


}
