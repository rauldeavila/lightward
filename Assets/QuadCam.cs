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
    public bool NewGame = false;

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

    public void StartCinematicTransition()
    {
        if (NewGame)
        {
            Vector3 startPosition = new Vector3(-1278f, -182.8f, 0.3f);
            Vector3 targetPosition = new Vector3(-1244.5f, -185f, 70f);
            StartCoroutine(CinematicTransition(startPosition, targetPosition));
        }
    }

    private IEnumerator CinematicTransition(Vector3 startPosition, Vector3 targetPosition)
    {
        float zOnlyDuration = 2f;  // Duration for moving only the Z-axis
        float fullMovementDuration = 3f; // Duration for moving X, Y, and the remaining Z movement
        float elapsedTime = 0f;

        // Initial Z movement phase
        while (elapsedTime < zOnlyDuration)
        {
            float t = elapsedTime / zOnlyDuration;
            float partialZ = Mathf.Lerp(startPosition.z, targetPosition.z, t * (zOnlyDuration / (zOnlyDuration + fullMovementDuration)));
            transform.position = new Vector3(startPosition.x, startPosition.y, partialZ);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;  // Reset elapsed time for the next phase

        // Full movement phase: X, Y, and remaining Z movement
        while (elapsedTime < fullMovementDuration)
        {
            float t = elapsedTime / fullMovementDuration;

            // Calculate the remaining Z movement
            float remainingZ = Mathf.Lerp(
                Mathf.Lerp(startPosition.z, targetPosition.z, zOnlyDuration / (zOnlyDuration + fullMovementDuration)),
                targetPosition.z, 
                t
            );

            transform.position = new Vector3(
                Mathf.Lerp(startPosition.x, targetPosition.x, t), 
                Mathf.Lerp(startPosition.y, targetPosition.y, t), 
                remainingZ
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is exactly the target position
        transform.position = targetPosition;
        SetNewGameToFalse();  // Ensure NewGame is set to false after the transition
    }


    public void SetNewGameToFalse()
    {
        NewGame = false;
    }

    [Button("Zoom In")]
    public void ZoomIn(bool strongZoom = false)
    {
        if (NewGame) { return; }
        float zoomZ = strongZoom ? strongZoomZ : mediumZoomZ;

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        Zoomed = true;

        Vector2 clampedNormalizedPosition = ZoomPositionBasedOnPlayer.Instance.transform.localPosition;
        Vector2 convertedVector = ConvertNormalizedToGameUnits(clampedNormalizedPosition);
        Debug.Log("Normalized: " + clampedNormalizedPosition.x + ", " + clampedNormalizedPosition.y + " - Converted: " + convertedVector.x + ", " + convertedVector.y);

        currentCoroutine = StartCoroutine(SmoothZoom(new Vector3(defaultX - convertedVector.x, defaultY - convertedVector.y, zoomZ)));
    }

    Vector2 ConvertNormalizedToGameUnits(Vector2 normalizedPosition)
    {
        float gameX = normalizedPosition.x * 320f;
        float gameY = normalizedPosition.y * 180f;

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
