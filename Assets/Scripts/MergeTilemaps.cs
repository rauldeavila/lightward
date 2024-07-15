using UnityEngine;
using UnityEngine.Tilemaps;

public class MergeTilemaps : MonoBehaviour
{
    public Tilemap destinationTilemap;  // The Tilemap to merge everything into

    void Start()
    {
        if (destinationTilemap == null)
        {
            Debug.LogError("Destination Tilemap is not set.");
            return;
        }

        Tilemap[] allTilemaps = FindObjectsOfType<Tilemap>();

        foreach (Tilemap sourceTilemap in allTilemaps)
        {
            // Skip the destination to avoid self-copying
            if (sourceTilemap == destinationTilemap) continue;

            BoundsInt bounds = sourceTilemap.cellBounds;
            TileBase[] tiles = sourceTilemap.GetTilesBlock(bounds);

            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    TileBase tile = tiles[x + y * bounds.size.x];
                    if (tile != null)
                    {
                        Vector3Int localPlace = new Vector3Int(x + bounds.x, y + bounds.y, 0);
                        Vector3Int globalPlace = sourceTilemap.LocalToCell(sourceTilemap.transform.TransformPoint(localPlace));
                        
                        Vector3Int destPlace = destinationTilemap.WorldToCell(destinationTilemap.transform.TransformPoint(globalPlace));
                        destinationTilemap.SetTile(destPlace, tile);
                    }
                }
            }
        }
    }
}
