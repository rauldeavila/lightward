using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(OnTrigger))]
public class Collectable : MonoBehaviour
{
    public enum ItemType
    {
        Key,
        Heart,
        // Add more item types here as needed
    }

    public ItemType itemType;

    private OnTrigger _onTrigger;

    void Awake()
    {
        _onTrigger = GetComponent<OnTrigger>();
        _onTrigger.WizEnteredTriggerEvent.AddListener(OnWizEnteredTrigger);
    }

    void OnDestroy()
    {
        _onTrigger.WizEnteredTriggerEvent.RemoveListener(OnWizEnteredTrigger);
    }

    private void OnWizEnteredTrigger()
    {
        CollectItem();
    }

    public void CollectItem()
    {
        switch (itemType)
        {
            case ItemType.Key:
                print("Key collected!");
                break;
            case ItemType.Heart:
                print("Heart collected!");
                break;
            // Add more cases here as needed
            default:
                print("Unknown item collected!");
                break;
        }
    }
}