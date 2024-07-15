using UnityEngine;

public class LightCollisionDetection : MonoBehaviour
{
    public GameObject explosionPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // Get the point of contact
            Vector2 contactPoint = other.ClosestPoint(transform.position);

            // Instantiate explosion
            Instantiate(explosionPrefab, contactPoint, Quaternion.identity);
        }
    }
}
