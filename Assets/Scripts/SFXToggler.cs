using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SFXToggler : MonoBehaviour
{

    public string EventPath;

    public bool is3D = false;
    [ShowIf("is3D")]
    public Transform InstantiatePositionFor3D;
    public bool Loop = false;

    public bool InstantiateOnAwake = false;
    public bool InstantiateOnEnable = false;
    public bool StopOnDisable = false;

    void Start(){
        if(InstantiateOnAwake)
        {
            InstantiateEvent();
        }
    }

    void OnEnable()
    {
        if(InstantiateOnEnable)
        {
            Invoke("InstantiateEvent", 0.5f);
        }
    }

    void OnDisable()
    {
        if(StopOnDisable)
        {
            StopEvent();
        }
    }
    public void InstantiateEvent(){
        if(is3D)
        {
            SFXController.Instance.Play3D(EventPath, new Vector3(InstantiatePositionFor3D.position.x, InstantiatePositionFor3D.position.y, InstantiatePositionFor3D.position.z));
        }
        if(Loop)
        {
            SFXController.Instance.PlayLoop(EventPath);
        } else {
            SFXController.Instance.Play(EventPath);
        }
    }

    public void StopEvent(){
        if(is3D)
        {
            SFXController.Instance.Stop3D(EventPath);
        }
        if(Loop)
        {
            SFXController.Instance.StopLoop(EventPath);
        } else {
            SFXController.Instance.Stop(EventPath);
        }
    }




}
