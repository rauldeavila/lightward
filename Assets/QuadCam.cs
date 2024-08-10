using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class QuadCam : MonoBehaviour
{
    public static QuadCam Instance;

    private float defaultX = -1249f;
    private float defaultY = -201f;
    private float defaultZ = 155.94f;
    private float mediumZoomZ = 75f;
    private float strongZoomZ = 50f;
    public float zoomSpeed = 5f;

    public bool Zoomed = false;


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
    float zoomZ = strongZoom ? strongZoomZ : mediumZoomZ;

    if (currentCoroutine != null)
    {
        StopCoroutine(currentCoroutine);
    }
    Zoomed = true;

    // Directly use the clamped normalized position
    Vector2 clampedNormalizedPosition = ZoomPositionBasedOnPlayer.Instance.transform.localPosition;

    // Convert to game units based on clamped normalized position
    Vector2 convertedVector = ConvertNormalizedToGameUnits(clampedNormalizedPosition);

    // Debug log with proper formatting
    Debug.Log("Normalized: " + clampedNormalizedPosition.x + ", " + clampedNormalizedPosition.y + " - Converted: " + convertedVector.x + ", " + convertedVector.y);

    // Start zoom coroutine
    currentCoroutine = StartCoroutine(SmoothZoom(new Vector3(defaultX - convertedVector.x, defaultY - convertedVector.y, zoomZ)));
}

Vector2 ConvertNormalizedToGameUnits(Vector2 normalizedPosition)
{
    float gameX = normalizedPosition.x * 320f;  // Correct scaling
    float gameY = normalizedPosition.y * 180f;  // Correct scaling
    
    return new Vector2(gameX, gameY);
}


    [Button("Zoom Out")]
    public void ZoomOut()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(SmoothZoom(new Vector3(defaultX, defaultY, defaultZ)));
        Zoomed = false;
    }


    private IEnumerator SmoothZoom(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * zoomSpeed);
            yield return null;
        }

        transform.position = targetPosition;
        currentCoroutine = null; 
    }
}
