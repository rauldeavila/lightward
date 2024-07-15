using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EBPatrol : MonoBehaviour {

    public float MoveSpeed = 2f;
    public bool FlipOnWalls = true;
    public bool FlipOnHoles = true;
    public bool MoveWhileNotGrounded = false;
    public bool SideToSide  = false;
    public bool Flyer = false;
    public bool FollowTargets = false;
    [ShowIf("Flyer")]
    public Vector2 MoveDirection = new Vector2(1f, 0.25f);
    [ShowIf("Flyer")]
    public bool ChangeYDirectionOnRoofAndGround = false;
    [ShowIf("FollowTargets")]
    public List<GameObject> Targets = new List<GameObject>();
    
    public Vector3 UpOffset;
    public float UpRectWidth;
    public float UpRectHeight;
    public Vector3 DownOffset;
    public float DownRectWidth;
    public float DownRectHeight;
    public Vector3 LeftOffset;
    public float LeftRectWidth;
    public float LeftRectHeight;
    public Vector3 RightOffset;
    public float RightRectWidth;
    public float RightRectHeight;

    private bool _upCheck = false;
    private bool _downCheck = false;
    private bool _leftCheck = false;
    private bool _rightCheck = false;


    public Vector3 LeftHoleOffset;
    public float LeftHoleRectWidth;
    public float LeftHoleRectHeight;
    public Vector3 RightHoleOffset;
    public float RightHoleRectWidth;
    public float RightHoleRectHeight;
    private bool _leftHoleCheck = false;
    private bool _rightHoleCheck = false;

    private Enemy _enemy;
    private LayerMask groundLayer;
    private int currentTargetIndex = 0;

    private bool _canChangeYDirection = true;

    void Awake(){
        _enemy = GetComponent<Enemy>();
    }

    void Start(){
        if(Flyer){
            MoveWhileNotGrounded = true;
        }
        groundLayer = LayerMask.GetMask("Ground", "Breakable", "DetectsOnlyEnemies");
    }

    void Update() {
        UpdateCheckers();
        if((!MoveWhileNotGrounded && _enemy.Grounded) || MoveWhileNotGrounded){
            HandleFlipOnWalls();
            HandleFlipOnHoles();
            HandleDirectionOnRoofAndGround();
        }
    }

    void FixedUpdate() {
        if(_enemy.CanTakeHit){
            if((!MoveWhileNotGrounded && _enemy.Grounded) || MoveWhileNotGrounded){
                Move();
            }
        }
    }


    void UpdateCheckers(){
        _upCheck = Physics2D.OverlapBox(transform.position + UpOffset, new Vector2 (UpRectWidth, UpRectHeight), 0f, groundLayer);
        _downCheck = Physics2D.OverlapBox(transform.position + DownOffset, new Vector2 (DownRectWidth, DownRectHeight), 0f, groundLayer);
        _leftCheck = Physics2D.OverlapBox(transform.position + LeftOffset, new Vector2 (LeftRectWidth, LeftRectHeight), 0f, groundLayer);
        _rightCheck = Physics2D.OverlapBox(transform.position + RightOffset, new Vector2 (RightRectWidth, RightRectHeight), 0f, groundLayer);
        _leftHoleCheck = Physics2D.OverlapBox(transform.position + LeftHoleOffset, new Vector2 (LeftHoleRectWidth, LeftHoleRectHeight), 0f, groundLayer);
        _rightHoleCheck = Physics2D.OverlapBox(transform.position + RightHoleOffset, new Vector2 (RightHoleRectWidth, RightHoleRectHeight), 0f, groundLayer);
    }

    void HandleFlipOnWalls(){
        if(FlipOnWalls){
            if(_enemy.FacingRight && _rightCheck){
                _enemy.Flip();
                MoveDirection.x= -MoveDirection.x;
            } else if(!_enemy.FacingRight && _leftCheck){
                _enemy.Flip();
                MoveDirection.x= -MoveDirection.x;
            }
        }
    }

    void HandleFlipOnHoles(){
        if(FlipOnHoles){
            if(_enemy.FacingRight && !_rightHoleCheck){
                _enemy.Flip();
                MoveDirection.x= -MoveDirection.x;
            } else if(!_enemy.FacingRight && !_leftHoleCheck){
                _enemy.Flip();
                MoveDirection.x= -MoveDirection.x;
            }
        }
    }


    void HandleDirectionOnRoofAndGround(){
        if(ChangeYDirectionOnRoofAndGround){
            if(_upCheck || _downCheck){
                if(_canChangeYDirection){
                    _canChangeYDirection = false;
                    Invoke("ReenableYDirection", 0.3f);
                    MoveDirection = new Vector2(MoveDirection.x, -MoveDirection.y);
                }
            }
        }
    }

    void ReenableYDirection(){
        _canChangeYDirection = true;
    }

void Move() {
    if (SideToSide){
        _enemy.Rb.velocity = new Vector2(MoveDirection.x, 0f) * MoveSpeed;
    } else if (FollowTargets) {
        // Check if there are any targets
        if (Targets.Count > 0) {
            Vector3 direction = (Targets[currentTargetIndex].transform.position - transform.position).normalized;
            _enemy.Rb.velocity = direction * MoveSpeed;

            if (Vector3.Distance(transform.position, Targets[currentTargetIndex].transform.position) < 0.1f) {
                float targetRotationZ = Targets[currentTargetIndex].transform.rotation.eulerAngles.z;
                currentTargetIndex++;

                if (currentTargetIndex >= Targets.Count){
                    // Wrap around to the first target
                    currentTargetIndex = 0;
                }
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, targetRotationZ);
            }
        }
    } else if(Flyer){
        _enemy.Rb.velocity = MoveDirection * MoveSpeed;
    }
}


    private void OnDrawGizmos(){
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position + UpOffset, new Vector3(UpRectWidth, UpRectHeight, 0));
        Gizmos.DrawWireCube(transform.position + DownOffset, new Vector3(DownRectWidth, DownRectHeight, 0));
        Gizmos.DrawWireCube(transform.position + LeftOffset, new Vector3(LeftRectWidth, LeftRectHeight, 0));
        Gizmos.DrawWireCube(transform.position + RightOffset, new Vector3(RightRectWidth, RightRectHeight, 0));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + LeftHoleOffset, new Vector3(LeftHoleRectWidth, LeftHoleRectHeight, 0));
        Gizmos.DrawWireCube(transform.position + RightHoleOffset, new Vector3(RightHoleRectWidth, RightHoleRectHeight, 0));
    }


}
