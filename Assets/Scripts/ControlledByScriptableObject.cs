using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledByScriptableObject : MonoBehaviour
{
    public BoolValue TheScriptableObject;
    public bool OnlyOnAwake = false;
    public List<GameObject> ObjectsToDisableIfTrue;
    public List<GameObject> ObjectsToEnableIfTrue;
    public List<GameObject> ObjectsToDisableIfFalse;
    public List<GameObject> ObjectsToEnableIfFalse;

    public IntValue IntScriptableObject;
    public List<GameObject> ObjectsToEnableIfIntBiggerThanZero;
    void Awake() 
    {
        ToggleObjects(); // bool
        ToggleInt(); // int
    }

    void Update() 
    {
        if(!OnlyOnAwake)
        {
            ToggleObjects();
            ToggleInt();
        }
    }

    void ToggleInt()
    {
        if(IntScriptableObject != null)
        {    
            int value = IntScriptableObject.runTimeValue;
            foreach (GameObject _gameObject in ObjectsToEnableIfIntBiggerThanZero)
            {
                if(value > 0)
                {
                    _gameObject.SetActive(true);
                }
            }
        }
    }


    void ToggleObjects()
    {
        if(TheScriptableObject != null)
        {
            bool scriptableObjectValue = TheScriptableObject.runTimeValue;
            
            foreach (GameObject _gameObject in ObjectsToDisableIfTrue)
            {
                if (scriptableObjectValue)
                {
                    _gameObject.SetActive(false);
                } 
            }
            foreach (GameObject _gameObject in ObjectsToDisableIfFalse)
            {
                if (!scriptableObjectValue)
                {
                    _gameObject.SetActive(false);
                } 
            }
            foreach (GameObject _gameObject in ObjectsToEnableIfTrue)
            {
                if (scriptableObjectValue)
                {
                    _gameObject.SetActive(true);
                } 
            }
            foreach (GameObject _gameObject in ObjectsToEnableIfFalse)
            {
                if (!scriptableObjectValue)
                {
                    _gameObject.SetActive(true);
                } 
            }
        }
    }
}
