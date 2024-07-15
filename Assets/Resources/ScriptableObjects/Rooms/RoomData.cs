using UnityEngine;

[CreateAssetMenu(fileName = "RoomData", menuName = "ScriptableObjects/RoomData", order = 1)]
public class RoomData : ScriptableObject
{
    public float SFXWindVolume = 0f;
    public float SFXGardensVolume = 0f;
    public float SFXThunderstormVolume = 0f;
    public float SFXRainVolume = 0f;
    public bool ThunderstormEffect = false;
    public int LightIntensity = 1; // 1 -> 3
    public string AreaName = "Graveyard"; // Forest | Catacombs | etc.
}
