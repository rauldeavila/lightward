using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

public class QuadCam : MonoBehaviour
{
    public static QuadCam Instance;

    private float defaultX = -1249f;
    private float defaultY = -201f;
    private float defaultZ = 155.94f;
    private float mediumZoomZ = 75f;
    private float strongZoomZ = 50f;
    public float zoomSpeed = 5f;

    public Transform A;
    public Transform B;
    public Transform C;
    public Transform D;
    public Transform E;
    public Transform F;
    public Transform G;
    public Transform H;
    public Transform I;

    private Coroutine currentCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    [Button("Zoom In")]
    public void ZoomIn(bool strongZoom = false)
    {
        Transform targetTransform = GetTargetTransformFromQuadrant();
        if (targetTransform == null)
        {
            Debug.LogWarning("No quadrant detected or invalid quadrant.");
            return;
        }

        float zoomZ = strongZoom ? strongZoomZ : mediumZoomZ;

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(SmoothZoom(new Vector3(targetTransform.position.x, targetTransform.position.y, zoomZ)));
    }

    [Button("Zoom Out")]
    public void ZoomOut()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(SmoothZoom(new Vector3(defaultX, defaultY, defaultZ)));
    }

    private Transform GetTargetTransformFromQuadrant()
    {
        string currentQuadrant = QuadrantController.Instance.GetCurrentQuadrant();

        switch (currentQuadrant)
        {
            case "A": return A;
            case "B": return B;
            case "C": return C;
            case "D": return D;
            case "E": return E;
            case "F": return F;
            case "G": return G;
            case "H": return H;
            case "I": return I;
            default: return null;
        }
    }

    private IEnumerator SmoothZoom(Vector3 targetPosition)
    {
        Debug.Log("Target position = " + GetTargetTransformFromQuadrant().ToString());
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * zoomSpeed);
            yield return null;
        }

        // Ensure the final position is set to avoid overshoot
        transform.position = targetPosition;
        currentCoroutine = null; // Reset the coroutine reference when done
    }
}
