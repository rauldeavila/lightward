using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SkellyHeadAttacks : MonoBehaviour
{
    public bool NoSkunnersP2 = false;
    public CircleCollider2D CircleCollider;
    public GameObject ParticlesTop;
    public GameObject ParticlesBottom;
    
    public GameObject ParticlesL2;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D _telegraphLight;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D _telegraphLight2;

    [SerializeField] private float _maxIntensity = 0.3f;
    [SerializeField] private float _buildupDuration = 1.5f; // Total duration of buildup
    [SerializeField] private AnimationCurve _intensityCurve; // Custom intensity curve for easing
    [SerializeField] private float _rotationSpeed = 50f;
    [SerializeField] private float _fadeDuration = 1.0f; // Duration for light to fade away after buildup

    private float _currentIntensity = 0f;
    private float _buildupTimer = 0f;
    private bool _isBuildingUp = false;
    private bool _isRotating = false;
    private float _rotationTarget = 0f;
    public Rigidbody2D HeadRigidbody;

    public GameObject ColliderObject;
    private bool _isReturningToBody = false;

    private void Start()
    {
        _telegraphLight.intensity = _currentIntensity;
        _telegraphLight2.intensity = _currentIntensity;
    }

    void FixedUpdate()
    {
        if(_isReturningToBody)
        {
            float currentForce = 500f * Mathf.Pow(100f, Time.deltaTime * 5);
            HeadRigidbody.AddForce(Vector2.left * currentForce, ForceMode2D.Force);
        }
    }
    
    private void Update()
    {
        // Check if we need to start building up intensity
        if (_isBuildingUp)
        {
            // Increment the buildup timer
            _buildupTimer += Time.deltaTime;

            // Calculate the normalized time
            float normalizedTime = Mathf.Clamp01(_buildupTimer / _buildupDuration);

            // Evaluate intensity using the custom curve
            _currentIntensity = _intensityCurve.Evaluate(normalizedTime) * _maxIntensity;

            // Update the light's intensity
            _telegraphLight.intensity = _currentIntensity;
            _telegraphLight2.intensity = _currentIntensity;

            // Check if we've reached the end of buildup
            if (_buildupTimer >= _buildupDuration)
            {
                // Stop building up intensity
                _isBuildingUp = false;

                // Start rotating the sprite
                _isRotating = true;
            }
        }

        // Rotate the sprite
        if (_isRotating)
        {
            if (_rotationTarget == -70f)
            {
                transform.Rotate(0, 0, -_rotationSpeed * Time.deltaTime);
                if (transform.eulerAngles.z <= 290) 
                {
                    _isRotating = false;
                    Invoke("ToggleEarthquakeCamFX",0f);
                    DisableCollider();
                }
            }
            else if(_rotationTarget == 9f)
            {
                transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
                if (transform.eulerAngles.z <= _rotationTarget || transform.eulerAngles.z >= _rotationTarget + 360)
                {
                    _isRotating = false;
                    Invoke("ToggleEarthquakeCamFX", 0f);
                    DisableCollider();
                }
            }

        }

        // Fade away the light after rotation
        if (!_isBuildingUp && !_isRotating)
        {
            _telegraphLight.intensity -= Time.deltaTime / _fadeDuration;
            if (_telegraphLight.intensity <= 0f)
            {
                // Ensure intensity does not go below 0
                _telegraphLight.intensity = 0f;
            }
            _telegraphLight2.intensity -= Time.deltaTime / _fadeDuration;
            if (_telegraphLight2.intensity <= 0f)
            {
                // Ensure intensity does not go below 0
                _telegraphLight2.intensity = 0f;
            }
        }
    }

    // Call this method to instantiate laser from the top
    [Button]
    public void InstantiateLaserFromTheTop()
    {
        transform.rotation = Quaternion.Euler(0, 0, -70f);
        ParticlesTop.SetActive(true);
        Invoke("DisableParticles", 2f);
        Invoke("ToggleEarthquakeCamFX", 1f);
        Invoke("PlayLaserSFX", 0.5f);
        Invoke("EnableCollider", 1f);
        _isBuildingUp = true;
        _buildupTimer = 0f; // Reset the buildup timer
        _rotationTarget = 9f;
    }

    // Call this method to instantiate laser from the bottom
    [Button]
    public void InstantiateLaserFromTheBottom()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0f);
        ParticlesBottom.SetActive(true);
        Invoke("DisableParticles", 2f);
        Invoke("ToggleEarthquakeCamFX", 1f);
        Invoke("PlayLaserSFX", 0.5f);
        Invoke("EnableCollider", 1f);
        _isBuildingUp = true;
        _buildupTimer = 0f; // Reset the buildup timer
        _rotationTarget = -70f;
    }


    [Button]
    public void InstantiateLaserLevel2()
    {
        GetComponent<Animator>().Play("skelly_head_lvl2");
        ParticlesL2.SetActive(true); // change particles
        Invoke("DisableParticles", 2f);
        Invoke("ToggleEarthquakeCamFX", 1f);
        Invoke("PlayLaserSFX", 0.5f);
        Invoke("EnableCollider", 1f);
        _isBuildingUp = true;
    }

    public void ANIMATOR_StopLaser()
    {
        Invoke("ToggleEarthquakeCamFX", 0f);
        DisableCollider();
        HeadRigidbody.gravityScale = 1f;
        FindObjectOfType<ReturnHeadToSkelly>().CanCheckCollision = true;
        Invoke("SetLightLvl2ToZero", 1.5f);
    }

    void SetLightLvl2ToZero()
    {
        _telegraphLight2.intensity = 0f;
    }
    public void PlayLaserSFX()
    {
        SFXController.Instance.Play("event:/game/00_game/skelly_laser");
    }

    [Button]
    public void TopBottomFall(){
        HeadRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
        Invoke("InstantiateLaserFromTheTop", 1f);
        Invoke("InstantiateLaserFromTheBottom", 4f);
        Invoke("MakeHeadFall", 7f);
    }

    void ToggleEarthquakeCamFX()
    {
        CameraSystem.Instance.ToggleEarthquake();
    }

    void DisableParticles(){
        ParticlesBottom.SetActive(false);
        ParticlesTop.SetActive(false);
    }

    void MakeHeadFall(){
        HeadRigidbody.gravityScale = 1f;
        if(NoSkunnersP2 == false)
        {
            Invoke("ReturnHeadToBody", 8f);
            Invoke("InstantiateSkunners", 2f);
        }
        else
        {
            Invoke("ReturnHeadToBody", 5f);
        }
    }

    void InstantiateSkunners()
    {
        FindObjectOfType<Skelly>().InstantiateSkunners();
    }

    void ReturnHeadToBody(){
        FindObjectOfType<ReturnHeadToSkelly>().CanCheckCollision = true;
        HeadRigidbody.constraints = RigidbodyConstraints2D.None;
        HeadRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        HeadRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        HeadRigidbody.gravityScale = 0f;
        // CircleCollider.enabled = false;

        _isReturningToBody = true;

    }

    void EnableCollider()
    {
        ColliderObject.SetActive(true);
    }
    void DisableCollider()
    {
        ColliderObject.SetActive(false);
    }
}
