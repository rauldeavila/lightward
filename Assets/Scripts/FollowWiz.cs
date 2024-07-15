using System.Collections;
using UnityEngine;

public class FollowWiz : MonoBehaviour
{
    public float Delay;  // Time before the ghost starts staring
    public float StareTime;  // Time the ghost will stare at Wiz before chasing
    public float InitialImpulseForce = 10f;  // Initial force applied
    public Vector2 InitialDirection = Vector2.up;  // Initial direction of the force
    public float ChasingSpeed = 0.1f;  // Speed at which the ghost will chase Wiz
    public bool IsHomingMissile = false;  // If true, the ghost will behave like a homing missile

    private Vector2 _wizPosition;
    private bool _isFollowing = false;
    private Vector2 _homingMissileDirection;  // Direction for the homing missile

    public GameObject GroundDetection;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(InitialDirection.normalized * InitialImpulseForce, ForceMode2D.Impulse);
        GetComponent<Enemy>().FlipBasedOnWizPos();
        StartCoroutine(StartFollowingAfterDelay());
    }

private IEnumerator StartFollowingAfterDelay()
{
    yield return new WaitForSeconds(Delay);

    // Stop and stare
    GetComponent<Rigidbody2D>().velocity = Vector2.zero;

    // Wait for the first 2/3 of the StareTime
    yield return new WaitForSeconds(StareTime * (2f / 3f));

    if (IsHomingMissile)
    {
        // Initial Wiz position for homing direction
        _wizPosition = PlayerController.Instance.transform.position;
        _homingMissileDirection = (_wizPosition - (Vector2)transform.position).normalized;

        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition - _homingMissileDirection * 1; // 2 units in the opposite direction

        float startTime = Time.time;
        float lerpDuration = StareTime / 3f; // Last third of the StareTime
        float elapsed = 0f;

        while (elapsed < lerpDuration)
        {
            float fraction = elapsed / lerpDuration;
            transform.position = Vector2.Lerp(startPosition, endPosition, fraction);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Grab Wiz's position right before launching
        _wizPosition = PlayerController.Instance.transform.position;
        _homingMissileDirection = (_wizPosition - (Vector2)transform.position).normalized;

        // Now, prepare to chase
        GetComponent<Enemy>().DontFlipBasedOnWizPos = true;
        Invoke("EnableGroundDetection", 0.1f);
        _isFollowing = true;
    }
    else
    {
        // Prepare to chase for non-homing missile case
        _isFollowing = true;
    }
}

void EnableGroundDetection(){
    if(GroundDetection){
        GroundDetection.SetActive(true);
    }
}




    private void FixedUpdate()
    {
        if (_isFollowing)
        {
            if (IsHomingMissile)
            {
                transform.position += (Vector3)_homingMissileDirection * ChasingSpeed;
            }
            else
            {
                _wizPosition = PlayerController.Instance.transform.position;
                transform.position = Vector2.MoveTowards(transform.position, _wizPosition, ChasingSpeed);
            }
        }
    }
}
