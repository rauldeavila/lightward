using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizLight : MonoBehaviour
{
    public List<GameObject> SoftLayers;
    public List<GameObject> BrightLayers;

    public int Level0Soft = 0;
    public int Level0Bright = 0;
    public int Level1Soft = 30;
    public int Level1Bright = 10;
    public int Level2Soft = 100;
    public int Level2Bright = 70;
    public int Level3Soft = 239;
    public int Level3Bright = 200;

    public static WizLight Instance;

    private Dictionary<int, int> softLevels;
    private Dictionary<int, int> brightLevels;

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

        softLevels = new Dictionary<int, int>
        {
            { 0, Level0Soft },
            { 1, Level1Soft },
            { 2, Level2Soft },
            { 3, Level3Soft }
        };

        brightLevels = new Dictionary<int, int>
        {
            { 0, Level0Bright },
            { 1, Level1Bright },
            { 2, Level2Bright },
            { 3, Level3Bright }
        };
    }

    void Start()
    {
        RoomConfigurations.OnRoomChanged.AddListener(UpdateLightLevel);
        GameManager.Instance.OnEnterDarkworld.AddListener(UpdateLightLevel);
        GameManager.Instance.OnExitDarkworld.AddListener(UpdateLightLevel);
        UpdateLightLevel();
    }

    void UpdateLightLevel()
    {
        if(GameState.Instance.Darkworld)
        {
            SetWizLightLevel(0);
        }
        else
        {
            if (RoomConfigurations.CurrentRoom != null)
            {
                SetWizLightLevel(RoomConfigurations.CurrentRoom.Data.LightIntensity);
            }
        }
    }

    public void SetWizLightLevel(int level)
    {
        StartCoroutine(LerpLightLevels(softLevels[level], brightLevels[level]));
    }

    private IEnumerator LerpLightLevels(int targetSoft, int targetBright)
    {
        float duration = 1.0f; // Duration of the lerp in seconds
        float elapsed = 0f;

        int currentSoft = GetCurrentRadius(SoftLayers);
        int currentBright = GetCurrentRadius(BrightLayers);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            int newSoft = Mathf.RoundToInt(Mathf.Lerp(currentSoft, targetSoft, t));
            int newBright = Mathf.RoundToInt(Mathf.Lerp(currentBright, targetBright, t));

            UpdateLayerMasks(SoftLayers, newSoft);
            UpdateLayerMasks(BrightLayers, newBright);

            yield return null;
        }

        UpdateLayerMasks(SoftLayers, targetSoft);
        UpdateLayerMasks(BrightLayers, targetBright);
    }

    private int GetCurrentRadius(List<GameObject> layers)
    {
        if (layers.Count > 0 && layers[0].GetComponent<SpriteMask>() != null)
        {
            string spriteName = layers[0].GetComponent<SpriteMask>().sprite.name;
            if (int.TryParse(spriteName.Replace("circle_radius_", ""), out int radius))
            {
                return radius;
            }
        }
        return 0;
    }

    private void UpdateLayerMasks(List<GameObject> layers, int radius)
    {
        foreach (var layer in layers)
        {
            var spriteMask = layer.GetComponent<SpriteMask>();
            if (spriteMask != null)
            {
                spriteMask.sprite = Resources.Load<Sprite>($"pixel-perfect-circles/circle_radius_{radius}");
            }
        }
    }
}
