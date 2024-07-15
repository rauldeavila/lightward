using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBear : MonoBehaviour {

    public float RunSpeed = 5f;
    public bool BattleStarted = false;
    public int SecondPhaseHealth = 25;
    public int ThirdPhaseHealth = 10;
    public bool SecondPhase = false;
    public bool ThirdPhase = false;
    public int TackleNumber = 0;
    private Animator _anim;
    private Enemy _enemy;
    // tackle
    public float TackleSpeed = 10f;
    public float TackleDistance = 5f; 
    public float WallDistance = 2f;
    public List<GameObject> OriginalSpikePositions;
    private List<GameObject> _spikePositions;
    public GameObject SpikePrefab;
    private bool _isTackling = false;
    private LayerMask _wallLayerMask;
    private bool _canCheckForWall = true;
    private float _defaultRunSpeed;

    public GameObject LeftTopPos;
    public GameObject LeftDownPos;
    public GameObject RightTopPos;
    public GameObject RightDownPos;
    public GameObject DumbFlyerFacingRight;
    public GameObject DumbFlyerFacingLeft;


    public static BossBear Instance;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        }       
        _anim = GetComponentInChildren<Animator>();
        _enemy = GetComponent<Enemy>();
        _wallLayerMask = LayerMask.GetMask("Ground");
        _defaultRunSpeed = RunSpeed;
    }

    void Start(){
        _spikePositions = new List<GameObject>(OriginalSpikePositions);
    }

    void Update() {
        if(_enemy.Health <= ThirdPhaseHealth && !ThirdPhase){
            ThirdPhase = true;
            SecondPhase = true;
            _anim.SetBool("third_phase", true);
            _anim.SetBool("second_phase", true);
            RunSpeed = 12f;
            TackleSpeed = 15f;
        } else if(_enemy.Health <= SecondPhaseHealth && !SecondPhase){
            SecondPhase = true;
            _anim.SetBool("second_phase", true);
            RunSpeed = 10f;
            TackleSpeed = 15f;
        }
        if(_enemy.FacingRight){
            RunSpeed = _defaultRunSpeed;
        } else {
            RunSpeed = -_defaultRunSpeed;
        }
        if (!_isTackling && BattleStarted) {
            CheckForTackle();
        } else if(_isTackling && _canCheckForWall){
            CheckForWall();
        }
    }


    public void HitByPlayer(){
        if(BattleStarted == false){
            SFXController.Instance.Play("event:/game/01_forest/boss_music");
            BattleStarted = true;
        }
    }

    public void Killed(){
        SFXController.Instance.Stop("event:/game/01_forest/boss_music");
    }

    void SpawnRandomSpikes() {
        List<GameObject> randomPositions = new List<GameObject>();

        int numOfSpikes;
        if(!SecondPhase){
            numOfSpikes = 5;
        } else {
            numOfSpikes = 10;
        }
        for (int i = 0; i < numOfSpikes && _spikePositions.Count > 0; i++) {
            int randomIndex = UnityEngine.Random.Range(0, _spikePositions.Count);
            randomPositions.Add(_spikePositions[randomIndex]);
            _spikePositions.RemoveAt(randomIndex); // Remove so it won't be selected again
        }

        // Instantiate spikes at the random positions
        foreach (GameObject position in randomPositions) {
            StartCoroutine(SpawnSpikeWithDelay(position.transform.position));
        }

        // Reset the spike positions for next call
        _spikePositions = new List<GameObject>(OriginalSpikePositions);
    }

    IEnumerator SpawnSpikeWithDelay(Vector3 position) {
        float delay = UnityEngine.Random.Range(0f, 0.3f);
        yield return new WaitForSeconds(delay);
        Instantiate(SpikePrefab, position, Quaternion.identity);
    }

    public void SpawnRandomDumbFlyer() {
        // List of game objects for each position
        List<GameObject> positions = new List<GameObject>() { LeftTopPos, LeftDownPos, RightTopPos, RightDownPos };

        // Randomly choose a position
        GameObject chosenPosition = positions[Random.Range(0, positions.Count)];

        // Determine whether to spawn right or left facing flyer
        GameObject flyerToSpawn;
        if (chosenPosition == LeftTopPos || chosenPosition == LeftDownPos) {
            flyerToSpawn = DumbFlyerFacingRight;
        } else {
            flyerToSpawn = DumbFlyerFacingLeft;
        }

        // Instantiate the flyer at the chosen position
        Instantiate(flyerToSpawn, chosenPosition.transform.position, Quaternion.identity);
    }

    public void SpawnSpikesPhase3() {
        StartCoroutine(SpawnSpikesAtFixedPosition());
    }

    IEnumerator SpawnSpikesAtFixedPosition() {
        GameObject fixedPosition;
        if(_enemy.FacingRight){
            fixedPosition = _spikePositions.Find(pos => pos.name == "position_06");
        } else {
            fixedPosition = _spikePositions.Find(pos => pos.name == "position_15"); 
        }
        if (fixedPosition != null) { // Make sure it exists
            for (int i = 0; i < 7; i++) { // Spawn 7 spikes
                Instantiate(SpikePrefab, fixedPosition.transform.position, Quaternion.identity); // Use your own prefab
                yield return new WaitForSeconds(0.4f);
            }
        }
    }

    public void Sleep(){
        if(BattleStarted){
            _anim.Play("boss_bear_wake");
        }
    }

    public void Run(){
        _isTackling = false;
        _enemy.SetVelocityNoRb(RunSpeed, 0f);
    }

    public void RunFaster(){
        _isTackling = false;
        _enemy.SetVelocityNoRb(RunSpeed, 0f);
    }

    public void RunThird(){
        _isTackling = false;
        _enemy.SetVelocityNoRb(RunSpeed, 0f);
    }

    public void TackleToLaying(){
        _enemy.SetVelocityNoRb(RunSpeed, 0f);
    }

    public void TackleToRun(){
        _enemy.SetVelocityNoRb(RunSpeed, 0f);
    }

    public void WallToTired(){
        _enemy.SetVelocityNoRb(RunSpeed, 0f);
    }

    public void FreezeMovement(){
        _enemy.SetVelocityNoRb(0f, 0f);
    }



    private void CheckForTackle() {
        if (IsNearWall()) {
            if(!SecondPhase && !ThirdPhase){
                Tackle();
            } else if(SecondPhase && !ThirdPhase) {
                Tackle2();
            } else if(ThirdPhase){
                Tackle3();
            }
        }
    }

    private void CheckForWall(){
        if (IsOnWall()){
            BossBearSprite.Instance.PlayTackle();
            SpawnRandomSpikes();
            _canCheckForWall = false;
            EndTackle();
        }
    }

    public void Tackle() {
        _isTackling = true;
        _enemy.SetVelocityNoRb(0f, 0f);
        _anim.Play("boss_bear_tackle"); // Play the tackle animation
    }

    public void Tackle2() {
        _isTackling = true;
        _enemy.SetVelocityNoRb(0f, 0f);
        _anim.Play("boss_bear_tackle_phase_2"); // Play the tackle animation
    }

    public void Tackle3() {
        _isTackling = true;
        _enemy.SetVelocityNoRb(0f, 0f);
        _anim.Play("boss_bear_tackle_phase_3"); // Play the tackle animation
    }

    public void TackleForce(){
        float tackleDirection = Mathf.Sign(RunSpeed);
        _enemy.SetVelocityNoRb(TackleSpeed * tackleDirection, 0f);
    }

    public void EndTackle() {
        _enemy.SetVelocityNoRb(0f, 0f);
        if(!SecondPhase){
            BossBearSprite.Instance.PlayTween();
            CameraSystem.Instance.ShakeCamera(2);
            Invoke("PlayLayingAnimation", 0.5f);
        } else if(!ThirdPhase){
            BossBearSprite.Instance.PlayTween2();
            CameraSystem.Instance.ShakeCamera(2);
            Invoke("PlayRunSecondPhase", 0.4f);
        } else {
            BossBearSprite.Instance.PlayTween2();
            CameraSystem.Instance.ShakeCamera(2);
            Invoke("PlayRunThirdPhase", 0.4f);
        }
    }

    void PlayLayingAnimation(){
        _anim.Play("boss_bear_tackle_to_laying");
        _enemy.Flip();
        _canCheckForWall = true; // depois do flip senao vai loopar
        CancelInvoke();
    }

    void PlayRunSecondPhase(){
        TackleNumber = TackleNumber + 1;
        if(TackleNumber >= 3){
            TackleNumber = 0;
            // cansou
            _anim.Play("boss_bear_wall_to_tired");
            _enemy.Flip();
            _canCheckForWall = true; // depois do flip senao vai loopar
            CancelInvoke();
        } else {
            _anim.Play("boss_bear_tackle_to_run");
            _enemy.Flip();
            _canCheckForWall = true; // depois do flip senao vai loopar
            CancelInvoke();
        }
    }

    void PlayRunThirdPhase(){
        TackleNumber = TackleNumber + 1;
        if(TackleNumber >= 5){
            TackleNumber = 0;
            // cansou
            _anim.Play("boss_bear_tackle_to_spikes");
            _enemy.Flip();
            _canCheckForWall = true; // depois do flip senao vai loopar
            CancelInvoke();
        } else {
            _anim.Play("boss_bear_tackle_to_run");
            _enemy.Flip();
            _canCheckForWall = true; // depois do flip senao vai loopar
            CancelInvoke();
        }
    }

    private bool IsNearWall() {
        if(_enemy.FacingRight){
            Vector2 direction = Vector2.right;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, TackleDistance, _wallLayerMask);
            if (hit.collider != null && hit.collider.CompareTag("Grass")) {
                return true;
            }
        } else {
            Vector2 direction = Vector2.left;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, TackleDistance, _wallLayerMask);
            if (hit.collider != null && hit.collider.CompareTag("Grass")) {
                return true;
            }
        }
        return false;
    }

    private bool IsOnWall() {
        if(_enemy.FacingRight){
            Vector2 direction = Vector2.right;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, WallDistance, _wallLayerMask);
            if (hit.collider != null && hit.collider.CompareTag("Grass")) {
                return true;
            }
        } else {
            Vector2 direction = Vector2.left;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, WallDistance, _wallLayerMask);
            if (hit.collider != null && hit.collider.CompareTag("Grass")) {
                return true;
            }
        }
        return false;
    }


    void OnDrawGizmos() {
        Vector3 origin = transform.position;
        Gizmos.color = Color.red;
        Vector3 direction = transform.right * Mathf.Sign(RunSpeed);
        Gizmos.DrawRay(origin, direction * TackleDistance);
        Gizmos.color = Color.blue;
        direction = transform.right * Mathf.Sign(-RunSpeed);
        Gizmos.DrawRay(origin, direction * TackleDistance);

        origin = new Vector3(this.transform.position.x, this.transform.position.y - 1f, this.transform.position.z);
        Gizmos.color = Color.cyan;
        direction = transform.right * Mathf.Sign(RunSpeed);
        Gizmos.DrawRay(origin, direction * WallDistance);
        Gizmos.color = Color.green;
        direction = transform.right * Mathf.Sign(-RunSpeed);
        Gizmos.DrawRay(origin, direction * WallDistance);
    }

        



}
