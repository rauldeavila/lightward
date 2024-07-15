using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnimation : MonoBehaviour
{
    public bool WizInside = false;
    public Sprite[] DefaultLightSprites;
    public Sprite[] WizLightSprites;
    public float FrameRate = 12;
    private SpriteRenderer _sr;
    private Material _defaultMat;
    private Material _wizMat;
    private int _currentIndex = 0;
    private float _timer = 0f;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _defaultMat = Resources.Load<Material>("Materials/Glow/FireParticle");
        _wizMat = Resources.Load<Material>("Materials/Glow/FireParticleWIZ");
    }

    public void UpdateWizInside(bool inside)
    {
        if(inside)
        {
            WizInside = true;
        }
        else
        {
            WizInside = false;
        }
    }

    void Update()
    {
        Sprite[] spritesToUse = WizInside ? WizLightSprites : DefaultLightSprites;
        Material matToUse = WizInside ? _wizMat : _defaultMat;

        // Update sprite and material based on frame rate
        _timer += Time.deltaTime;
        if (spritesToUse.Length > 0)
        {
            if (_timer >= 1 / FrameRate)
            {
                _timer -= 1 / FrameRate;
                _currentIndex = (_currentIndex + 1) % spritesToUse.Length;
                _sr.sprite = spritesToUse[_currentIndex];
                _sr.material = matToUse;
            }
        }
    }
}
