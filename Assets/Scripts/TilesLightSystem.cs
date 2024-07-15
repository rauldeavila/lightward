using UnityEngine;
using UnityEngine.Tilemaps;
using Sirenix.OdinInspector;

public class TilesLightSystem : MonoBehaviour
{
    public RuleTile originalVersionRuleTile; // Assign the original version RuleTile in the inspector
    public RuleTile darkerVersionRuleTile; // Assign the darker version RuleTile in the inspector
    public Color DarkerColor = new Color(0.376f, 0.396f, 1.0f); // Default color #6065FF

    [Button("Adjust Tiles for Custom Light System")]
    public void GenerateTilemapCopies()
    {
        // Create the first copy
        GameObject firstCopy = CreateTilemapCopy(1, "Tiles", SpriteMaskInteraction.VisibleInsideMask, "Ground Mid");

        // Create the second copy
        GameObject secondCopy = CreateTilemapCopy(2, "Tiles", SpriteMaskInteraction.VisibleInsideMask, "Ground Bright");

        // Ensure the tilemaps are copied properly
        CopyTilemapData(gameObject, firstCopy, true);
        CopyTilemapData(gameObject, secondCopy);

        // Set the color and rule tile of the original tilemap
        SetTilemapColor(gameObject, DarkerColor);
        ReplaceTilemapDataWithDarkerVersion(gameObject);
    }

    [Button("Reset to Original Tilemap")]
    public void ResetToOriginalTilemap()
    {
        // Delete "Ground Mid" if it exists
        Transform groundMid = transform.parent.Find("Ground Mid");
        if (groundMid != null)
        {
            DestroyImmediate(groundMid.gameObject);
            Debug.Log("Removed Ground Mid tile.");
        }

        // Delete "Ground Bright" if it exists
        Transform groundBright = transform.parent.Find("Ground Bright");
        if (groundBright != null)
        {
            DestroyImmediate(groundBright.gameObject);
            Debug.Log("Removed Ground Bright tile.");
        }

        // Replace tilemap data with original version
        ReplaceTilemapDataWithOriginalVersion(gameObject);
    }

    private GameObject CreateTilemapCopy(int orderInLayer, string sortingLayerName, SpriteMaskInteraction maskInteraction, string copyName)
    {
        // Create a new GameObject
        GameObject copy = new GameObject(copyName);
        copy.transform.SetParent(transform.parent);
        copy.transform.position = transform.position;
        copy.transform.rotation = transform.rotation;
        copy.transform.localScale = transform.localScale;

        // Add and setup Tilemap component
        Tilemap tilemap = copy.AddComponent<Tilemap>();

        // Add and setup TilemapRenderer component
        TilemapRenderer tilemapRenderer = copy.AddComponent<TilemapRenderer>();
        tilemapRenderer.sortingLayerName = sortingLayerName;
        tilemapRenderer.sortingOrder = orderInLayer;
        tilemapRenderer.maskInteraction = maskInteraction;

        return copy;
    }

    private void CopyTilemapData(GameObject source, GameObject destination, bool useDarkerVersion = false)
    {
        Tilemap sourceTilemap = source.GetComponent<Tilemap>();
        Tilemap destinationTilemap = destination.GetComponent<Tilemap>();

        if (sourceTilemap != null && destinationTilemap != null)
        {
            destinationTilemap.ClearAllTiles();
            BoundsInt bounds = sourceTilemap.cellBounds;
            TileBase[] allTiles = sourceTilemap.GetTilesBlock(bounds);

            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    for (int z = 0; z < bounds.size.z; z++)
                    {
                        TileBase tile = allTiles[x + bounds.size.x * (y + bounds.size.y * z)];
                        if (tile != null)
                        {
                            if (useDarkerVersion && darkerVersionRuleTile != null)
                            {
                                destinationTilemap.SetTile(new Vector3Int(bounds.xMin + x, bounds.yMin + y, bounds.zMin + z), darkerVersionRuleTile);
                                Debug.Log("Generated Ground Mid tiles.");
                            }
                            else
                            {
                                destinationTilemap.SetTile(new Vector3Int(bounds.xMin + x, bounds.yMin + y, bounds.zMin + z), tile);
                                Debug.Log("Generated Ground Bright tiles.");
                            }
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Tilemap component not found on source or destination game object.");
        }
    }

    private void ReplaceTilemapDataWithDarkerVersion(GameObject target)
    {
        Tilemap tilemap = target.GetComponent<Tilemap>();

        if (tilemap != null && darkerVersionRuleTile != null)
        {
            BoundsInt bounds = tilemap.cellBounds;
            TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    for (int z = 0; z < bounds.size.z; z++)
                    {
                        TileBase tile = allTiles[x + bounds.size.x * (y + bounds.size.y * z)];
                        if (tile != null)
                        {
                            tilemap.SetTile(new Vector3Int(bounds.xMin + x, bounds.yMin + y, bounds.zMin + z), darkerVersionRuleTile);
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Tilemap or darker version RuleTile component not found on the target game object.");
        }
    }

    private void ReplaceTilemapDataWithOriginalVersion(GameObject target)
    {
        Tilemap tilemap = target.GetComponent<Tilemap>();

        if (tilemap != null && originalVersionRuleTile != null)
        {
            BoundsInt bounds = tilemap.cellBounds;
            TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    for (int z = 0; z < bounds.size.z; z++)
                    {
                        TileBase tile = allTiles[x + bounds.size.x * (y + bounds.size.y * z)];
                        if (tile != null)
                        {
                            tilemap.SetTile(new Vector3Int(bounds.xMin + x, bounds.yMin + y, bounds.zMin + z), originalVersionRuleTile);
                            Debug.Log("Reset original tile settings.");
                        }
                    }
                }
            }

            // Set the color to white
            tilemap.color = Color.white;
        }
        else
        {
            Debug.LogError("Tilemap or original version RuleTile component not found on the target game object.");
        }
    }

    private void SetTilemapColor(GameObject target, Color color)
    {
        Tilemap tilemap = target.GetComponent<Tilemap>();
        if (tilemap != null)
        {
            tilemap.color = color;
        }
        else
        {
            Debug.LogError("Tilemap component not found on the target game object.");
        }
    }
}
