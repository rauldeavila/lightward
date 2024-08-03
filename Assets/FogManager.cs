using System.Collections.Generic;
using UnityEngine;

public class FogManager : MonoBehaviour
{
    public GameObject fogPrefab;
    public GameObject fogPrefab2;
    public GameObject fogPrefab3;
    public int poolSize = 20;
    public float spawnInterval = 1.0f;
    public bool SpawnAtLeft = false;
    public bool SpawnCloserToCam = false;
    public bool endAtFarPosition = true;

    private List<GameObject> fogPool;
    private List<float> initialZPositions; // Store initial Z positions
    private float timer;
    private int previousPositionIndex = -1;
    private int[] positionUsage = {0, 0, 0}; // Keeps track of the usage count for top, mid, bottom positions

    void Start()
    {
        InitializePool();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnFog();
            timer = 0;
        }

        CheckAndDeactivateFog();
    }

    void InitializePool()
    {
        fogPool = new List<GameObject>();
        initialZPositions = new List<float>(); // Initialize the list
        for (int i = 0; i < poolSize; i++)
        {
            GameObject fog = Instantiate(GetRandomFogPrefab());
            fog.SetActive(false);
            fogPool.Add(fog);
            initialZPositions.Add(fog.transform.position.z); // Store initial Z position
        }
    }

    GameObject GetRandomFogPrefab()
    {
        if (fogPrefab2 != null && fogPrefab3 != null)
        {
            int rand = Random.Range(0, 3);
            return rand == 0 ? fogPrefab : rand == 1 ? fogPrefab2 : fogPrefab3;
        }
        else if (fogPrefab2 != null)
        {
            return Random.Range(0, 2) == 0 ? fogPrefab : fogPrefab2;
        }
        else
        {
            return fogPrefab;
        }
    }

    void SpawnFog()
    {
        GameObject fog = GetPooledFog();
        if (fog != null)
        {
            Transform spawnPosition = GetSpawnPosition();
            Vector3 spawnPos = new Vector3(spawnPosition.position.x, spawnPosition.position.y, initialZPositions[fogPool.IndexOf(fog)]); // Use stored Z position

            fog.transform.position = spawnPos;

            // Reset the fog object before activating it
            ResetFog(fog, spawnPos);

            fog.SetActive(true);

            // Debug log to check the position of spawning
            Debug.Log($"Spawned fog at position: {spawnPos}");
        }
    }

    Transform GetSpawnPosition()
    {
        if (SpawnAtLeft)
        {
            if (SpawnCloserToCam)
            {
                return GetNextPosition(
                    ParticlePosition.Instance.left_top_00,
                    ParticlePosition.Instance.left_mid_00,
                    ParticlePosition.Instance.left_bottom_00
                );
            }
            else
            {
                return GetNextPosition(
                    ParticlePosition.Instance.left_top_01,
                    ParticlePosition.Instance.left_mid_01,
                    ParticlePosition.Instance.left_bottom_01
                );
            }
        }
        else
        {
            if (SpawnCloserToCam)
            {
                return GetNextPosition(
                    ParticlePosition.Instance.right_top_00,
                    ParticlePosition.Instance.right_mid_00,
                    ParticlePosition.Instance.right_bottom_00
                );
            }
            else
            {
                return GetNextPosition(
                    ParticlePosition.Instance.right_top_01,
                    ParticlePosition.Instance.right_mid_01,
                    ParticlePosition.Instance.right_bottom_01
                );
            }
        }
    }

    Transform GetNextPosition(Transform top, Transform mid, Transform bottom)
    {
        int nextPositionIndex = GetNextPositionIndex();
        positionUsage[nextPositionIndex]++;
        previousPositionIndex = nextPositionIndex;

        switch (nextPositionIndex)
        {
            case 0: return top;
            case 1: return mid;
            case 2: return bottom;
            default: return mid; // Fallback to mid in case of an error
        }
    }

    int GetNextPositionIndex()
    {
        // Check for the position that hasn't been used for the longest time
        for (int i = 0; i < positionUsage.Length; i++)
        {
            if (positionUsage[i] == 0)
            {
                return i;
            }
        }

        // If all positions have been used, reset the usage counts
        positionUsage[0] = positionUsage[1] = positionUsage[2] = 0;

        // Ensure we don't repeat the same position consecutively
        List<int> availablePositions = new List<int> {0, 1, 2};
        availablePositions.Remove(previousPositionIndex);
        return availablePositions[Random.Range(0, availablePositions.Count)];
    }

    GameObject GetPooledFog()
    {
        foreach (GameObject fog in fogPool)
        {
            if (!fog.activeInHierarchy)
            {
                return fog;
            }
        }
        return null;
    }

    void CheckAndDeactivateFog()
    {
        foreach (GameObject fog in fogPool)
        {
            if (fog.activeInHierarchy)
            {
                if (ShouldDeactivateFog(fog.transform))
                {
                    // Debug log to check the position of deactivation
                    Debug.Log($"Deactivating fog at position: {fog.transform.position}");
                    fog.SetActive(false);
                }
            }
        }
    }

    bool ShouldDeactivateFog(Transform fogTransform)
    {
        if (SpawnAtLeft)
        {
            if (endAtFarPosition)
            {
                return fogTransform.position.x >= ParticlePosition.Instance.right_top_01.position.x;
            }
            else
            {
                return fogTransform.position.x >= ParticlePosition.Instance.right_top_00.position.x;
            }
        }
        else
        {
            if (endAtFarPosition)
            {
                return fogTransform.position.x <= ParticlePosition.Instance.left_top_01.position.x;
            }
            else
            {
                return fogTransform.position.x <= ParticlePosition.Instance.left_top_00.position.x;
            }
        }
    }

    void ResetFog(GameObject fog, Vector3 newPosition)
    {
        // Reset any necessary components or variables here
        Rigidbody2D rb = fog.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // Reset the Parallax component
        Parallax parallax = fog.GetComponent<Parallax>();
        if (parallax != null)
        {
            parallax.ResetParallax(newPosition);
        }

        // Add any other necessary resets
    }
}
