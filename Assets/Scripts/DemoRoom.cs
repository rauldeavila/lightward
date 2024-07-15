using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoRoom : MonoBehaviour
{
    public bool ThisRoomIsADemo = false;
    public float MaxHearts = 8f;

    void Start()
    {
        if(ThisRoomIsADemo)
        {
            ScriptableObjectsManager.Instance.ResetAllForNewGame();
            ScriptableObjectsManager.Instance.SetScriptableObjectValue<BoolValue>("game_new_game", false);
            ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("wiz_x", FindObjectOfType<PlayerController>().transform.position.x);
            ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("wiz_y", FindObjectOfType<PlayerController>().transform.position.y);
            ScriptableObjectsManager.Instance.SetMaxValueForFloatObject("wiz_health", MaxHearts);
            ScriptableObjectsManager.Instance.SetScriptableObjectValue<FloatValue>("wiz_health", MaxHearts);
        }
    }
}
