using UnityEngine;

public class SquishAndStretch : MonoBehaviour
{
    [SerializeField] private float maxScale = 1.5f;
    [SerializeField] private float minScale = 0.5f;

    private Vector3 defaultScale;
    private Vector3 previousPosition;
    private Vector3 currentPosition;

    private void Awake()
    {
        defaultScale = transform.localScale;
        previousPosition = transform.position;
        currentPosition = transform.position;
    }

private void Update()
{
    currentPosition = transform.position;

    // Calculate velocity based on the change in position since the last frame
    Vector3 velocity = (currentPosition - previousPosition) / Time.deltaTime;

    // Calculate the scale factor based on the magnitude of the velocity
    float scaleFactor = Mathf.Clamp(velocity.magnitude / 10f, 0f, maxScale);

    // Calculate the scale for each axis based on the scale factor
    float xScale = Mathf.Lerp(defaultScale.x, defaultScale.x * scaleFactor, scaleFactor);
    float yScale = defaultScale.y;

    if (Mathf.Approximately(velocity.magnitude, 0f))
    {
        yScale = defaultScale.y;
    }
    else
    {
        yScale = Mathf.Lerp(defaultScale.y, defaultScale.y / scaleFactor, scaleFactor);
    }

    // Apply the scale to the transform
    transform.localScale = new Vector3(xScale, yScale, defaultScale.z);

    // Update the previous position for the next frame
    previousPosition = currentPosition;
}
}
