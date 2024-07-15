using UnityEngine;

[CreateAssetMenu(fileName = "ColorMapping", menuName = "Lighting/ColorMapping", order = 1)]
public class ColorMapping : ScriptableObject
{
    [System.Serializable]
    public struct ColorMap
    {
        public Color originalColor;
        public Color brightLightColor;
        public Color mediumLightColor;
        public Color noLightColor;
    }

    public ColorMap[] colorMaps;
}
