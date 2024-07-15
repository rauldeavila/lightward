using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSprite : MonoBehaviour
{
    public SpriteRenderer m_SpriteRenderer;
    public Sprite NewSprite;

    public void UpdateThisSprite()
    {
        m_SpriteRenderer.sprite = NewSprite;
    }
}
