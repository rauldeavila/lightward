using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
public class InventorySOManager : MonoBehaviour
{
    public static InventorySOManager Instance;


    [HideInInspector]
    public UnityEvent OnUpdateCloak;
    public string CurrentCloak = ""; // From Resources/ScriptableObjects/wiz_cloak - "default" | "blue" | "red" | "black" .
    private List<string> allowedCloaks = new List<string> { "default", "blue", "red", "black" };

    [HideInInspector]
    public UnityEvent OnUpdateSword;
    public string CurrentSword = ""; // From Resources/ScriptableObjects/wiz_sword - "default" | "ancient" | "master" | "black" .
    private List<string> allowedSwords = new List<string> { "default", "ancient", "master", "black" };

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }

    void Start()
    {
        CurrentCloak = Resources.Load<StringValue>("ScriptableObjects/wiz_cloak").runTimeValue;
        CurrentSword = Resources.Load<StringValue>("ScriptableObjects/wiz_sword").runTimeValue;
    }

    [Button]
    public void UpdateCloak(string cloak)
    {
        if(!allowedCloaks.Contains(cloak))
        {
            Debug.LogError("Invalid cloak type!");
            return;
        }
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("wiz_cloak", cloak);
        CurrentCloak = cloak;
        UpdateSpriteScriptableObject();
        OnUpdateCloak?.Invoke();
    }

    [Button]
    public void UpdateSword(string sword)
    {
        if(!allowedSwords.Contains(sword))
        {
            Debug.LogError("Invalid sword type!");
            return;
        }
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("wiz_sword", sword);
        CurrentSword = sword;
        UpdateSpriteScriptableObject();
        OnUpdateSword?.Invoke();
    }

    void UpdateSpriteScriptableObject()
    {
        switch (ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("wiz_cloak").runTimeValue)
        {
            case "default":
                switch (ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("wiz_sword").runTimeValue)
                {
                    case "default":
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("wiz_current_sprite", "default_default");
                        break;
                    case "ancient":
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("wiz_current_sprite", "default_ancient");
                        break;
                    case "master":
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("wiz_current_sprite", "default_master");
                        break;
                    case "black":
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("wiz_current_sprite", "default_black");
                        break;
                    default:
                        break;
                }
                    break;
            case "blue":
                switch (ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("wiz_sword").runTimeValue)
                {
                    case "default":
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("wiz_current_sprite", "blue_default");
                        break;
                    case "ancient":
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("wiz_current_sprite", "blue_ancient");
                        break;
                    case "master":
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("wiz_current_sprite", "blue_master");
                        break;
                    case "black":
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("wiz_current_sprite", "blue_black");
                        break;
                    default:
                        break;
                }
                break;
            case "red":
                switch (ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("wiz_sword").runTimeValue)
                {
                    case "default":
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("wiz_current_sprite", "red_default");
                        break;
                    case "ancient":
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("wiz_current_sprite", "red_ancient");
                        break;
                    case "master":
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("wiz_current_sprite", "red_master");
                        break;
                    case "black":
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("wiz_current_sprite", "red_black");
                        break;
                    default:
                        break;
                }
                break;
            case "black":
                switch (ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("wiz_sword").runTimeValue)
                {
                    case "default":
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("wiz_current_sprite", "black_default");
                        break;
                    case "ancient":
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("wiz_current_sprite", "black_ancient");
                        break;
                    case "master":
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("wiz_current_sprite", "black_master");
                        break;
                    case "black":
                        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("wiz_current_sprite", "black_black");
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }


}
