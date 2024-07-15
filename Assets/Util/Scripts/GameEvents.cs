using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameEvents : MonoBehaviour {

    public static GameEvents Instance;
    public FloatValue wizX;
    public FloatValue wizY;
    public IntValue magicLevel;
    public IntValue hearts;
    public List<IntValue> intObjects;
    public List<BoolValue> wizObjects;
    public List<BoolValue> moneyObjects;
    public List<BoolValue> eventObjects;
    public List<BoolValue> heartObjects;
    public List<BoolValue> bossObjects;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 

    }

    // Breakable.cs calls this
    public bool CheckIfMoneyIsTrue(string moneyName){
        foreach (BoolValue moneyItem in moneyObjects) {
            if(moneyItem.name == moneyName) {
                if(moneyItem.initialValue) {
                    return true;
                } else {
                    return false;
                }
            }
        }
        return false;
    }

    public void SetMoneyToTrue(string moneyName) {
        foreach(BoolValue moneyItem in moneyObjects) {
            if(moneyItem.name == moneyName) {
                moneyItem.initialValue = true;
            }
        }
    }

    public bool CheckIfEventIsTrue(string eventName){
        foreach (BoolValue eventItem in eventObjects) {
            if(eventItem.name == eventName) {
                if(eventItem.initialValue) {
                    return true;
                } else {
                    return false;
                }
            }
        }
        return false;
    }

    public void SetEventToTrue(string eventName) {
        foreach(BoolValue eventItem in eventObjects) {
            if(eventItem.name == eventName) {
                eventItem.initialValue = true;
            }
        }
    }

    public bool CheckIfHeartIsTrue(string heartName){
        foreach (BoolValue heartItem in heartObjects) {
            if(heartItem.name == heartName) {
                if(heartItem.initialValue) {
                    return true;
                } else {
                    return false;
                }
            }
        }
        return false;
    }

    public void SetHeartToTrue(string heartName) {
        foreach(BoolValue heartItem in heartObjects) {
            if(heartItem.name == heartName) {
                heartItem.initialValue = true;
            }
        }
    }

    public bool CheckIfBossIsTrue(string bossName){
        foreach (BoolValue bossItem in bossObjects) {
            if(bossItem.name == bossName) {
                if(bossItem.initialValue) {
                    return true;
                } else {
                    return false;
                }
            }
        }
        return false;
    }

    public void SetBossToTrue(string bossName) {
        foreach(BoolValue bossItem in eventObjects) {
            if(bossItem.name == bossName) {
                bossItem.initialValue = true;
            }
        }
    }

    public bool CheckIfWizObjectIsTrue(string wizObjectName){
        foreach (BoolValue wizItem in wizObjects) {
            if(wizItem.name == wizObjectName) {
                if(wizItem.initialValue) {
                    return true;
                } else {
                    return false;
                }
            }
        }
        return false;
    }

    public void SetWizObjectToTrue(string wizObjectName) {
        foreach(BoolValue wizItem in wizObjects) {
            if(wizItem.name == wizObjectName) {
                wizItem.initialValue = true;
            }
        }
    }

    public void ResetAllScriptableObjects(){
        foreach(BoolValue wizItem in wizObjects) {
            wizItem.initialValue = false;
        }
        foreach(BoolValue heartItem in heartObjects) {
            heartItem.initialValue = false;
        }
        foreach(BoolValue bossItem in bossObjects) {
            bossItem.initialValue = false;
        }
        foreach(BoolValue eventItem in eventObjects) {
            eventItem.initialValue = false;
        }
        foreach(BoolValue moneyItem in moneyObjects) {
            moneyItem.initialValue = false;
        }
        foreach(IntValue intItem in intObjects) {
            intItem.initialValue = 0;
            intItem.runTimeValue = 0;
        }

        wizY.runTimeValue = 0f;
        wizY.initialValue = 0f;
        wizX.runTimeValue = 0f;
        wizX.initialValue = 0f;
        hearts.runTimeValue = 5;
        hearts.initialValue = 5;
        hearts.maxValue = 5;
        magicLevel.runTimeValue = 100;
        magicLevel.initialValue = 100;
        magicLevel.maxValue = 100;
    }

    public void SetWizIntValue(string intObjectName, int newValue, bool maxValue = false) {
        foreach(IntValue wizItem in intObjects) {
            if(wizItem.name == intObjectName) {
                wizItem.initialValue = newValue;
                wizItem.runTimeValue = newValue;
                if(maxValue){
                    wizItem.maxValue = newValue;
                }
            }
        }
    }

    public void AddWizIntValue(string intObjectName, int newValue, bool maxValue = false) {
        foreach(IntValue wizItem in intObjects) {
            if(wizItem.name == intObjectName) {
                wizItem.initialValue = wizItem.initialValue += newValue;
                wizItem.runTimeValue = wizItem.runTimeValue += newValue;
                if(maxValue){
                    wizItem.maxValue = wizItem.maxValue += newValue;
                }
            }
        }
    }

    public void SetWizBoolRunTimeValue(string wizObjectName, bool value = true) {
        foreach(BoolValue wizItem in wizObjects) {
            if(wizItem.name == wizObjectName) {
                if(value){
                    wizItem.runTimeValue = true;
                } else {
                    wizItem.runTimeValue = false;
                }
            }
        }
    }

    public void SetWizBoolValue(string wizObjectName, bool value = true) {
        foreach(BoolValue wizItem in wizObjects) {
            if(wizItem.name == wizObjectName) {
                if(value){
                    wizItem.runTimeValue = true;
                    wizItem.initialValue = true;
                } else {
                    wizItem.runTimeValue = false;
                    wizItem.initialValue = false;
                }
            }
        }
    }


    
}
