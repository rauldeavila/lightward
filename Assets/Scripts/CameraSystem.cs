using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Cinemachine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using Pathfinding;

public class CameraSystem : MonoBehaviour {

    #region unity

    // 1 = 384x216 - NATIVE = FOV: 6.75
    // 2 = 320x180 - MEIDUM = FOV: 5.625
    // 3 = 240x135 - STRONG = FOV: 4.21875
    // 4 = 192x108 - STRONGEST = FOV: 

    public Transform Hero;
    public PolygonCollider2D GenericCamBoundariesForAwake;
    public float FixedXPosition = 0f;
    public float FixedYPosition = 0f;

    public int CurrentZoom = 1; 
    public bool Handheld = false;
    public bool Earthquake = false;
    public bool LookingUp = false;
    public bool LookingDown = false;

    private CinemachineVirtualCamera cam;
    private CinemachineBrain _brain;
    private float _shakeIntensity = 2f;
    private float _shakeTime = 0.2f;

    private float _timer = 0f;
    private CinemachineBasicMultiChannelPerlin _shakeChannel;

    private NoiseSettings _handheldProfile;
    private NoiseSettings _regularProfile;
 
    public static CameraSystem Instance;

    private GameObject _fireballRightChild;
    private GameObject _fireballLeftChild;
    private GameObject _shakeSoft;
    private GameObject _shakeMedium;
    private GameObject _shakeHard;
    private GameObject _earthquake;

    private const float _nativeFOV = 5.625f;
    private const float _farFOV = 9.4375f;
    private const float _zoomedFOV = 2.8125f;
    private Coroutine fovCoroutine;

    private const float defaultY = 0f; // Set to the default Y offset
    private const float upY = 3f; // Set to the Y offset when looking up
    private const float downY = -3f; // Set to the Y offset when looking down
    private const float downYFalling = -5f;
    private Coroutine positionCoroutine;
    private CinemachineCameraOffset cameraOffset;

    private Transform temporaryTarget;

    private float _originalDeadZoneWidth;
    private float _originalDeadZoneHeight;

    private CinemachineFramingTransposer framingTransposer;
    private float originalLookaheadTime;
    private float originalLookaheadSmoothing;
    private float originalXDamping;
    private float originalYDamping;
    private Coroutine moveCoroutine;

    private float pixelsPerUnit = 16f;
    private float unitsPerPixel;

    void Awake() {
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        }       
        cam = GetComponent<CinemachineVirtualCamera>();
        _brain = FindObjectOfType<CinemachineBrain>();

        // Store original lookahead values
        framingTransposer = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        if (framingTransposer != null)
        {
            originalLookaheadTime = framingTransposer.m_LookaheadTime;
            originalLookaheadSmoothing = framingTransposer.m_LookaheadSmoothing;
            originalXDamping = framingTransposer.m_XDamping;
            originalYDamping = framingTransposer.m_YDamping;
            _originalDeadZoneWidth = framingTransposer.m_DeadZoneWidth;
            _originalDeadZoneHeight = framingTransposer.m_DeadZoneHeight;
        }
        cameraOffset = cam.GetComponent<CinemachineCameraOffset>();
        _handheldProfile = Resources.Load<NoiseSettings>("Cinemachine Presets/Handheld");
        _fireballRightChild = this.transform.Find("FireballRight").gameObject;
        _fireballLeftChild = this.transform.Find("FireballLeft").gameObject;
        _shakeSoft = this.transform.Find("ShakeSoft").gameObject;
        _shakeMedium = this.transform.Find("ShakeMedium").gameObject;
        _shakeHard = this.transform.Find("ShakeHard").gameObject;
        _earthquake = this.transform.Find("Earthquake").gameObject;

