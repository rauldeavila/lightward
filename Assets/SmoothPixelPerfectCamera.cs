using UnityEngine;

public class SmoothPixelPerfectCamera : MonoBehaviour
{
    public Transform target; // The target the camera will follow
    public float smoothSpeed = 0.125f; // How smoothly the camera follows the target
    public int pixelsPerUnit = 16; // Pixels per unit (PPU) for your assets

    private Vector3 _velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null)
            return;

        // Smoothly follow the target
        Vector3 desiredPosition = target.position;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, smoothSpeed);

        // Apply pixel-perfect adjustment
        smoothedPosition.x = Mathf.Round(smoothedPosition.x * pixelsPerUnit) / pixelsPerUnit;
        smoothedPosition.y = Mathf.Round(smoothedPosition.y * pixelsPerUnit) / pixelsPerUnit;
        smoothedPosition.z = transform.position.z; // Keep the original z position

        transform.position = smoothedPosition;
    }
}
