using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskUpdater : MonoBehaviour
{
    private SpriteMask _spriteMask; // The sprite mask to be updated
    public int FrameRate = 24; // frames per second
    public bool RandomizeFrameRate = false;
    public bool PingPong = false; // Whether to ping-pong the animation
    public Sprite[] Frames; // frames to animate

    private int _currentFrameIndex = 0; // Index of the current frame
    private int _direction = 1; // Direction of animation (1 for forward, -1 for backward)
    private float _timePerFrame; // Time per frame
    private float _timer; // Timer to keep track of time

    void Start()
    {
        // Get the SpriteMask component
        _spriteMask = GetComponent<SpriteMask>();

        // Calculate the time per frame
        _timePerFrame = 1f / FrameRate;
    }

    void Update()
    {
        if(GameState.Instance.Darkworld)
        {
            _spriteMask.sprite = null;
            return;
        }
        // Increment the timer by the time passed since the last frame
        if(RandomizeFrameRate)
        {
            FrameRate = Random.Range(FrameRate - 5, FrameRate + 6);
            _timePerFrame = 1f / FrameRate;
        }

        _timer += Time.deltaTime;

        // If enough time has passed for the next frame
        if (_timer >= _timePerFrame)
        {
            // Reset the timer
            _timer = 0f;

            // Increment or decrement the frame index based on direction
            _currentFrameIndex += _direction;

            // If ping-pong is enabled and we've reached the first or last frame, change direction
            if (PingPong && (_currentFrameIndex == Frames.Length - 1 || _currentFrameIndex == 0))
            {
                _direction *= -1; // Reverse the direction
            }
            else if (_currentFrameIndex >= Frames.Length) // If we've reached the end of the frames array, loop back to the beginning
            {
                _currentFrameIndex = 0;
            }
            else if (_currentFrameIndex < 0) // If we've reached the beginning of the frames array and ping-pong is enabled, set direction to forward
            {
                _currentFrameIndex = 1;
                _direction = 1;
            }

            // Update the sprite mask's sprite to the current frame
            _spriteMask.sprite = Frames[_currentFrameIndex];
        }
    }
}
