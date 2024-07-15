using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour {

    public delegate void MagicChanged();
    [System.Serializable]
    public class MagicChangeEvent : UnityEvent { }
    public MagicChangeEvent OnMagicChanged = new MagicChangeEvent();
    
    
    public static PlayerStats Instance;


    public string Cloak = "default";

    public SignalSender gained_health;
    public SignalSender lost_health;

    //------

    public FloatValue wiz_health_f;
    public FloatValue wiz_health_yellow_f;

    public FloatValue wiz_magic;
    public BoolValue wall_jump;
    private int recentCoinsAcumulator = 0;
    public IntValue coins;
    private bool timesUp = false;

    public int FireballPower = 3;
    public int AttackPower = 1;

    public IntValue keys;

    // SPELLS -------
    public BoolValue fireball;
    public BoolValue dashingSoul;
    public BoolValue dashingLight;

    private HealthUIController healthUI;

    public IntValue magicSlot1Index;
    public IntValue magicSlot2Index;

    public IntValue sword; // 0 = regular / 1 = ancient / 2 = void

    public IntValue bottle1; // 0 = dont have / 1 = empty / 2 = potion 1 (...)
    public IntValue bottle2;
    public IntValue bottle3;
    public IntValue bottle4;
    public IntValue bottle5;  

    // public BoolValue cloak1; // starts true / default cloak       - true or false
    // public BoolValue cloak2;
    // public BoolValue cloak3;
    // public BoolValue cloak4;

    public BoolValue facingRight;
    public FloatValue wiz_x;
    public FloatValue wiz_y;
    public BoolValue loading_new_scene;

    private int _wizDamage = 5;

    [FMODUnity.EventRef]
    public string CoinsSound = "";

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
        healthUI = FindObjectOfType<HealthUIController>();
    }

    void Start()
    {
        // CHECK WIZ CLOAK OR ANYTHING THAT PREVENTS DAMAGE...
        _wizDamage = 5;
    }

    void Update(){
        if(timesUp){
            timesUp = false;
            // add coins to wallet
            coins.initialValue += recentCoinsAcumulator;
            // reset recent coins
            recentCoinsAcumulator = 0;
            // hide recent picked ui
            HideRecentPickedUI(); // call to UI coins manager
        }
    }

    public void PickUpCoin(){
        FMODUnity.RuntimeManager.PlayOneShot(CoinsSound, transform.position);
        recentCoinsAcumulator += 1;
        EnableCoinsAndItemsPanel.Instance.ShowCoins();
        CoinsPanelController.Instance.UpdateTempCoinsText(recentCoinsAcumulator.ToString());
        CoinsPanelController.Instance.ShowTempCoins();
        CancelInvoke("ResetTimer");
        Invoke("ResetTimer", 3f);
    }

    void ResetTimer(){
        timesUp = true;
    }

    void HideRecentPickedUI(){
        CoinsPanelController.Instance.HideTempCoins();
        Invoke("HidePanel", 3f);
    }

    void HidePanel(){
        EnableCoinsAndItemsPanel.Instance.HideCoins();
    }

    public void DecreaseMagic(float amount){
        wiz_magic.runTimeValue = wiz_magic.runTimeValue - amount;
        Magic.Instance.UpdateMagic();
        OnMagicChanged?.Invoke();
    }

    public void IncreaseMagic(float amount){
        wiz_magic.runTimeValue = wiz_magic.runTimeValue + amount;
        Magic.Instance.UpdateMagic();
    }

    public float GetMaxMagic(){
        return wiz_magic.maxValue;
    }

    public float GetCurrentMagic(){
        return wiz_magic.runTimeValue;
    }

    public void DecreaseHealth(){ // muda de int pra float...
        float difference = wiz_health_yellow_f.runTimeValue - _wizDamage;
        if(difference >=0){
            wiz_health_yellow_f.runTimeValue -= _wizDamage;
            Health.Instance.UpdateHealth();
            return;
        } else {
            wiz_health_yellow_f.runTimeValue = 0;
            wiz_health_f.runTimeValue = wiz_health_f.runTimeValue + difference; // diff is a negative value.
            Health.Instance.UpdateHealth();
        }
        // lost_health.Raise();
    }

    public void IncreaseYellowHealth(){
        float difference = wiz_health_f.maxValue - (wiz_health_f.runTimeValue + wiz_health_yellow_f.runTimeValue);
        if(difference > 0){
            wiz_health_yellow_f.runTimeValue += _wizDamage;
            HealthUIController.Instance.GetComponent<Animator>().Play("hearts-panel-yellow-heart");
            WizEyesController.Instance. SetEyesToYellow();
        }
        Health.Instance.UpdateHealth();
    }

    public void IncreaseHealth(int amount){
        // wiz_health.runTimeValue = wiz_health.runTimeValue + amount;
        wiz_health_f.runTimeValue = wiz_health_f.runTimeValue + amount;
        // gained_health.Raise();
        Health.Instance.UpdateHealth();
    }

    public float GetMaxHealth(){
        // return wiz_health.maxValue;
        return wiz_health_f.maxValue;
    }

    public float GetCurrentHealth(){
        // return wiz_health.runTimeValue;
        return wiz_health_f.runTimeValue;
    }

    public bool IsDashingLightEquiped(){
        if(magicSlot1Index.initialValue == 6 || magicSlot2Index.initialValue == 6){
            return true;
        } else {
            return false;
        }
    }

    public float GetYellowHealth(){
        // return wiz_yellow_health.runTimeValue;
        return wiz_health_yellow_f.runTimeValue;
    }

    public float GetTotalHealth()
    {
        return wiz_health_f.runTimeValue + wiz_health_yellow_f.runTimeValue;
    }

}
