using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public GameObject[] Tiles;
    public float fallDelay = 0.12f; // Delay between each tile falling
    public float respawnDelay = 3f;
    private Vector3[] originalTilePositions;

    void Start()
    {
        originalTilePositions = new Vector3[Tiles.Length];
        for(int i = 0; i < Tiles.Length; i++)
        {
            originalTilePositions[i] = Tiles[i].transform.position;
        }
    }

    public void StartCollapse() 
    {
        StartCoroutine(CollapseSequence());
    }

    IEnumerator CollapseSequence()
    {
        yield return new WaitForSeconds(fallDelay); 
        for (int i = 0; i < Tiles.Length; i++)
        {
            GameObject tile = Tiles[i];

            // Make the tile fall
            Rigidbody2D tileRb = tile.GetComponent<Rigidbody2D>();
            BoxCollider2D tileCol = tile.GetComponent<BoxCollider2D>();
            tileRb.bodyType = RigidbodyType2D.Dynamic; // Enable gravity
            tileCol.enabled = false; // Disable Collider

            // Optional: Add slight random force
            tileRb.AddForce(new Vector2(Random.Range(-0.5f, 0.5f), 0f), ForceMode2D.Impulse);

            // Start fade out and destroy coroutine
            StartCoroutine(FadeAndRespawn(tile));
        }
    }

    IEnumerator FadeAndRespawn(GameObject tile)
    {
        yield return new WaitForSeconds(0.5f); // Delay before fade

        SpriteRenderer tileRenderer = tile.GetComponent<SpriteRenderer>();
        float fadeTime = 1f;

        for (float t = 0f; t < fadeTime; t += Time.deltaTime)
        {
            Color newColor = tileRenderer.color;
            newColor.a = Mathf.Lerp(1f, 0f, t / fadeTime);
            tileRenderer.color = newColor;
            yield return null;
        }

        yield return new WaitForSeconds(respawnDelay);

        for (int i = 0; i < Tiles.Length; i++)
        {

                Tiles[i].transform.position = originalTilePositions[i]; 
                Rigidbody2D tileRb = Tiles[i].GetComponent<Rigidbody2D>();
                tileRb.bodyType = RigidbodyType2D.Static;
                tileRb.velocity = Vector2.zero;

                BoxCollider2D tileCol = Tiles[i].GetComponent<BoxCollider2D>();
                tileCol.enabled = true; 

                SpriteRenderer m_tileRenderer = Tiles[i].GetComponent<SpriteRenderer>();
                tileRenderer.color = new Color(tileRenderer.color.r, tileRenderer.color.g, tileRenderer.color.b, 1f);
                StartCoroutine(PunchAnimation(Tiles[i]));
        } 
    }

    IEnumerator PunchAnimation(GameObject tile)
    {
        float animationTime = 0.3f; // How long the punch animation lasts
        float overshootScale = 1.3f; // How much the tile "overshoots" its normal size

        // Overshoot
        for (float t = 0f; t < animationTime / 2; t += Time.deltaTime) 
        {
            float progress = t / (animationTime / 2); // Scale up progress 
            float scale = Mathf.Lerp(1f, overshootScale, progress);
            tile.transform.localScale = Vector3.one * scale;
            yield return null;
        }

        // Scale back down
        for (float t = 0f; t < animationTime / 2; t += Time.deltaTime) 
        {
            float progress = t / (animationTime / 2); // Scale down progress
            float scale = Mathf.Lerp(overshootScale, 1f, progress);
            tile.transform.localScale = Vector3.one * scale;
            yield return null;
        }

        // Ensure it ends up at 100% scale
        tile.transform.localScale = Vector3.one;
    }

}