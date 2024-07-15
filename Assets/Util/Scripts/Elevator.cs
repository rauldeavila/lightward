using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    public IntValue ElevatorStateKeeper;
    public int CurLevel;
    public GameObject Level1;
    public GameObject Level2;

    public bool GoingUp;
    public bool GoingDown;
    public bool GoingUpFast;
    public bool GoingDownFast;

    public Gate Gate1;
    public Gate Gate2;

    public GameObject UpSlamParticles;
    public GameObject UpParticlesPos;
    public GameObject DownSlamParticles;
    public GameObject DownParticlesPos;
    public GameObject FakeWalls;


    private FMOD.Studio.EventInstance ElevatorGears;
    private FMOD.Studio.EventInstance ElevatorGearsFast;
    private string ElevatorClank = "event:/game/00_game/elevator_clank";
    private string ElevatorSlam = "event:/game/00_game/elevator_slam";


    private Animator _anim;
    private PlayerController _wiz;
    private PlayerState _playerState;

    void Awake(){
        _wiz = FindObjectOfType<PlayerController>();
        _playerState = FindObjectOfType<PlayerState>();
        _anim = GetComponent<Animator>();
        ElevatorGears = FMODUnity.RuntimeManager.CreateInstance("event:/game/00_game/elevator_engine_2");
        ElevatorGearsFast = FMODUnity.RuntimeManager.CreateInstance("event:/game/00_game/elevator_engine_fast");
    }

    void Start(){
        if(ElevatorStateKeeper != null)
        {
            if(ElevatorStateKeeper.runTimeValue == 1)
            {
                Gate1.UnlockGate();
                Gate2.LockGate();
                if(this.transform.position == Level2.transform.position){
                    this.transform.position = Level1.transform.position;
                    CurLevel = 1;
                }
            }
            else if(ElevatorStateKeeper.runTimeValue == 2)
            {
                Gate2.UnlockGate();
                Gate1.LockGate();
                if(this.transform.position == Level1.transform.position){
                    this.transform.position = Level2.transform.position;
                    CurLevel = 2;
                }
            }
        } 
        else
        {
            if(CurLevel == 1)
            {
                Gate2.LockGate();
                Gate1.UnlockGate();
                if(this.transform.position == Level2.transform.position){
                    this.transform.position = Level1.transform.position;
                }
            } else if(CurLevel == 2)
            {
                Gate1.LockGate();
                Gate2.UnlockGate();
                if(this.transform.position == Level1.transform.position){
                    this.transform.position = Level2.transform.position;
                }
            }
        }
    }


    void FixedUpdate(){
        if(!GameState.Instance.Paused){
            if(GoingUp){
                Gate1.LockGate();
                this.transform.position = Vector2.MoveTowards(this.transform.position, Level2.transform.position, 8 * Time.deltaTime);
                if(this.transform.position == Level2.transform.position){
                    ElevatorStateKeeper.runTimeValue = 2;
                    Gate2.UnlockGate();
                    Stop();
                }
            } else if(GoingDown){
                Gate2.LockGate();
                this.transform.position = Vector2.MoveTowards(this.transform.position, Level1.transform.position, 8 * Time.deltaTime);
                if(this.transform.position == Level1.transform.position){
                    ElevatorStateKeeper.runTimeValue = 1;
                    Gate1.UnlockGate();
                    Stop();
                }
            } else if(GoingUpFast){
                Gate1.LockGate();
                this.transform.position = Vector2.MoveTowards(this.transform.position, Level2.transform.position, 30 * Time.deltaTime);
                if(this.transform.position == Level2.transform.position){
                    ElevatorStateKeeper.runTimeValue = 2;
                    Gate2.UnlockGate();
                    Stop();
                }
            } else if(GoingDownFast){
                Gate2.LockGate();
                this.transform.position = Vector2.MoveTowards(this.transform.position, Level1.transform.position, 30 * Time.deltaTime);
                if(this.transform.position == Level1.transform.position){
                    ElevatorStateKeeper.runTimeValue = 1;
                    Gate1.UnlockGate();
                    Stop();
                }
            }
        }
    }


    public void GoUp(){
        FakeWalls.SetActive(true);
        StopNoClank();
        ElevatorGears.start();
        CurLevel += 1;
        _anim.Play("elevator_up");
        GoingUp = true;
    }

    public void GoDown(){
        FakeWalls.SetActive(true);
        StopNoClank();
        ElevatorGears.start();
        CurLevel -= 1;
        _anim.Play("elevator_down");
        GoingDown = true;
    }

    public void GoUpFast(){
        FakeWalls.SetActive(true);
        StopNoClank();
        ElevatorGearsFast.start();
        CurLevel += 1;
        _anim.Play("elevator_up");
        GoingUpFast = true;
    }

    public void GoDownFast(){
        FakeWalls.SetActive(true);
        StopNoClank();
        ElevatorGearsFast.start();
        CurLevel -= 1;
        _anim.Play("elevator_down");
        GoingDownFast = true;
    }

    public void Stop(){
        FakeWalls.SetActive(false);
        ElevatorGears.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        ElevatorGearsFast.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        FMODUnity.RuntimeManager.PlayOneShot(ElevatorClank, transform.position);
        _anim.Play("elevator_static");
        if(GoingUpFast){
            CameraSystem.Instance.ShakeCamera(3);
            FMODUnity.RuntimeManager.PlayOneShot(ElevatorSlam, transform.position);
            Instantiate(UpSlamParticles, UpParticlesPos.transform.position, Quaternion.identity);
            ControllerRumble.Instance.StrongImpactRumble();
        } else if(GoingDownFast){
            CameraSystem.Instance.ShakeCamera(3);
            FMODUnity.RuntimeManager.PlayOneShot(ElevatorSlam, transform.position);
            Instantiate(DownSlamParticles, DownParticlesPos.transform.position, Quaternion.identity);
            ControllerRumble.Instance.StrongImpactRumble();
        }
        GoingUp = false;
        GoingDown = false;
        GoingUpFast = false;
        GoingDownFast = false;
    }

    public void StopNoClank(){
        ElevatorGears.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        ElevatorGearsFast.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _anim.Play("elevator_static");
        GoingUp = false;
        GoingDown = false;
        GoingUpFast = false;
        GoingDownFast = false;
    }




}
