using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour {

    public SignalSender SignalSender;
    public UnityEvent SignalEvent;

    public void OnSignalRaised(){
        SignalEvent.Invoke();
    }

    private void OnEnable() {
        if(SignalSender != null)
        {
            SignalSender.RegisterListener(this);
        }
    }

    private void OnDisable() {
        if(SignalSender != null)
        {
        SignalSender.DeRegisterListener(this);
        }
    }



}
