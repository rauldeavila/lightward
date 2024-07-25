using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

    public bool Stop = false;
    public string SoundPath;
    public bool Looping = false;
    public bool OnTrigger = false;

    public bool Is3D = false;

    void Start()
    {
        if(OnTrigger == false)
        {
            PlaySoundEvent();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("WizHitBox")){
            if(OnTrigger){
                if(SoundPath != null){
                    if(Stop){
                        SFXController.Instance.StopLoop(SoundPath);
                    } else if(Looping){
                        SFXController.Instance.PlayLoop(SoundPath);
                    } else {
                        if(Is3D)
                        {
                            SFXController.Instance.Play3D(SoundPath, this.transform.position);
                        }
                        else
                        {
                            SFXController.Instance.Play(SoundPath);
                        }
                    }
                }
            }
        }
    }

    public void PlaySoundEvent(){
        if(SoundPath != null){
            if(Looping){
                SFXController.Instance.PlayLoop(SoundPath);
            } else {
                if(Is3D)
                {
                    SFXController.Instance.Play3D(SoundPath, this.transform.position);
                }
                else
                {
                    SFXController.Instance.Play(SoundPath);
                }
            }
        }
    }


}
