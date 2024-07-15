using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour {

    public static SaveSystem Instance;

    private IntValue _slot;
    private string _dataPath;

    [System.Serializable]
    public class SaveData {
        public List<IntValueSaveData> IntObjects = new List<IntValueSaveData>();
        public List<FloatValueSaveData> FloatObjects = new List<FloatValueSaveData>();
        public List<BoolValueSaveData> BoolObjects = new List<BoolValueSaveData>();
        public List<StringValueSaveData> StringObjects = new List<StringValueSaveData>();
    }

    [System.Serializable]
    public class IntValueSaveData {
        public string name;
        public int initialValue;
        public int maxValue;

        public IntValueSaveData(IntValue intValue) {
            name = intValue.name;
            initialValue = intValue.initialValue;
            maxValue = intValue.maxValue;
        }
    }

    [System.Serializable]
    public class FloatValueSaveData {
        public string name;
        public float initialValue;
        public float maxValue;

        public FloatValueSaveData(FloatValue floatValue) {
            name = floatValue.name;
            initialValue = floatValue.initialValue;
            maxValue = floatValue.maxValue;
        }
    }

    [System.Serializable]
    public class BoolValueSaveData {
        public string name;
        public bool initialValue;

        // The constructor now takes the runtime value and sets it as the initial value.
        public BoolValueSaveData(BoolValue boolValue) {
            name = boolValue.name;
            initialValue = boolValue.runTimeValue; // Assuming this is the current state you want to save.
        }
    }

    [System.Serializable]
    public class StringValueSaveData {
        public string name;
        public string initialValue;

        public StringValueSaveData(StringValue stringValue) {
            name = stringValue.name;
            initialValue = stringValue.initialValue;
        }
    }

    void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
        _dataPath = Application.persistentDataPath;
    }

    void Start(){
        _slot = ScriptableObjectsManager.Instance.GetScriptableObject<IntValue>("game_slot"); 
    }

    public void SaveGame() {
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("wiz_facing_right", PlayerState.Instance.FacingRight);
        float xPosition = PlayerController.Instance.transform.position.x;
        float yPosition = PlayerController.Instance.transform.position.y;

        ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("game_new_game",  false);
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("wiz_x",  xPosition);
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("wiz_y",  yPosition);

        float maxMagic = ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("hero_magic").maxValue;
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("hero_magic",  maxMagic);
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("hero_health_yellow", 0);
        float maxHealth = ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>("hero_health").maxValue;
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("hero_health", maxHealth);
        string currentScene = ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>("game_current_scene").runTimeValue;
        ScriptableObjectsManager.Instance.SetScriptableObjectValue<StringValue>("game_saved_scene", currentScene);
        StartCoroutine(HandleSaving());
    }

    private IEnumerator HandleSaving() {
        yield return new WaitForSeconds(0.5f);

        SaveData saveData = new SaveData();

        foreach (var intValue in ScriptableObjectsManager.Instance.IntObjects) {
            saveData.IntObjects.Add(new IntValueSaveData(intValue));
        }

        foreach (var floatValue in ScriptableObjectsManager.Instance.FloatObjects) {
            saveData.FloatObjects.Add(new FloatValueSaveData(floatValue));
        }

        foreach (var boolValue in ScriptableObjectsManager.Instance.BoolObjects) {
            // Before creating the BoolValueSaveData, set the initialValue to the current runtime value.
            boolValue.initialValue = boolValue.runTimeValue; // Update the scriptable object's initial value.
            saveData.BoolObjects.Add(new BoolValueSaveData(boolValue));
        }

        foreach (var stringValue in ScriptableObjectsManager.Instance.StringObjects) {
            saveData.StringObjects.Add(new StringValueSaveData(stringValue));
        }

        string jsonData = JsonUtility.ToJson(saveData, true);
        string saveFilePath = Path.Combine(_dataPath, "save" + _slot.initialValue + ".json");
        File.WriteAllText(saveFilePath, jsonData);

        Debug.Log("Game saved to " + saveFilePath);
        Health.Instance.UpdateHealth();
        // HealthUIController.Instance.UpdateUI();
        // TODO
        // MagicUIController.Instance.UpdateUI();
    }

    public void LoadGame() {
        // Define the save file path
        string saveFilePath = Path.Combine(_dataPath, "save" + _slot.initialValue + ".json");

        // If the save file doesn't exist, we can't load the data
        if (!File.Exists(saveFilePath)) {
            Debug.LogError("Save file not found at " + saveFilePath);
            LoadNewGame();
            return;
        }

        // Read the JSON data from the file
        string jsonData = File.ReadAllText(saveFilePath);

        // Convert the JSON back to a SaveData object
        SaveData loadedData = JsonUtility.FromJson<SaveData>(jsonData);

        // Populate the manager's scriptable objects with the loaded data
        foreach (var intValueData in loadedData.IntObjects) {
            var intValue = ScriptableObjectsManager.Instance.GetScriptableObject<IntValue>(intValueData.name);
            if (intValue != null) {
                intValue.runTimeValue = intValueData.initialValue;
                intValue.maxValue = intValueData.maxValue;
            }
        }

        foreach (var floatValueData in loadedData.FloatObjects) {
            var floatValue = ScriptableObjectsManager.Instance.GetScriptableObject<FloatValue>(floatValueData.name);
            if (floatValue != null) {
                floatValue.runTimeValue = floatValueData.initialValue;
                floatValue.maxValue = floatValueData.maxValue;
            }
        }

        foreach (var boolValueData in loadedData.BoolObjects) {
            var boolValue = ScriptableObjectsManager.Instance.GetScriptableObject<BoolValue>(boolValueData.name);
            if (boolValue != null) {
                boolValue.runTimeValue = boolValueData.initialValue;
            }
        }

        foreach (var stringValueData in loadedData.StringObjects) {
            var stringValue = ScriptableObjectsManager.Instance.GetScriptableObject<StringValue>(stringValueData.name);
            if (stringValue != null) {
                stringValue.runTimeValue = stringValueData.initialValue;
            }
        }
        Debug.Log("Game loaded from " + saveFilePath);
    }

    private void LoadNewGame(){
        ScriptableObjectsManager.Instance.ResetAllForNewGame();
    }
}
