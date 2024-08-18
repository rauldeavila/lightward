using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ColorLightSystem : MonoBehaviour
{
    public Color tintColor = Color.white; // Public color picker, default to white
    public Color lightLayerTintColor = Color.white; // Public color picker for light layer, default to white
    public Color darkLayerTintColor = Color.white; // Public color picker for dark layer, default to white

    private GameObject brightLayer;
    private GameObject darkLayer;

    private Coroutine breathingCoroutine;

    void Start()
    {
        SetupColorLightSystem();
        GameManager.Instance.OnEnterDarkworld.AddListener(() => 
        {
            UpdateColorLightSystem(true);
            StartBreathingEffect();
        });
        GameManager.Instance.OnExitDarkworld.AddListener(() => 
        {
            UpdateColorLightSystem(false);
            StopBreathingEffect();
        });
    }

    void SetupColorLightSystem()
    {
        var materials = GetMaterialsForCurrentRoom();

        // Set up the layers only once
        if (brightLayer == null && darkLayer == null)
        {
            SpriteRenderer parentSpriteRenderer = GetComponent<SpriteRenderer>();
            TilemapRenderer parentTilemapRenderer = GetComponent<TilemapRenderer>();

            if (parentSpriteRenderer != null)
            {
                SetupSpriteRendererLayers(parentSpriteRenderer, materials.colorLightMaterial, materials.colorDarkMaterial);
            }
            else if (parentTilemapRenderer != null)
            {
                SetupTilemapRendererLayers(parentTilemapRenderer, materials.colorLightMaterial, materials.colorDarkMaterial);
            }
        }
    }

    void UpdateColorLightSystem(bool isDarkworld)
    {
        var materials = GetMaterialsForCurrentRoom(isDarkworld);

        if (darkLayer != null)
        {
            SpriteRenderer darkSpriteRenderer = darkLayer.GetComponent<SpriteRenderer>();
            Tilemap darkTilemap = darkLayer.GetComponent<Tilemap>();

            if (darkSpriteRenderer != null)
            {
                darkSpriteRenderer.material = materials.colorDarkMaterial;
                darkSpriteRenderer.color = darkLayerTintColor; // Apply tint color for dark world
            }
            else if (darkTilemap != null)
            {
                darkTilemap.GetComponent<TilemapRenderer>().material = materials.colorDarkMaterial;
                darkTilemap.color = darkLayerTintColor; // Apply tint color for dark world
            }
        }
    }

    (Material colorLightMaterial, Material colorDarkMaterial) GetMaterialsForCurrentRoom(bool isDarkworld = false)
    {
        Material colorLightMaterial = Resources.Load<Material>("ColorLight-LVL23"); // DEFAULT
        Material colorDarkMaterial = Resources.Load<Material>("ColorDark-LVL23"); // DEFAULT

        switch (RoomConfigurations.CurrentRoom.Data.AreaName)
        {
            case "Forest":
                colorDarkMaterial = isDarkworld
                    ? Resources.Load<Material>("material-color-palettes/forest-darkworld")
                    : Resources.Load<Material>("material-color-palettes/forest-dark");
                break;
            case "Graveyard":
                colorDarkMaterial = isDarkworld
                    ? Resources.Load<Material>("material-color-palettes/graveyard-darkworld")
                    : Resources.Load<Material>("material-color-palettes/graveyard-dark");
                break;
            default:
                break;
        }

        return (colorLightMaterial, colorDarkMaterial);
    }

    void SetupSpriteRendererLayers(SpriteRenderer parentSpriteRenderer, Material colorLightMaterial, Material colorDarkMaterial)
    {
        Material brightMaterial = Resources.Load<Material>("Sprite-Unlit-Default-Copy");

        // Create the bright layer
        brightLayer = new GameObject("BrightLayer");
        brightLayer.transform.SetParent(transform);
        brightLayer.transform.localPosition = Vector3.zero;

        SpriteRenderer brightSpriteRenderer = brightLayer.AddComponent<SpriteRenderer>();
        brightSpriteRenderer.sprite = parentSpriteRenderer.sprite;
        brightSpriteRenderer.material = brightMaterial;
        brightSpriteRenderer.sortingLayerID = parentSpriteRenderer.sortingLayerID;
        brightSpriteRenderer.sortingOrder = parentSpriteRenderer.sortingOrder + 10;
        brightSpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        brightSpriteRenderer.color = lightLayerTintColor;

        // Create the dark layer
        darkLayer = new GameObject("DarkLayer");
        darkLayer.transform.SetParent(transform);
        darkLayer.transform.localPosition = Vector3.zero;

        SpriteRenderer darkSpriteRenderer = darkLayer.AddComponent<SpriteRenderer>();
        darkSpriteRenderer.sprite = parentSpriteRenderer.sprite;
        darkSpriteRenderer.material = colorDarkMaterial;
        darkSpriteRenderer.sortingLayerID = parentSpriteRenderer.sortingLayerID;
        darkSpriteRenderer.sortingOrder = parentSpriteRenderer.sortingOrder;
        darkSpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        darkSpriteRenderer.color = darkLayerTintColor != Color.white ? darkLayerTintColor : tintColor;
    }

    void SetupTilemapRendererLayers(TilemapRenderer parentTilemapRenderer, Material colorLightMaterial, Material colorDarkMaterial)
    {
        Material brightMaterial = Resources.Load<Material>("Sprite-Unlit-Default-Copy");

        // Create the bright layer
        brightLayer = new GameObject("BrightLayer");
        brightLayer.transform.SetParent(transform);
        brightLayer.transform.localPosition = Vector3.zero;

        Tilemap brightTilemap = brightLayer.AddComponent<Tilemap>();
        TilemapRenderer brightTilemapRenderer = brightLayer.AddComponent<TilemapRenderer>();
        brightTilemapRenderer.material = brightMaterial;
        brightTilemapRenderer.sortingLayerID = parentTilemapRenderer.sortingLayerID;
        brightTilemapRenderer.sortingOrder = parentTilemapRenderer.sortingOrder + 10;
        brightTilemapRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        brightTilemap.color = lightLayerTintColor;

        // Copy tiles from parent to bright layer
        CopyTilemap(parentTilemapRenderer.GetComponent<Tilemap>(), brightTilemap);

        // Create the dark layer
        darkLayer = new GameObject("DarkLayer");
        darkLayer.transform.SetParent(transform);
        darkLayer.transform.localPosition = Vector3.zero;

        Tilemap darkTilemap = darkLayer.AddComponent<Tilemap>();
        TilemapRenderer darkTilemapRenderer = darkLayer.AddComponent<TilemapRenderer>();
        darkTilemapRenderer.material = colorDarkMaterial;
        darkTilemapRenderer.sortingLayerID = parentTilemapRenderer.sortingLayerID;
        darkTilemapRenderer.sortingOrder = parentTilemapRenderer.sortingOrder;
        darkTilemapRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        darkTilemap.color = darkLayerTintColor != Color.white ? darkLayerTintColor : tintColor;

        // Copy tiles from parent to dark layer
        CopyTilemap(parentTilemapRenderer.GetComponent<Tilemap>(), darkTilemap);
    }

    void StartBreathingEffect()
    {
        if (breathingCoroutine == null && darkLayer != null)
        {
            breathingCoroutine = StartCoroutine(BreathingEffectCoroutine());
        }
    }

    void StopBreathingEffect()
    {
        if (breathingCoroutine != null)
        {
            StopCoroutine(breathingCoroutine);
            breathingCoroutine = null;
        }

        if (darkLayer != null)
        {
            ResetAlpha();
        }
    }

    IEnumerator BreathingEffectCoroutine()
    {
        SpriteRenderer darkSpriteRenderer = darkLayer.GetComponent<SpriteRenderer>();
        Tilemap darkTilemap = darkLayer.GetComponent<Tilemap>();

        Color originalColor = darkSpriteRenderer != null ? darkSpriteRenderer.color : darkTilemap.color;
        float alphaMin = 180f / 255f;
        float alphaMax = 255f / 255f;
        float duration = 2.0f; // Time to go from alphaMin to alphaMax

        while (true)
        {
            // Fade in
            yield return StartCoroutine(FadeAlpha(originalColor, alphaMin, alphaMax, duration));

            // Fade out
            yield return StartCoroutine(FadeAlpha(originalColor, alphaMax, alphaMin, duration));
        }
    }

    IEnumerator FadeAlpha(Color originalColor, float startAlpha, float endAlpha, float duration)
    {
        SpriteRenderer darkSpriteRenderer = darkLayer.GetComponent<SpriteRenderer>();
        Tilemap darkTilemap = darkLayer.GetComponent<Tilemap>();

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);

            if (darkSpriteRenderer != null)
            {
                darkSpriteRenderer.color = newColor;
            }
            else if (darkTilemap != null)
            {
                darkTilemap.color = newColor;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void ResetAlpha()
    {
        SpriteRenderer darkSpriteRenderer = darkLayer.GetComponent<SpriteRenderer>();
        Tilemap darkTilemap = darkLayer.GetComponent<Tilemap>();

        if (darkSpriteRenderer != null)
        {
            Color resetColor = darkSpriteRenderer.color;
            resetColor.a = 1f;
            darkSpriteRenderer.color = resetColor;
        }
        else if (darkTilemap != null)
        {
            Color resetColor = darkTilemap.color;
            resetColor.a = 1f;
            darkTilemap.color = resetColor;
        }
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
