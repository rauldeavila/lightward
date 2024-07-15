using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelly : MonoBehaviour
{
    public enum BossState
    {
        Sleeping,
        Idle,
        Attack1,
        Attack2,
        Awakening,
    }

    public bool Phase2 = false;
    public GameObject BeginParticles;
    public GameObject Rock;
    public GameObject RockP2;
    public GameObject Head;
    public GameObject HeadP2;
    public GameObject Skunner;
    public Transform ThrowingPosition;
    public Transform SkunnerPosition1;
    public Transform SkunnerPosition2;
    private BossState _currentState;
    private Animator _anim;
    private Enemy _enemy;
    private SkellySpikes _spikesScript;
    private bool _raisedFlag = false;
    private GameObject KilledParticles;
    private GameObject HeadParticles;
    private GameObject SkellyBoneParticles;
    private GameObject SkunnerAntecipationParticles;

    private int _amountOfRocks = 0; // for handling head throw moment

    void Awake()
    {
        SkunnerAntecipationParticles = Resources.Load<GameObject>("Particles/SkunnerAntecipationParticles");
        KilledParticles = Resources.Load<GameObject>("Particles/KilledParticles");
        HeadParticles = Resources.Load<GameObject>("Particles/SkellyHeadParticles");
        SkellyBoneParticles = Resources.Load<GameObject>("Particles/SkellyBoneParticles");
        _anim = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        _spikesScript = FindObjectOfType<SkellySpikes>();
    }
    void Start()
    {
        ChangeState(BossState.Sleeping);
    }

    void Update()
    {
        if(!Phase2)
        {
            if(_enemy.Health < 50)
            {
                Phase2 = true;
                _anim.SetBool("Phase2", true);
                GetComponentInChildren<BlinkSpriteRenderer>().BlinkLoopEachSecond();
            }
        }
        // Check current state and perform actions accordingly
        if(_raisedFlag){
            switch (_currentState)
            {
                case BossState.Sleeping:
                    _anim.Play("skelly");
                    _raisedFlag = false;
                    break;
                case BossState.Awakening:
                    // Code for idle state
                    _anim.Play("skelly_awakening");
                    _raisedFlag = false;
                    break;               
                case BossState.Idle:
                    // Code for idle state
                    _anim.Play("skelly_idle");
                    float idleTime = Random.Range(1.0f, 3.0f);
                    StartCoroutine(SelectRandomAttack(idleTime));
                    _raisedFlag = false;
                    break;
                case BossState.Attack1:
                    // Code for attack 1 state
                    _amountOfRocks = _amountOfRocks + 1;
                    _anim.Play("skelly_rock");
                    _raisedFlag = false;
                    break;
                case BossState.Attack2:
                    // Code for attack 2 state
                    _anim.Play("skelly_head_throw");
                    _raisedFlag = false;
                    _amountOfRocks = 0;
                    break;
                // Add cases for other states
            }
        }
    }

    public void ChangeState(BossState newState)
    {
        _currentState = newState;
        _raisedFlag = true;
    }

    public BossState GetCurrentState()
    {
        return _currentState;
    }

    public void ANIMATOR_InstantiateRock()
    {
        if(!Phase2)
        {
            Instantiate(Rock, ThrowingPosition.position, ThrowingPosition.rotation);
        }
        else
        {
            Instantiate(RockP2, ThrowingPosition.position, ThrowingPosition.rotation);
        }
    }

    public void ANIMATOR_InstantiateHead()
    {
        if(!Phase2)
        {
            GameObject headInstance = Instantiate(Head, ThrowingPosition.position, ThrowingPosition.rotation);
            headInstance.transform.parent = null;
        }
        else
        {
            GameObject headInstance = Instantiate(HeadP2, ThrowingPosition.position, ThrowingPosition.rotation);
            headInstance.transform.parent = null;
        }
    }

    public void ANIMATOR_ActivateSpikes()
    {
        _spikesScript.ActivateSpikes();
    }

    public void ANIMATOR_DeactivateSpikes()
    {
        _spikesScript.DeactivateSpikes();
    }

    public void InstantiateSkunners() // Called from head script
    {
        if(SkunnerAntecipationParticles) Instantiate(SkunnerAntecipationParticles, SkunnerPosition1.position, Quaternion.identity);
        if(SkunnerAntecipationParticles) Instantiate(SkunnerAntecipationParticles, SkunnerPosition2.position, Quaternion.identity);
        Invoke("SkunnersSpawning", 1f);
    }
    
    void SkunnersSpawning()
    {
        _anim.Play("skelly_summoning");
        GameObject skunner1 = Instantiate(Skunner, SkunnerPosition1.position, SkunnerPosition1.rotation);
        GameObject skunner2 = Instantiate(Skunner, SkunnerPosition2.position, SkunnerPosition2.rotation);
        skunner1.transform.parent = null;
        skunner2.transform.parent = null;
    }
    
    public void Wake()
    {
        if(_currentState == BossState.Sleeping)
        {
            ChangeState(BossState.Awakening);
            SFXController.Instance.Play("event:/game/02_graveyard/skely/skelly_wake_sequence");
        }
    }

    public void ReturnHead()
    {
        _anim.Play("skelly_return_head");
        FMODUnity.RuntimeManager.PlayOneShot("event:/game/01_forest/spike_falling_to_ground", transform.position);
    }

    public void ANIMATOR_ThrowSFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/game/02_graveyard/skely/skelly_throw", transform.position);
    }

    public void ANIMATOR_StartSkellyMusic()
    {
        // FMODUnity.RuntimeManager.PlayOneShot("event:/game/02_graveyard/skely/skelly_placeholder_music", transform.position);
        SFXController.Instance.PlayMusic("event:/game/02_graveyard/skely/skelly_placeholder_music");
        Instantiate(BeginParticles, this.transform.position, Quaternion.identity);
        CameraSystem.Instance.ShakeCamera(2);
    }

    public void EVENT_DestroyHead()
    {
        if(FindObjectOfType<SkellyHeadAttacks>())
        {
            Vector3 posForSmokes = FindObjectOfType<SkellyHeadAttacks>().transform.position;
            Destroy(FindObjectOfType<SkellyHeadAttacks>().gameObject.transform.parent.parent.gameObject);
            if(KilledParticles) Instantiate(KilledParticles, posForSmokes, Quaternion.identity);
            if(HeadParticles) Instantiate(HeadParticles, posForSmokes, Quaternion.identity);
        }
    }

    public void EVENT_DestroyReturnHeadObject()
    {
        Destroy(FindObjectOfType<ReturnHeadToSkelly>().gameObject);
    }

    public void EVENT_StopSkellyMusic()
    {
        SFXController.Instance.PlayMusic("event:/music/catacombs");
        if(SkellyBoneParticles) Instantiate(SkellyBoneParticles, this.transform.position, Quaternion.identity);
    }

    IEnumerator SelectRandomAttack(float delay) 
    {
        yield return new WaitForSeconds(delay); 
        if(_amountOfRocks < 5)
        {
            ChangeState(BossState.Attack1);
        } else {
            ChangeState(BossState.Attack2);
        }
    }


}
