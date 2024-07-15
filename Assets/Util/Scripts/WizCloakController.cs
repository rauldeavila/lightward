using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizCloakController : MonoBehaviour {

    public Material DefaultCloak;
    public Material BlueCloak;
    public Material RedCloak;
    private SpriteRenderer _sr;

    private Material _default_default;
    private Material _default_ancient;
    private Material _default_master;
    private Material _default_black;
    private Material _blue_default;
    private Material _blue_ancient;
    private Material _blue_master;
    private Material _blue_black;
    private Material _red_default;
    private Material _red_ancient;
    private Material _red_master;
    private Material _red_black;
    private Material _black_default;
    private Material _black_ancient;
    private Material _black_master;
    private Material _black_black;
    // EYES
    private Material _eyes_black_default;
    private Material _eyes_black_ancient;
    private Material _eyes_black_master;
    private Material _eyes_black_black;
    private Material _eyes_default;
    private Material _eyes_red;
    private Material _eyes_blue;

    void Awake(){
        _sr = GetComponent<SpriteRenderer>();
    }

    void Start(){
        InitializeResources();
        if(InventorySOManager.Instance != null){
            InventorySOManager.Instance.OnUpdateCloak.AddListener(UpdateWizSprite);
            InventorySOManager.Instance.OnUpdateSword.AddListener(UpdateWizSprite);
        }
        UpdateWizSprite();
    }

    void InitializeResources()
    {
        _default_default = Resources.Load<Material>("Materials/ColorChangingShader/cloak_default_sword_default");
        _default_ancient = Resources.Load<Material>("Materials/ColorChangingShader/cloak_default_sword_ancient");
        _default_master = Resources.Load<Material>("Materials/ColorChangingShader/cloak_default_sword_master");
        _default_black = Resources.Load<Material>("Materials/ColorChangingShader/cloak_default_sword_black");
        _red_default = Resources.Load<Material>("Materials/ColorChangingShader/cloak_red_sword_default");
        _red_ancient = Resources.Load<Material>("Materials/ColorChangingShader/cloak_red_sword_ancient");
        _red_master = Resources.Load<Material>("Materials/ColorChangingShader/cloak_red_sword_master");
        _red_black = Resources.Load<Material>("Materials/ColorChangingShader/cloak_red_sword_black");
        _blue_default = Resources.Load<Material>("Materials/ColorChangingShader/cloak_blue_sword_default");
        _blue_ancient = Resources.Load<Material>("Materials/ColorChangingShader/cloak_blue_sword_ancient");
        _blue_master = Resources.Load<Material>("Materials/ColorChangingShader/cloak_blue_sword_master");
        _blue_black = Resources.Load<Material>("Materials/ColorChangingShader/cloak_blue_sword_black");
        _black_default = Resources.Load<Material>("Materials/ColorChangingShader/cloak_black_sword_default");
        _black_ancient = Resources.Load<Material>("Materials/ColorChangingShader/cloak_black_sword_ancient");
        _black_master = Resources.Load<Material>("Materials/ColorChangingShader/cloak_black_sword_master");
        _black_black = Resources.Load<Material>("Materials/ColorChangingShader/cloak_black_sword_black");
        _eyes_black_default = Resources.Load<Material>("Materials/Glow/eyes_black_default");
        _eyes_black_ancient = Resources.Load<Material>("Materials/Glow/eyes_black_ancient");
        _eyes_black_master = Resources.Load<Material>("Materials/Glow/eyes_black_master");
        _eyes_black_black = Resources.Load<Material>("Materials/Glow/eyes_black_black");
        _eyes_default = Resources.Load<Material>("Materials/Glow/eyes_default");
        _eyes_red = Resources.Load<Material>("Materials/Glow/eyes_red");
        _eyes_blue = Resources.Load<Material>("Materials/Glow/eyes_blue");
    }

    void UpdateWizSprite()
    {
        switch (ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("wiz_cloak").runTimeValue)
        {
            case "default":
                SetCloakToDefault();
                break;
            case "blue":
                SetCloakToBlue();
                break;
            case "red":
                SetCloakToRed();
                break;
            case "black":
                SetCloakToBlack();
                break;
            default:
                break;
        }
    }

    public void SetCloakToDefault()
    {
        switch (ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("wiz_sword").runTimeValue)
        {
            case "default":
                _sr.material = _default_default;
                WizEyesController.Instance.SetEyesToDefault();
                break;
            case "ancient":
                _sr.material = _default_ancient;
                WizEyesController.Instance.SetEyesToDefault();
                break;
            case "master":
                _sr.material = _default_master;
                WizEyesController.Instance.SetEyesToDefault();
                break;
            case "black":
                _sr.material = _default_black;
                WizEyesController.Instance.SetEyesToDefault();
                break;
            default:
                break;
        }
    }

    public void SetCloakToBlue()
    {
        switch (ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("wiz_sword").runTimeValue)
        {
            case "default":
                _sr.material = _blue_default;
                WizEyesController.Instance.SetEyesToBlue();
                break;
            case "ancient":
                _sr.material = _blue_ancient;
                WizEyesController.Instance.SetEyesToBlue();
                break;
            case "master":
                _sr.material = _blue_master;
                WizEyesController.Instance.SetEyesToBlue();
                break;
            case "black":
                _sr.material = _blue_black;
                WizEyesController.Instance.SetEyesToBlue();
                break;
            default:
                break;
        }
    }

    public void SetCloakToRed()
    {
        switch (ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("wiz_sword").runTimeValue)
        {
            case "default":
                _sr.material = _red_default;
                WizEyesController.Instance.SetEyesToRed();
                break;
            case "ancient":
                _sr.material = _red_ancient;
                WizEyesController.Instance.SetEyesToRed();
                break;
            case "master":
                _sr.material = _red_master;
                WizEyesController.Instance.SetEyesToRed();
                break;
            case "black":
                _sr.material = _red_black;
                WizEyesController.Instance.SetEyesToRed();
                break;
            default:
                break;
        }
    }

    public void SetCloakToBlack()
    {
        switch (ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("wiz_sword").runTimeValue)
        {
            case "default":
                _sr.material = _black_default;
                WizEyesController.Instance.SetEyesToDefault();
                break;
            case "ancient":
                _sr.material = _black_ancient;
                WizEyesController.Instance.SetEyesToGreen();
                break;
            case "master":
                _sr.material = _black_master;
                WizEyesController.Instance.SetEyesToBlue();
                break;
            case "black":
                _sr.material = _black_black;
                WizEyesController.Instance.SetEyesToRed();
                break;
            default:
                break;
        }    
    }

}
