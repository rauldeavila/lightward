using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticlesOnAnimator : MonoBehaviour {


    public bool grassParticles = false; // set on playerlanding.cs
    public ParticleSystem regularWalk;
    public ParticleSystem grassWalk;
    public ParticleSystem autumnWalk;

    public ParticleSystem particles1;
    public ParticleSystem particles2;
    public ParticleSystem particles3;
    public ParticleSystem particles4;
    public ParticleSystem particles5;
    public ParticleSystem particles6;
    public ParticleSystem particles7;
    public ParticleSystem particles8;
    public ParticleSystem particles9;
    public ParticleSystem particles10;
    public ParticleSystem particles11;
    public ParticleSystem particles12;
    public ParticleSystem particles13;
    public ParticleSystem particles14;
    public ParticleSystem particles15;

    private PlayerController controller;
    private PlayerState _state;

    void Awake(){
        controller = FindObjectOfType<PlayerController>();
        _state = FindObjectOfType<PlayerState>();
    }

    void Update(){
        if(controller.State.OnGrass){
            grassParticles = true;
        } else{
            grassParticles = false;
        }
    }

    public void PlayParticles(bool setPosition = false, float x = 0f, float y = 0f){
        if(setPosition){
            if(particles1 != null){
                particles1.transform.SetPositionAndRotation(new Vector3(this.transform.position.x, this.transform.position.y + y, this.transform.position.z),transform.rotation);
                particles1.Play();
            }
            if(particles2 != null){
                particles2.transform.SetPositionAndRotation(new Vector3(this.transform.position.x, this.transform.position.y + y, this.transform.position.z),transform.rotation);
                particles2.Play();
            }
            if(particles3 != null){
                particles3.transform.SetPositionAndRotation(new Vector3(this.transform.position.x, this.transform.position.y + y, this.transform.position.z),transform.rotation);
                particles3.Play();
            }
            if(particles4 != null){
                particles4.transform.SetPositionAndRotation(new Vector3(this.transform.position.x, this.transform.position.y + y, this.transform.position.z),transform.rotation);
                particles4.Play();
            }
            if(particles5 != null){
                particles5.transform.SetPositionAndRotation(new Vector3(this.transform.position.x, this.transform.position.y + y, this.transform.position.z),transform.rotation);
                particles5.Play();
            }
            if(particles6 != null){
                particles6.transform.SetPositionAndRotation(new Vector3(this.transform.position.x, this.transform.position.y + y, this.transform.position.z),transform.rotation);
                particles6.Play();
            }
            if(particles7 != null){
                particles7.transform.SetPositionAndRotation(new Vector3(this.transform.position.x, this.transform.position.y + y, this.transform.position.z),transform.rotation);
                particles7.Play();
            }
            if(particles8 != null){
                particles8.transform.SetPositionAndRotation(new Vector3(this.transform.position.x, this.transform.position.y + y, this.transform.position.z),transform.rotation);
                particles8.Play();
            }
            if(particles9 != null){
                particles9.transform.SetPositionAndRotation(new Vector3(this.transform.position.x, this.transform.position.y + y, this.transform.position.z),transform.rotation);
                particles9.Play();
            }
            if(particles10 != null){
                particles10.transform.SetPositionAndRotation(new Vector3(this.transform.position.x, this.transform.position.y + y, this.transform.position.z),transform.rotation);
                particles10.Play();
            }
            if(particles11 != null){
                particles11.transform.SetPositionAndRotation(new Vector3(this.transform.position.x, this.transform.position.y + y, this.transform.position.z),transform.rotation);
                particles11.Play();
            }
            if(particles12 != null){
                particles12.transform.SetPositionAndRotation(new Vector3(this.transform.position.x, this.transform.position.y + y, this.transform.position.z),transform.rotation);
                particles12.Play();
            }
            if(particles13 != null){
                particles13.transform.SetPositionAndRotation(new Vector3(this.transform.position.x, this.transform.position.y + y, this.transform.position.z),transform.rotation);
                particles13.Play();
            }
            if(particles14 != null){
                particles14.transform.SetPositionAndRotation(new Vector3(this.transform.position.x, this.transform.position.y + y, this.transform.position.z),transform.rotation);
                particles14.Play();
            }
            if(particles15 != null){
                particles15.transform.SetPositionAndRotation(new Vector3(this.transform.position.x, this.transform.position.y + y, this.transform.position.z),transform.rotation);
                particles15.Play();
            }
        } else{
            if(particles1 != null){
                particles1.Play();
            }
            if(particles2 != null){
                particles2.Play();
            }
            if(particles3 != null){
                particles3.Play();
            }
            if(particles4 != null){
                particles4.Play();
            }
            if(particles5 != null){
                particles5.Play();
            }
            if(particles6 != null){
                particles6.Play();
            }
            if(particles7 != null){
                particles7.Play();
            }
            if(particles8 != null){
                particles8.Play();
            }
            if(particles9 != null){
                particles9.Play();
            }
            if(particles10 != null){
                particles10.Play();
            }
            if(particles11 != null){
                particles11.Play();
            }
            if(particles12 != null){
                particles12.Play();
            }
            if(particles13 != null){
                particles13.Play();
            }
            if(particles14 != null){
                particles14.Play();
            }
            if(particles15 != null){
                particles15.Play();
            }
        }
    }

    public void PlayWalkParticles(){
        if(!_state.OnWater){
            if(grassParticles){
                if(RoomConfigurations.Instance.GetProfileName() == "Autumn")
                {
                    autumnWalk.Play();
                }
                else
                {
                    grassWalk.Play();
                }
            } else{
                regularWalk.Play();
            }
        }
    }

    public void PlayParticles1(){
        if(particles1 != null){
            particles1.Play();
        }
    }

    public void PlayParticles2(){
        if(particles2 != null){
            particles2.Play();
        }
    }
    
    public void PlayParticles3(){
        if(particles3 != null){
            particles3.Play();
        }
    }

    public void PlayParticles4(){
        if(particles4 != null){
            particles4.Play();
        }
    }

    public void PlayParticles5(){
        if(particles5 != null){
            particles5.Play();
        }
    }

    public void PlayParticles6(){
        if(particles6 != null){
            particles6.Play();
        }
    }

    public void PlayParticles7(){
        if(particles7 != null){
            particles7.Play();
        }
    }

    public void PlayParticles8(){
        if(particles8 != null){
            particles8.Play();
        }
    }

    public void PlayParticles9(){
        if(particles9 != null){
            particles9.Play();
        }
    }

    public void PlayParticles10(){
        if(particles10 != null){
            particles10.Play();
        }
    }
    
    public void PlayParticles11(){
        if(particles11 != null){
            particles11.Play();
        }
    }


}