        unitsPerPixel = 1f / pixelsPerUnit;
    }

    void Start() {
        if (SceneManager.GetActiveScene().name != "intro"){
            cam.m_Follow = Hero;
            cam.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = GenericCamBoundariesForAwake;
            cam.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = FindObjectsOfType<RoomConfigurations>().FirstOrDefault(room => room.isCurrentRoom).GetComponent<PolygonCollider2D>();
            _originalDeadZoneWidth = framingTransposer.m_DeadZoneWidth;
            _originalDeadZoneHeight = framingTransposer.m_DeadZoneHeight;
        }
    }

    void LateUpdate() {
        EnforcePixelPerfectMovement();
    }

    private void EnforcePixelPerfectMovement() {
        Vector3 position = transform.position;
        position.x = Mathf.Round(position.x / unitsPerPixel) * unitsPerPixel;
        position.y = Mathf.Round(position.y / unitsPerPixel) * unitsPerPixel;
        transform.position = position;
    }

    void FixedUpdate() {
        if (SceneManager.GetActiveScene().name != "intro"){
            if (GameState.Instance.Overworld)
            {
                if (!PlayerController.Instance.AnimatorIsPlaying("fall") && PlayerController.Instance.canLook && Inputs.Instance.HoldingDownArrow && !PlayerState.Instance.OnSaveTrigger && !PlayerState.Instance.OnElevator && PlayerState.Instance.Grounded && !PlayerController.Instance.AnimatorIsPlaying("run") && !PlayerController.Instance.AnimatorIsPlaying("run_landing") && (PlayerController.Instance.AnimatorIsPlaying("idle") || PlayerController.Instance.AnimatorIsPlaying("look_up") || PlayerController.Instance.AnimatorIsPlaying("look_up_2") || PlayerController.Instance.AnimatorIsPlaying("look_down") || PlayerController.Instance.AnimatorIsPlaying("look_down_2") || PlayerController.Instance.AnimatorIsPlaying("attack_up") || PlayerController.Instance.AnimatorIsPlaying("attack_down"))){
                    LookDown();
                } else if (!PlayerController.Instance.AnimatorIsPlaying("fall") && PlayerController.Instance.canLook && Inputs.Instance.HoldingUpArrow && !PlayerState.Instance.OnSaveTrigger && !PlayerState.Instance.OnElevator && PlayerState.Instance.Grounded && !PlayerController.Instance.AnimatorIsPlaying("run") && !PlayerController.Instance.AnimatorIsPlaying("run_landing") && (PlayerController.Instance.AnimatorIsPlaying("idle") || PlayerController.Instance.AnimatorIsPlaying("look_up") || PlayerController.Instance.AnimatorIsPlaying("look_up_2") || PlayerController.Instance.AnimatorIsPlaying("look_down") || PlayerController.Instance.AnimatorIsPlaying("look_down_2") || PlayerController.Instance.AnimatorIsPlaying("attack_up") || PlayerController.Instance.AnimatorIsPlaying("attack_down"))){
                    LookUp();
                } else {
                    LookStraight();
                }
            }
        }
    }

    #endregion

    // Method to disable lookahead
    private void DisableCameraSmoothingAndDamping() {
        if (framingTransposer != null)
        {
            framingTransposer.m_LookaheadTime = 0;
            framingTransposer.m_LookaheadSmoothing = 0;
            framingTransposer.m_XDamping = 0;
            framingTransposer.m_YDamping = 0;
            framingTransposer.m_DeadZoneWidth = 0;
            framingTransposer.m_DeadZoneHeight = 0;
        }
    }

    // Method to restore camera smoothing, damping, and deadzone
    private void RestoreCameraSmoothingAndDamping() {
        if (framingTransposer != null)
        {
            framingTransposer.m_LookaheadTime = originalLookaheadTime;
            framingTransposer.m_LookaheadSmoothing = originalLookaheadSmoothing;
            framingTransposer.m_XDamping = originalXDamping;
            framingTransposer.m_YDamping = originalYDamping;
            framingTransposer.m_DeadZoneWidth = _originalDeadZoneWidth;
            framingTransposer.m_DeadZoneHeight = _originalDeadZoneHeight;
        }
    }

    public IEnumerator SmoothTransitionToNewConfiner(Collider2D newRoomCollider, CinemachineConfiner2D confiner, float duration = 0.1f) // Super fast transition
    {
        // DisableCameraSmoothingAndDamping();

        float timeElapsed = 0f;
        Vector3 startPosition = cam.transform.position;
        Vector3 targetPosition = Hero.position;
        targetPosition.z = startPosition.z; // Keep the same Z value

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;
            cam.transform.position = Vector3.Lerp(startPosition, targetPosition, EaseInOutQuad(t));
            yield return null;
        }

        confiner.m_BoundingShape2D = newRoomCollider;
        cam.transform.position = targetPosition; // Ensure final position is set

        // RestoreCameraSmoothingAndDamping();
    }

    private float EaseInOutQuad(float t) {
        return t < 0.5 ? 2 * t * t : -1 + (4 - 2 * t) * t;
    }

    private IEnumerator MoveToWiz() {
        float timeElapsed = 0f;
        Vector3 startPosition = cam.transform.position;
        float fixedZ = cam.transform.position.z; // Store the fixed Z value

        CinemachineFramingTransposer framingTransposer = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        float deadZoneWidth = framingTransposer.m_DeadZoneWidth;
        float deadZoneHeight = framingTransposer.m_DeadZoneHeight;

        while (true) {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / 2;
            Vector3 targetPosition = new Vector3(Hero.position.x, Hero.position.y, fixedZ);
            cam.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            startPosition = cam.transform.position; // Update the start position to the current position
            
            Camera actualCamera = CinemachineCore.Instance.GetActiveBrain(0).OutputCamera;
            Vector3 wizScreenPos = actualCamera.WorldToScreenPoint(Hero.position);

            Vector3 camCenterScreenPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Vector3 delta = wizScreenPos - camCenterScreenPos;

            yield return null;
        }
    }

    public void SwitchToWizCam() {
        StartCoroutine(MoveToWiz());
    }

    public void SwitchToFixedCam(bool fast = false) {
        Debug.Log("FIXING CAM AT: " + FixedXPosition + " | " + FixedYPosition);
        
        if (moveCoroutine != null) {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
        
        if (fast) {
            moveCoroutine = StartCoroutine(MoveToPosition(new Vector3(FixedXPosition, FixedYPosition, cam.transform.position.z), 0.3f));
        } else {
            moveCoroutine = StartCoroutine(MoveToPosition(new Vector3(FixedXPosition, FixedYPosition, cam.transform.position.z), 2f));
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition, float duration) {
        Debug.Log("Starting MoveToPosition Coroutine");

        float timeElapsed = 0f;
        Vector3 startPosition = cam.transform.position;
        _brain.enabled = false;
        cam.m_Follow = null;

        while (timeElapsed < duration) {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration; // Normalized time (0 to 1)
            // Lerp and apply the position immediately
            cam.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            Debug.Log($"Moving Camera: {cam.transform.position}");
            yield return null; // Wait for the next frame 
        }

        // Ensure the final position is set (optional, but good practice)
        cam.transform.position = targetPosition;
        Debug.Log($"Final Camera Position: {cam.transform.position}");

        _brain.enabled = true;
        moveCoroutine = null;
    }

    [Button]
    public void ShakeCamera(int _intensity = 0) {
        switch (_intensity) {
            case 0:
                _shakeSoft.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
                break;
            case 1:
                _shakeMedium.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
                break;
            case 2:
                _shakeHard.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
                break;
            default:
                _shakeSoft.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
                break;
        }
    }

    [Button]
    public void ToggleHandheld() {
        if (Handheld) {
            Handheld = false;
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        } else {
            Handheld = true;
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1;
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        }
    }

    [Button]
    public void ToggleEarthquake() {
        if (Earthquake) {
            Earthquake = false;
        } else {
            Earthquake = true;
            StartCoroutine(StartEarthquake());
        }
    }

    IEnumerator StartEarthquake() {
        while (Earthquake) {
            _earthquake.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
            yield return new WaitForSeconds(0.2f);
        }
    }

    [Button]
    public void FireballRight() {
        _fireballRightChild.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
    }

    [Button]
    public void FireballLeft() {
        _fireballLeftChild.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
    }

    public void FarZoom() {
        if (fovCoroutine != null) StopCoroutine(fovCoroutine);
        fovCoroutine = StartCoroutine(ChangeFOV(_farFOV));
    }

    public void StrongZoom(bool fast = false) {
        if (fast) {
            // print("zoom fast!");
            // Stop any ongoing dead zone coroutine and FOV coroutine
            if (fovCoroutine != null) StopCoroutine(fovCoroutine);
            if (positionCoroutine != null) StopCoroutine(positionCoroutine);

            // Start new coroutines for fast zoom and dead zone adjustment
            positionCoroutine = StartCoroutine(LerpDeadZonesToZero(0.1f));
            fovCoroutine = StartCoroutine(ChangeFOVFast(_zoomedFOV));

            // Instantly move the camera to the player's position
            cam.transform.position = new Vector3(PlayerController.Instance.transform.position.x, PlayerController.Instance.transform.position.y, cam.transform.position.z);
        } else {
            // Stop any ongoing dead zone coroutine and FOV coroutine
            if (fovCoroutine != null) StopCoroutine(fovCoroutine);
            if (positionCoroutine != null) StopCoroutine(positionCoroutine);

            // Start new coroutines for smooth zoom and dead zone adjustment
            positionCoroutine = StartCoroutine(LerpDeadZonesToZero(0.5f));
            fovCoroutine = StartCoroutine(ChangeFOV(_zoomedFOV));

            // Smoothly move the camera to the player's position
            cam.transform.position = new Vector3(PlayerController.Instance.transform.position.x, PlayerController.Instance.transform.position.y, cam.transform.position.z);
        }
    }

    public void DefaultZoom() {
        StartCoroutine(LerpDeadZonesToOriginal(1f));
        if (fovCoroutine != null) StopCoroutine(fovCoroutine);
        fovCoroutine = StartCoroutine(ChangeFOV(_nativeFOV));
    }

    private void SetDeadZoneValues(float width, float height) {
        if (cam != null)
        {
            cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = width;
            cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = height;
        }
    }

    private IEnumerator LerpDeadZonesToZero(float duration) {
        float elapsedTime = 0f;
        while (elapsedTime < duration) {
            float t = elapsedTime / duration;
            float lerpedWidth = Mathf.Lerp(_originalDeadZoneWidth, 0f, t);
            float lerpedHeight = Mathf.Lerp(_originalDeadZoneHeight, 0f, t);

            // Set the lerped dead zone values
            SetDeadZoneValues(lerpedWidth, lerpedHeight);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure dead zone values are set to zero at the end
        SetDeadZoneValues(0f, 0f);
    }

    private IEnumerator LerpDeadZonesToOriginal(float duration) {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float lerpedWidth = Mathf.Lerp(0f, _originalDeadZoneWidth, t);
            float lerpedHeight = Mathf.Lerp(0f, _originalDeadZoneHeight, t);
            
            // Set the lerped dead zone values
            SetDeadZoneValues(lerpedWidth, lerpedHeight);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        // Ensure dead zone values are set to zero at the end
        SetDeadZoneValues(_originalDeadZoneWidth, _originalDeadZoneHeight);
    }

    private IEnumerator ChangeFOV(float targetFOV) {
        float elapsedTime = 0f;
        float currentFOV = cam.m_Lens.OrthographicSize;

        float duration = 0.5f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            cam.m_Lens.OrthographicSize = Mathf.Lerp(currentFOV, targetFOV, elapsedTime / duration);
            yield return null;
        }
    }

    private IEnumerator ChangeFOVFast(float targetFOV) {
        float elapsedTime = 0f;
        float currentFOV = cam.m_Lens.OrthographicSize;

        // Reduce the duration to make the zoom faster
        float duration = 0.2f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            cam.m_Lens.OrthographicSize = Mathf.Lerp(currentFOV, targetFOV, elapsedTime / duration);
            yield return null;
        }
        // Ensure the final FOV is set
        cam.m_Lens.OrthographicSize = targetFOV;
    }

    public void LookUp() {
        StartCoroutine(StartLookCoroutine(true));
    }

    public void LookDown() {
        StartCoroutine(StartLookCoroutine(false));
    }

    private IEnumerator StartLookCoroutine(bool isLookingUp) {
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds

        if (isLookingUp) {
            // Existing logic for looking up
            if (positionCoroutine != null) StopCoroutine(positionCoroutine);
            positionCoroutine = StartCoroutine(ChangeY(upY));
            if (!PlayerController.Instance.AnimatorIsPlaying("look_up") && !PlayerController.Instance.AnimatorIsPlaying("look_up_2")){
                PlayerController.Instance.Animator.Play("look_up");
            }
        } else {
            // Existing logic for looking down
            if (positionCoroutine != null) StopCoroutine(positionCoroutine);
            positionCoroutine = StartCoroutine(ChangeY(downY));
            if (!PlayerController.Instance.AnimatorIsPlaying("look_down") && !PlayerController.Instance.AnimatorIsPlaying("look_down_2")){
                PlayerController.Instance.Animator.Play("look_down");
            }
        }
    }

    public void LookDownFalling() {
        if (positionCoroutine != null) StopCoroutine(positionCoroutine);
        positionCoroutine = StartCoroutine(ChangeYFaster(downYFalling));
    }

    public void LookStraight() {
        if (positionCoroutine != null) StopCoroutine(positionCoroutine);
        positionCoroutine = StartCoroutine(ChangeY(defaultY));
        if (PlayerController.Instance.AnimatorIsPlaying("look_up") || PlayerController.Instance.AnimatorIsPlaying("look_up_2") || PlayerController.Instance.AnimatorIsPlaying("look_down") || PlayerController.Instance.AnimatorIsPlaying("look_down_2")){
            PlayerController.Instance.Animator.Play("idle");
        }
    }

    private IEnumerator ChangeY(float targetY) {
        float elapsedTime = 0f;
        float currentY = cameraOffset.m_Offset.y;
        Vector3 currentOffset = cameraOffset.m_Offset;

        while (elapsedTime < 1f) {
            elapsedTime += Time.deltaTime * 2;
            currentOffset.y = Mathf.Lerp(currentY, targetY, elapsedTime);
            cameraOffset.m_Offset = currentOffset;
            yield return null;
        }
    }

    private IEnumerator ChangeYFaster(float targetY) {
        float elapsedTime = 0f;
        float currentY = cameraOffset.m_Offset.y;
        Vector3 currentOffset = cameraOffset.m_Offset;

        while (elapsedTime < 1f) {
            elapsedTime += Time.deltaTime * 5;
            currentOffset.y = Mathf.Lerp(currentY, targetY, elapsedTime);
            cameraOffset.m_Offset = currentOffset;
            yield return null;
        }
    }

    public void SetLookAt(Transform target) {
        DisableCameraSmoothingAndDamping();
        StartCoroutine(LerpDeadZonesToZero(0.1f));
        cam.m_Follow = target;
    }

    public void SetLookAtHero() {
        RestoreCameraSmoothingAndDamping();
        StartCoroutine(LerpDeadZonesToOriginal(0.1f));
        cam.m_Follow = Hero;
        RestoreCameraSmoothingAndDamping();
        StartCoroutine(LerpDeadZonesToOriginal(0.1f));
    }

    public void SetFollow(Transform target) {
        cam.m_LookAt = target;
    }

    public void RemoveLookAt() {
        StartCoroutine(SmoothTransition(FindObjectOfType<PlayerController>().transform));
    }

    private IEnumerator SmoothTransition(Transform target) {
        if (temporaryTarget == null) {
            GameObject tempObj = new GameObject("Temporary Target");
            temporaryTarget = tempObj.transform;
        }

        Vector3 startPosition = temporaryTarget.position;
        Vector3 targetPosition = target.position;
        float transitionTime = 3f; // You can adjust this value
        float elapsedTime = 0f;

        while (elapsedTime < transitionTime) {
            elapsedTime += Time.deltaTime;
            temporaryTarget.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / transitionTime);
            yield return null;
        }

        temporaryTarget.position = targetPosition;
        cam.m_Follow = target;
    }

    public void StopAllShakes() {
        StopAllCoroutines();
        CancelInvoke();
    }

    [Button]
    public void DarkworldTransition() {
        StartCoroutine(DarkworldCameraTransition());
    }

    private IEnumerator DarkworldCameraTransition() {
        float totalDuration = 4.3f;
        float anticipationTime = 0.3f;
        float mainRotationTime = 3f;
        float stabilizationTime = 1f;

        float elapsedTime = 0f;

        // Initial anticipation phase
        while (elapsedTime < anticipationTime) {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / anticipationTime;
            float angle = Mathf.Lerp(0, 10, t); // Anticipation angle (opposite direction)
            transform.rotation = Quaternion.Euler(0, 0, angle);
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);

        // Main rotation phase with overshooting
        elapsedTime = 0f;
        while (elapsedTime < mainRotationTime) {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / mainRotationTime;
            float angle = Mathf.Lerp(10, -390, t); // -360 with overshooting
            transform.rotation = Quaternion.Euler(0, 0, angle);
            yield return null;
        }

        // Handshake stabilization phase with progressively faster segments
        elapsedTime = 0f;
        float[] overshootAngles = { -390, -350, -370, -360.5f };
        int numSegments = overshootAngles.Length - 1;
        float baseHandshakeTime = stabilizationTime / (numSegments * (numSegments + 1) / 4); // Adjusted to make each segment faster

        for (int i = 0; i < numSegments; i++) {
            float segmentTime = baseHandshakeTime * (numSegments - i); // Decreasing time for each segment
            while (elapsedTime < segmentTime) {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / segmentTime;
                float angle = Mathf.Lerp(overshootAngles[i], overshootAngles[i + 1], t);
                transform.rotation = Quaternion.Euler(0, 0, angle);
                yield return null;
            }
            elapsedTime = 0f; // Reset for the next segment
        }

        // Return to 0 rotation for gameplay
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    [Button]
    public void MoveCameraToPlayer() {
        Debug.Log("Moved camera to HERO");
        this.transform.position = new Vector3(FindObjectOfType<PlayerController>().transform.position.x, FindObjectOfType<PlayerController>().transform.position.y, this.transform.position.z);
        EditorUtility.SetDirty(this.transform);
        SceneView.RepaintAll();
    }

    public void ChangeCamBoundaries(PolygonCollider2D collider)
    {
        cam.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = collider;
    }

}
