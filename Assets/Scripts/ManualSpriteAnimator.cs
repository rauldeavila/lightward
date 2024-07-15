using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualSpriteAnimator : MonoBehaviour
{
    public List<Sprite> sprites;
    public float frameTimeInMilliseconds;

    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex;
    private Coroutine animationCoroutine;

    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSpriteIndex = 0;

        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
        animationCoroutine = StartCoroutine(AnimateSprite());
    }

    void OnDisable()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
    }

    IEnumerator AnimateSprite()
    {
        while (true)
        {
            spriteRenderer.sprite = sprites[currentSpriteIndex];
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Count;

            float frameTimeInSeconds = frameTimeInMilliseconds / 1000.0f;
            yield return new WaitForSeconds(frameTimeInSeconds);
        }
    }
}
