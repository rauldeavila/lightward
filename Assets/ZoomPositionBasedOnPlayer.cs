using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomPositionBasedOnPlayer : MonoBehaviour
{
    // Define boundaries in normalized coordinates (-0.5 to 0.5)
    private const float xMin = -0.4f;  // -80px / 320px
    private const float xMax = 0.4f;   // 80px / 320px
    private const float yMin = -0.4f;  // -45px / 180px
    private const float yMax = 0.4f;   // 45px / 180px

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
        if (QuadCam.Instance.Zoomed == false)
        {
            // Get the player's position in the render view (0 to 320 for x, 0 to 180 for y)
            Vector2 playerRenderPosition = PlayerPositionInView.Instance.GetPlayerPositionInRenderView();

            // Normalize the player's render position (-0.5 to 0.5 scale)
            Vector2 normalizedPosition = new Vector2(
                (playerRenderPosition.x / 320f) - 0.5f,
                (playerRenderPosition.y / 180f) - 0.5f
            );

            // Clamp the normalized position within the defined boundaries
            float clampedX = Mathf.Clamp(normalizedPosition.x, xMin, xMax);
            float clampedY = Mathf.Clamp(normalizedPosition.y, yMin, yMax);

            // Set the local position of the transform to the clamped normalized values
            this.transform.localPosition = new Vector2(clampedX, clampedY);
        }
    }
}
