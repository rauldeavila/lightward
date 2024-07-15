using UnityEngine;

public class RandomAnimationSpeed : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (_animator != null)
        {
            // Randomize speed between 0.5 and 1.5
            _animator.speed = Random.Range(0.5f, 1.5f);
        }
    }
}