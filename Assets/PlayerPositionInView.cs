using UnityEngine;

public class PlayerPositionInView : MonoBehaviour
{
    private Transform playerTransform;
    private Camera mainCamera;

    public static PlayerPositionInView Instance;

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

    private void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = PlayerController.Instance.transform;
        }

        if (mainCamera == null)
        {
            mainCamera = MainCamera.Instance.GetComponent<Camera>();
        }
    }

    public Vector2 GetPlayerPositionInRenderView()
    {
        // Get the player's position in world space
        Vector3 playerPosition = playerTransform.position;

        // Convert player's position to viewport space (0,0 is bottom-left, 1,1 is top-right)
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(playerPosition);

        // Convert viewport position to a pixel position within the 320x180 render view
        float xInRenderView = viewportPosition.x * 320f;
        float yInRenderView = viewportPosition.y * 180f;

        // Return the position as a Vector2
        return new Vector2(xInRenderView, yInRenderView);
    }

}
