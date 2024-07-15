using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{
    [Header("Animation Settings")]
    public Sprite[] Frames;       // Array of frames for the animation
    public int FramesPerSecond = 2; // Animation speed (frames per second)
    public bool PingPong = false; // Whether the animation should ping pong

    private SpriteRenderer _spriteRenderer;
    private Image _uiImage;
    private int _currentFrame;
    private float _timer;
    private bool _isReversing;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _uiImage = GetComponent<Image>();
        _currentFrame = 0;
        _timer = 0f;
        _isReversing = false;

        if (Frames.Length > 0)
        {
            if (_spriteRenderer != null)
            {
                _spriteRenderer.sprite = Frames[_currentFrame];
            }
            else if (_uiImage != null)
            {
                _uiImage.sprite = Frames[_currentFrame];
            }
        }
    }

    void Update()
    {
        if (Frames.Length == 0) return;

        _timer += Time.deltaTime;

        if (_timer >= 1f / FramesPerSecond)
        {
            _timer -= 1f / FramesPerSecond;

            if (PingPong)
            {
                if (!_isReversing)
                {
                    _currentFrame++;
                    if (_currentFrame >= Frames.Length)
                    {
                        _currentFrame = Frames.Length - 2;
                        _isReversing = true;
                    }
                }
                else
                {
                    _currentFrame--;
                    if (_currentFrame < 0)
                    {
                        _currentFrame = 1;
                        _isReversing = false;
                    }
                }
            }
            else
            {
                _currentFrame++;
                if (_currentFrame >= Frames.Length)
                {
                    _currentFrame = 0;
                }
            }

            if (_spriteRenderer != null)
            {
                _spriteRenderer.sprite = Frames[_currentFrame];
            }
            else if (_uiImage != null)
            {
                _uiImage.sprite = Frames[_currentFrame];
            }
        }
    }
}