using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class ScriptableObjectsManager : MonoBehaviour {

    public static ScriptableObjectsManager Instance;

    // Declare the lists here
    public List<FloatValue> FloatObjects;
    public List<IntValue> IntObjects;
    public List<BoolValue> BoolObjects;
    public List<StringValue> StringObjects;

    void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
        FloatObjects = Resources.LoadAll<FloatValue>("ScriptableObjects").ToList();
        IntObjects = Resources.LoadAll<IntValue>("ScriptableObjects").ToList();
        BoolObjects = Resources.LoadAll<BoolValue>("ScriptableObjects").ToList();
        StringObjects = Resources.LoadAll<StringValue>("ScriptableObjects").ToList();

    }

    // This function returns the scriptable object of the given type and name
    public T GetScriptableObject<T>(string name) where T : ScriptableObject {
        if (typeof(T) == typeof(FloatValue)) {
            return FloatObjects.Find(item => item.name == name) as T;
        } else if (typeof(T) == typeof(IntValue)) {
            return IntObjects.Find(item => item.name == name) as T;
        } else if (typeof(T) == typeof(BoolValue)) {
            return BoolObjects.Find(item => item.name == name) as T;
        } else if (typeof(T) == typeof(StringValue)) {
            return StringObjects.Find(item => item.name == name) as T;
        }

        return null;
    }

    public void SetScriptableObjectValue<T>(string name, object value) where T : ScriptableObject {
        if (typeof(T) == typeof(FloatValue)) {
            FloatValue item = FloatObjects.Find(x => x.name == name);
            if (item != null) {
                item.runTimeValue = Convert.ToSingle(value);
                item.initialValue = Convert.ToSingle(value);
            }
        } 
        else if (typeof(T) == typeof(IntValue)) {
            IntValue item = IntObjects.Find(x => x.name == name);
            if (item != null) {
                item.runTimeValue = Convert.ToInt32(value);
                item.initialValue = Convert.ToInt32(value);
            }
        }
        else if (typeof(T) == typeof(BoolValue)) {
            BoolValue item = BoolObjects.Find(x => x.name == name);
            if (item != null) {
                item.runTimeValue = Convert.ToBoolean(value);
                item.initialValue = Convert.ToBoolean(value);
            }
        }
        else if (typeof(T) == typeof(StringValue)) {
            StringValue item = StringObjects.Find(x => x.name == name);
            if (item != null) {
                item.runTimeValue = value.ToString();
                item.initialValue = value.ToString();
            }
        }
    }

    public void SetMaxValueForFloatObject(string name, float newMaxValue) {
        FloatValue item = FloatObjects.Find(x => x.name == name);
        if (item != null) {
            item.maxValue = newMaxValue;
        }
}
    
    public bool CheckIfBoolIsTrue<T>(string objName) where T : BoolValue {
        T item = GetScriptableObject<T>(objName);

        if (item != null && item.initialValue) {
            return true;
        }

        return false;
    }


    [Button]
    public void ResetAllForNewGame(){
        print("Reseting stuff for new game...");
        foreach(var obj in FloatObjects){
            if(obj.name == "wiz_health"){
                obj.runTimeValue = 5f;
                obj.maxValue = 5f;
            } else if(obj.name == "wiz_magic"){
                obj.runTimeValue = 3f;
                obj.maxValue = 3f;
            } else if(obj.name == "wiz_x"){
                obj.runTimeValue = 30.5f;
            } else if(obj.name == "wiz_y"){
                obj.runTimeValue = 25.5f;
            }else {
                obj.runTimeValue = 0f;
                obj.maxValue = 0f;
            }
        }
        foreach(var obj in BoolObjects){
            if(obj.name == "game_new_game"){
                obj.runTimeValue = true;
            } else if(obj.name == "wiz_dash"){
                obj.runTimeValue = true;
            } else if(obj.name == "wiz_wall_jump"){
                obj.runTimeValue = true;
            } else {
                obj.runTimeValue = false;
            }
        }
        foreach(var obj in IntObjects){
            if(obj.name == "wiz_magic"){
                obj.maxValue = 3;
                obj.runTimeValue = 3;
            } else {
                obj.runTimeValue = 0;
            }
        }
        foreach(var obj in StringObjects){
            obj.runTimeValue = "";
        }
    }



    public void ResetAreaName()
    {
        SetScriptableObjectValue<StringValue>("game_last_area_name_shown", "");
    }

}
