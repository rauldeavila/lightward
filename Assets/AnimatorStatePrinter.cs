using UnityEngine;

public class AnimatorStatePrinter : MonoBehaviour
{
    public bool TurnedOn = false;
    private Animator _animator;
    private AnimatorStateInfo _currentState;
    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        if(TurnedOn){
            _animator = GetComponent<Animator>();
            _rigidbody2D = PlayerController.Instance.GetComponent<Rigidbody2D>();
            _currentState = _animator.GetCurrentAnimatorStateInfo(0);
            Debug.Log("Initial State: " + _currentState.fullPathHash);
        }
    }

    void FixedUpdate()
    {
        if(TurnedOn){
            float horizontal = Mathf.Abs(_rigidbody2D.velocity.x);
            _animator.SetFloat("horizontal", horizontal);

            AnimatorStateInfo newState = _animator.GetCurrentAnimatorStateInfo(0);

            if (newState.fullPathHash != _currentState.fullPathHash)
            {
                _currentState = newState;
                Debug.Log("State changed to: " + _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name + " | Current 'horizontal' parameter value: " + horizontal);
            }
        }
    }
}
