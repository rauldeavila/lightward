using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class RebindButton : MonoBehaviour
{
    public string ControlScheme = "Keyboard";
    public string ActionToRebind = "Jump";
    public string NewBinding = "<Keyboard>/q";

    [Button()]
    public void DoRebind()
    {
        Rebind.Instance.RebindAction(ControlScheme, ActionToRebind, NewBinding);
    }

    [Button()]
    public void StartRebindProcessForAction()
    {
        GameState.Instance.Rebinding = true;
        GetComponentInParent<RebindUI>().StartListeningInput(ActionToRebind, ControlScheme);
    }
}
