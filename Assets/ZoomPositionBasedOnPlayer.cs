using UnityEngine;

public class ZoomPositionBasedOnPlayer : MonoBehaviour
{
    private const float xMin = -0.4f;
    private const float xMax = 0.4f;
    private const float yMin = -0.4f;
    private const float yMax = 0.4f;

    public bool Looping = true;
    public Transform CustomTransform;

    public static ZoomPositionBasedOnPlayer Instance;

    void Awake()
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

    void Update()
    {
        if (Looping)
        {
            if (QuadCam.Instance.Zoomed == false)
            {
                Vector2 playerRenderPosition = PlayerPositionInView.Instance.GetPlayerPositionInRenderView();
                UpdateCameraPosition(playerRenderPosition);
            }
        }
    }

    void UpdateCameraPosition(Vector2 renderPosition)
    {
        Vector2 normalizedPosition = new Vector2(
            (renderPosition.x / 320f) - 0.5f,
            (renderPosition.y / 180f) - 0.5f
        );

        float clampedX = Mathf.Clamp(normalizedPosition.x, xMin, xMax);
        float clampedY = Mathf.Clamp(normalizedPosition.y, yMin, yMax);

        this.transform.localPosition = new Vector2(clampedX, clampedY);
    }

    public void ZoomThis(Transform transform)
    {
        Debug.Log("Zooming to Custom Transform: " + transform.name);
        Looping = false;  // Stop the loop to prevent updates from the player position
        CustomTransform = transform;

        UpdateCameraToCustomTransform();
    }

    void UpdateCameraToCustomTransform()
    {
        if (CustomTransform != null)
        {
            Vector2 transformRenderPosition = GetTransformPositionInRenderView(CustomTransform);
            UpdateCameraPosition(transformRenderPosition);
        }
        else
        {
            Debug.LogError("CustomTransform is null in UpdateCameraToCustomTransform");
        }
    }

    Vector2 GetTransformPositionInRenderView(Transform targetTransform)
    {
        Vector3 transformPosition = targetTransform.position;
        Vector3 viewportPosition = MainCamera.Instance.GetComponent<Camera>().WorldToViewportPoint(transformPosition);

        float xInRenderView = viewportPosition.x * 320f;
        float yInRenderView = viewportPosition.y * 180f;

        return new Vector2(xInRenderView, yInRenderView);
    }

    public void ResetTransformToZoom()
    {
        Debug.Log("Resetting to Player Tracking");
        CustomTransform = null;
        Looping = true;  // Resume the loop to follow the player again
    }
}
