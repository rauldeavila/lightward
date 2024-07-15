using UnityEngine;

[CreateAssetMenu(fileName = "DoorData", menuName = "ScriptableObjects/DoorData", order = 1)]
public class DoorData : ScriptableObject
{
    public string SceneName;
    public Vector2 SpawnPosition;
    public bool Up;
    public bool Down;
    public bool Left;
    public bool Right;
}



