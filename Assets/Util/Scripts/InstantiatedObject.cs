using UnityEngine;

public class InstantiatedObject : MonoBehaviour
{
    public float MinRotationSpeed = 0f;
    public float MaxRotationSpeed = 360f;
    public float InitialForceYMin = 0f;
    public float InitialForceYMax = 0f;
    public float MaxRandomForceX = 10f;

    private float _rotationSpeed;

    void Start()
    {
        // Calculate the initial rotation speed based on the min and max rotation speeds
        _rotationSpeed = Random.Range(MinRotationSpeed, MaxRotationSpeed);

        // Calculate the initial force
        Vector2 force = new Vector2(0f, Random.Range(InitialForceYMin, InitialForceYMax));
        force += new Vector2(Random.Range(-MaxRandomForceX, MaxRandomForceX), 0f);

        // Apply the force to the object
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody != null)
        {
            rigidbody.AddForce(force, ForceMode2D.Impulse);
        }
    }

    void Update()
    {
        // Rotate the object around the z axis
        transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime);
    }
}
