using QFSW.QC;
using UnityEngine;

// This script will attempt to find the QuantumConsole component to subscribe to in the following order
// 1. The assigned _qc value
// 2. An instance of QuantumConsole on the same object
// 3. A singleton QuantumConsole instance in the scene
public class QCEventHooks : MonoBehaviour
{
    [SerializeField] QuantumConsole _qc;

    void Start()
    {
        _qc = _qc
            ?? GetComponent<QuantumConsole>()
            ?? QuantumConsole.Instance;

        if (_qc)
        {
            _qc.OnActivate += OnActivate;
            _qc.OnDeactivate += OnDeactivate;
        }
    }

    void OnActivate()
    {
        HideMouseOnMenu.Instance.ShowMouse();
    }

    void OnDeactivate()
    {
        HideMouseOnMenu.Instance.HideMouse();
    }
}