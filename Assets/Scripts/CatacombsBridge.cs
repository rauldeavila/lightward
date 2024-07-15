using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatacombsBridge : MonoBehaviour
{
    public GameObject[] stoneTiles;
    public GameObject[] stoneFinalTiles; 
    public float fallDelay = 0.12f; // Delay between each tile falling

    public void StartCollapse() 
    {
        StartCoroutine(CollapseSequence());
    }

    public void FinalCollapse()
    {
        StartCoroutine(CollapseFinalTiles());
    }

    IEnumerator CollapseFinalTiles()
    {
        for (int i = 0; i < stoneFinalTiles.Length; i++)
        {
            GameObject tile = stoneFinalTiles[i];

            // Make the tile fall
            Rigidbody2D tileRb = tile.GetComponent<Rigidbody2D>();
            tileRb.bodyType = RigidbodyType2D.Dynamic; // Enable gravity

            // Optional: Add slight random force
            tileRb.AddForce(new Vector2(Random.Range(-0.5f, 0.5f), 0f), ForceMode2D.Impulse);

            // Start fade out and destroy coroutine
            StartCoroutine(FadeAndDestroy(tile));
            yield return new WaitForSeconds(0.05f); 

        }
    }

    IEnumerator CollapseSequence()
    {
        SFXController.Instance.Play("event:/game/02_graveyard/bridge_falling");
        for (int i = 5; i < stoneTiles.Length; i++)
        {
            GameObject tile = stoneTiles[i];

            // Make the tile fall
            Rigidbody2D tileRb = tile.GetComponent<Rigidbody2D>();
            tileRb.bodyType = RigidbodyType2D.Dynamic; // Enable gravity

            // Optional: Add slight random force
            tileRb.AddForce(new Vector2(Random.Range(-0.5f, 0.5f), 0f), ForceMode2D.Impulse);

            // Start fade out and destroy coroutine
            StartCoroutine(FadeAndDestroy(tile));
            if(i < 25)
            {
                yield return new WaitForSeconds(0f);
            } else {
                yield return new WaitForSeconds(fallDelay); 
            }
        }
    }

    IEnumerator FadeAndDestroy(GameObject tile)
    {
        yield return new WaitForSeconds(2f); // Delay before fade

        if(tile != null){
            SpriteRenderer tileRenderer = tile.GetComponent<SpriteRenderer>();
            float fadeTime = 1f;  // Adjust fade duration

            for (float t = 0f; t < fadeTime; t += Time.deltaTime)
            {
                Color newColor = tileRenderer.color;
                newColor.a = Mathf.Lerp(1f, 0f, t / fadeTime);
                tileRenderer.color = newColor;
                yield return null;
            }
        }
        Destroy(tile);
    }
}
