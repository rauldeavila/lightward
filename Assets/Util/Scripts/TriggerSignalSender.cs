using System.Collections.Generic;
using UnityEngine;

public class TriggerSignalSender : MonoBehaviour {

    public List<ITriggerObserver> Listeners = new List<ITriggerObserver>();

    private void OnTriggerEnter2D(Collider2D collider) {
        Raise(collider);
    }

    public void Raise(Collider2D collider){
        for(int i = Listeners.Count - 1; i >= 0; i--){
            Listeners[i].OnTrigger(collider);
        }
    }


}