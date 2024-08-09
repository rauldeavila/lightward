using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

public class QuadCam : MonoBehaviour
{
    public static QuadCam Instance;

    private float defaultX = -1249f;
    private float defaultY = -201f;
    private float defaultZ = 155.94f;
    private float zoomedZ = 50f;
    public float zoomSpeed = 5f;

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
    public void ZoomIn(float targetX, float targetY)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(SmoothZoom(new Vector3(-1249f, -140f, zoomedZ)));
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

    private IEnumerator SmoothZoom(Vector3 targetPosition)
    {
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
