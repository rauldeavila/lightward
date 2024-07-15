using UnityEngine;

public class GhostExplosion : MonoBehaviour
{
    private GameObject explosionPrefab;

    private void Start()
    {
        // Load the explosion prefab from Resources/Particles folder
        explosionPrefab = Resources.Load("Particles/GhostExplosion") as GameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "WizHitBox")
        {
            if(explosionPrefab){
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }
            Destroy(transform.parent.gameObject);
        }
    }
}
