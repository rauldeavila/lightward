using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ColorLightSystem : MonoBehaviour
{
    public Color tintColor = Color.white; // Public color picker, default to white
    public Color lightLayerTintColor = Color.white; // Public color picker for light layer, default to white
    public Color darkLayerTintColor = Color.white; // Public color picker for dark layer, default to white

    void Start()
    {
        SetupColorLightSystem();
    }

    void SetupColorLightSystem()
    {
        // RoomConfigurations.Instance.Data.LightIntensity
        // Load the ColorLight material from the Resources folder
        Material colorLightMaterial = Resources.Load<Material>("ColorLight-LVL23"); // DEFAULT
        Material colorLightMaterialDarkRoom = Resources.Load<Material>("ColorLight-LVL01");
        Material colorDarkMaterial = Resources.Load<Material>("ColorDark-LVL23"); // DEFAULT
        Material colorDarkMaterialDarkRoom = Resources.Load<Material>("ColorDark-LVL01");

        switch (RoomConfigurations.CurrentRoom.Data.LightIntensity)
        {
            case 0:
                colorLightMaterial = colorLightMaterialDarkRoom;
                colorDarkMaterial = colorDarkMaterialDarkRoom;
                break;
            case 1:
                colorLightMaterial = colorLightMaterialDarkRoom;
                colorDarkMaterial = colorDarkMaterialDarkRoom;
                break;
            default:
                break;
        }

        // Load the Sprite-Unlit-Default material from the Resources folder
        Material brightMaterial = Resources.Load<Material>("Sprite-Unlit-Default-Copy");
        if (brightMaterial == null)
        {
            Debug.LogError("Sprite-Unlit-Default material not found. Ensure it is available in the Resources folder.");
            return;
        }

        // Check if there is a SpriteRenderer in the current object
        SpriteRenderer parentSpriteRenderer = GetComponent<SpriteRenderer>();
        TilemapRenderer parentTilemapRenderer = GetComponent<TilemapRenderer>();

        if (parentSpriteRenderer != null)
        {
            parentSpriteRenderer.material = colorLightMaterial;
            parentSpriteRenderer.color = tintColor; // Set the tint color
            CreateLayersForSpriteRenderer(parentSpriteRenderer, brightMaterial, colorLightMaterial, colorDarkMaterial);
        }
        else if (parentTilemapRenderer != null)
        {
            parentTilemapRenderer.material = colorLightMaterial;
            GetComponent<Tilemap>().color = tintColor; // Set the tint color
            CreateLayersForTilemapRenderer(parentTilemapRenderer, brightMaterial, colorLightMaterial, colorDarkMaterial);
        }
    }

    void CreateLayersForSpriteRenderer(SpriteRenderer parentSpriteRenderer, Material brightMaterial, Material colorLightMaterial, Material colorDarkMaterial)
    {
        // Create the first child object with the bright material
        GameObject brightChild = new GameObject("BrightLayer");
        brightChild.transform.SetParent(transform);
        brightChild.transform.localPosition = Vector3.zero;

        SpriteRenderer brightSpriteRenderer = brightChild.AddComponent<SpriteRenderer>();
        brightSpriteRenderer.sprite = parentSpriteRenderer.sprite;
        brightSpriteRenderer.material = brightMaterial;
        brightSpriteRenderer.sortingLayerID = parentSpriteRenderer.sortingLayerID;
        brightSpriteRenderer.sortingOrder = parentSpriteRenderer.sortingOrder + 2;
        brightSpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        brightSpriteRenderer.color = lightLayerTintColor; // Set the light layer tint color

        // Create the second child object with the original material
        GameObject darkChild = new GameObject("DarkLayer");
        darkChild.transform.SetParent(transform);
        darkChild.transform.localPosition = Vector3.zero;

        SpriteRenderer darkSpriteRenderer = darkChild.AddComponent<SpriteRenderer>();
        darkSpriteRenderer.sprite = parentSpriteRenderer.sprite;
        darkSpriteRenderer.material = colorDarkMaterial;
        darkSpriteRenderer.sortingLayerID = parentSpriteRenderer.sortingLayerID;
        darkSpriteRenderer.sortingOrder = parentSpriteRenderer.sortingOrder;
        darkSpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        darkSpriteRenderer.color = darkLayerTintColor != Color.white ? darkLayerTintColor : tintColor; // Set the dark layer tint color
    }

    void CreateLayersForTilemapRenderer(TilemapRenderer parentTilemapRenderer, Material brightMaterial, Material colorLightMaterial, Material colorDarkMaterial)
    {
        // Create the first child object with the bright material
        GameObject brightChild = new GameObject("BrightLayer");
        brightChild.transform.SetParent(transform);
        brightChild.transform.localPosition = Vector3.zero;

        Tilemap brightTilemap = brightChild.AddComponent<Tilemap>();
        TilemapRenderer brightTilemapRenderer = brightChild.AddComponent<TilemapRenderer>();
        brightTilemapRenderer.material = brightMaterial;
        brightTilemapRenderer.sortingLayerID = parentTilemapRenderer.sortingLayerID;
        brightTilemapRenderer.sortingOrder = parentTilemapRenderer.sortingOrder + 2;
        brightTilemapRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        brightTilemap.color = lightLayerTintColor;// Set the light layer tint color

        // Copy tiles from parent to bright layer
        CopyTilemap(parentTilemapRenderer.GetComponent<Tilemap>(), brightTilemap);

        // Create the second child object with the original material
        GameObject darkChild = new GameObject("DarkLayer");
        darkChild.transform.SetParent(transform);
        darkChild.transform.localPosition = Vector3.zero;

        Tilemap darkTilemap = darkChild.AddComponent<Tilemap>();
        TilemapRenderer darkTilemapRenderer = darkChild.AddComponent<TilemapRenderer>();
        darkTilemapRenderer.material = colorDarkMaterial;
        darkTilemapRenderer.sortingLayerID = parentTilemapRenderer.sortingLayerID;
        darkTilemapRenderer.sortingOrder = parentTilemapRenderer.sortingOrder;
        darkTilemapRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        darkTilemap.color = darkLayerTintColor != Color.white ? darkLayerTintColor : tintColor; // Set the dark layer tint color

        // Copy tiles from parent to dark layer
        CopyTilemap(parentTilemapRenderer.GetComponent<Tilemap>(), darkTilemap);
    }

    void CopyTilemap(Tilemap source, Tilemap destination)
    {
        destination.ClearAllTiles();
        BoundsInt bounds = source.cellBounds;
        TileBase[] allTiles = source.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                for (int z = 0; z < bounds.size.z; z++)
                {
                    TileBase tile = allTiles[x + bounds.size.x * (y + bounds.size.y * z)];
                    if (tile != null)
                    {
                        Vector3Int position = new Vector3Int(bounds.xMin + x, bounds.yMin + y, bounds.zMin + z);
                        destination.SetTile(position, tile);

                        // Copy tile properties such as rotation, color, etc.
                        destination.SetTransformMatrix(position, source.GetTransformMatrix(position));
                        destination.SetColor(position, source.GetColor(position));
                    }
                }
            }
        }
    }
}