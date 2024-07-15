using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryEquip : MonoBehaviour
{
    void Start()
    {
        InventoryController.Instance.CursorJustStoppedMoving.AddListener(HandleCurrentSelection);
    }

    void HandleCurrentSelection()
    {
        print("Hi from handle currenct selection method =P");
        string currentSelectedName = EventSystem.current.currentSelectedGameObject.name;
        print("this is the current selected object that we need to handle: " + currentSelectedName);
        if(currentSelectedName.Contains("cloak_"))
        {
            print("it's a cloak!");
        }
        else if(currentSelectedName.Contains("item_"))
        {
            print("it's an item!");
        } 
        else if(currentSelectedName.Contains("sword_"))
        {
            print("it's a sword!");
        }
        else
        {
            print("it's not equipable. map | compass | lantern | shard | spell ");
        }
    }

}
