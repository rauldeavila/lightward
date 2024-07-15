using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeatMapData", menuName = "WIZ/HeatMapData")]
public class HeatMapData : ScriptableObject {
    public Dictionary<string, int> heatMapFrequency = new Dictionary<string, int>();
}