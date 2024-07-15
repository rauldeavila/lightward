using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    public Sprite ItemSprite;
    public string ID;
    public string Name;
    [TextArea] public string Description;
    public ItemType ItemType;
    public BottleState BottleState = BottleState.NotBottle;
    public bool HasItem;
    public bool IsEquipped;

    public string GetDescription()
    {
        string baseDescription = Description;
        if(!HasItem)
        {
            Description = "";
        }
        else
        {
            switch(BottleState)
            {
                case BottleState.Empty:
                    Description = "An empty bottle, perfect for catching unsuspecting fairies... or maybe just rainwater.";
                    break;
                case BottleState.HealthBottle:
                    Description = "A bottle brimming with a vibrant red potion. One sip promises a surge of vitality.";
                    break;
                case BottleState.MagicBottle:
                    Description = "A bottle filled with a swirling green liquid. Its mystical vapors hint at replenished magical energy.";
                    break;
                case BottleState.Milk:
                    Description = "A bottle filled with fresh milk. This creamy concoction offers both nourishment and a touch of home-brewed comfort.";
                    break;
                default:
                    Description = baseDescription;
                    break;
            }
        }
        return Description;
    }

    public Sprite GetSprite()
    {   
        Sprite baseSprite = ItemSprite;
        if(!HasItem) 
        { 
            ItemSprite = Resources.Load<Sprite>("Items/item_empty_slot");
            return ItemSprite;
        }
        else
        {
            switch(BottleState)
            {
                case BottleState.Empty:
                    ItemSprite = Resources.Load<Sprite>("Items/item_bottle_empty");
                    break;
                case BottleState.HealthBottle:
                    ItemSprite = Resources.Load<Sprite>("Items/item_bottle_health");
                    break;
                case BottleState.MagicBottle:
                    ItemSprite = Resources.Load<Sprite>("Items/item_bottle_magic");
                    break;
                case BottleState.Milk:
                    ItemSprite = Resources.Load<Sprite>("Items/item_bottle_milk");
                    break;
                default:
                    ItemSprite = Resources.Load<Sprite>("Items/" + ID);
                    break;
            }
        }
        return ItemSprite;
    }

    public string GetName()
    {
        if(!HasItem) 
        { 
            return "";
        }
        else
        {
            switch(BottleState)
            {
                case BottleState.Empty:
                    Name = "Empty Bottle"; 
                    break;
                case BottleState.HealthBottle:
                    Name = "Health Potion";
                    break;
                case BottleState.MagicBottle:
                    Name = "Magic Potion";
                    break;
                case BottleState.Milk:
                    Name = "Milk";
                    break;
                default:
                    break;
            }
            return Name;
        }
    }

    public void Initialize()
    {
        GetName();
        GetDescription();
        GetSprite();
    }
}

public enum ItemType 
{
    KeyItem,
    Sword,
    Cloak,
    Utility,
    Spell

}

public enum BottleState
{
    NotBottle,
    Empty,
    HealthBottle,
    MagicBottle,
    Milk
}
