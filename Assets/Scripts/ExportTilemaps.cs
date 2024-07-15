using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using UnityEngine;

public class ExportTilemaps : MonoBehaviour
{
    public Camera tempCamera;
    public Tilemap[] tilemaps;

    void Start()
    {
        Vector3Int minCoord = new Vector3Int(int.MaxValue, int.MaxValue, 0);
        Vector3Int maxCoord = new Vector3Int(int.MinValue, int.MinValue, 0);

        foreach (Tilemap tilemap in tilemaps)
        {
            BoundsInt bounds = tilemap.cellBounds;
            minCoord = Vector3Int.Min(minCoord, bounds.min);
            maxCoord = Vector3Int.Max(maxCoord, bounds.max);
        }

        int textureWidth = maxCoord.x - minCoord.x + 1;
        int textureHeight = maxCoord.y - minCoord.y + 1;

        // The rest is mostly the same, but use textureWidth and textureHeight when creating the RenderTexture
        RenderTexture renderTexture = new RenderTexture(textureWidth, textureHeight, 24);
        tempCamera.targetTexture = renderTexture;
        tempCamera.Render();

        Texture2D finalTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.ARGB32, false);
        RenderTexture.active = renderTexture;
        finalTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        finalTexture.Apply();

        RenderTexture.active = null;
        Destroy(renderTexture);

        byte[] bytes = finalTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/DynamicTexture.png", bytes);

        Debug.Log("Texture saved at " + Application.dataPath + "/DynamicTexture.png");
    }
}
