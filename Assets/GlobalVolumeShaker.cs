using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalVolumeShaker : MonoBehaviour
{
    public Color lightColor = Color.white;
    public Color darkWorldColor = Color.red;
    public float shakeSpeed = 0.25f;

    private Volume volume;
    private Bloom bloom;
    private float timer = 0f;

    public static GlobalVolumeShaker Instance;
    private bool _darkWorldShaking = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void ToggleDarkWorldBloom()
    {
        volume = GetComponent<Volume>();
        if (volume.profile.TryGet(out bloom))
        {
            bloom.tint.overrideState = true;
        }
        _darkWorldShaking = !_darkWorldShaking;
        if (!_darkWorldShaking)
        {
            bloom.tint.value = lightColor; // Reset to light color when not in dark world
        }
    }

    void Start()
    {
        volume = GetComponent<Volume>();
        if (volume.profile.TryGet(out bloom))
        {
            bloom.tint.overrideState = true;
        }
    }

    void Update()
    {
        if (_darkWorldShaking && bloom != null)
        {
            timer += Time.deltaTime;
            float t = Mathf.PingPong(timer / shakeSpeed, 1f);

            bloom.tint.value = Color.Lerp(lightColor, darkWorldColor, t);
        }
    }
}