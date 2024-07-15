using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class TilemapCulling : MonoBehaviour
{
    public Tilemap tilemap;
    public float maxDistance = 10f;
    public float updateDelay = 0.5f;

    private Vector3Int previousPlayerTilePosition;
    private TileBase[,] disabledTiles;

    private Coroutine cullingCoroutine;

    private void Start()
    {
        previousPlayerTilePosition = tilemap.WorldToCell(PlayerController.Instance.transform.position);
        disabledTiles = new TileBase[tilemap.size.x, tilemap.size.y];

        cullingCoroutine = StartCoroutine(CullingUpdateCoroutine());
    }

    private IEnumerator CullingUpdateCoroutine()
    {
        while (true)
        {
            Vector3Int currentPlayerTilePosition = tilemap.WorldToCell(PlayerController.Instance.transform.position);

            if (currentPlayerTilePosition != previousPlayerTilePosition)
            {
                DisableDistantTiles(currentPlayerTilePosition);
                EnableCloseTiles(currentPlayerTilePosition);
            }

            previousPlayerTilePosition = currentPlayerTilePosition;

            yield return new WaitForSeconds(updateDelay);
        }
    }

    private void DisableDistantTiles(Vector3Int currentPlayerTilePosition)
    {
        BoundsInt bounds = tilemap.cellBounds;

        foreach (var position in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(position))
            {
                float distance = Vector3.Distance(tilemap.GetCellCenterWorld(position), PlayerController.Instance.transform.position);

                if (distance > maxDistance)
                {
                    TileBase tile = tilemap.GetTile(position);
                    tilemap.SetTile(position, null);
                    disabledTiles[position.x - bounds.xMin, position.y - bounds.yMin] = tile;
                }
            }
        }
    }

    private void EnableCloseTiles(Vector3Int currentPlayerTilePosition)
    {
        BoundsInt bounds = tilemap.cellBounds;

        foreach (var position in bounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(position) && disabledTiles[position.x - bounds.xMin, position.y - bounds.yMin] != null)
            {
                float distance = Vector3.Distance(tilemap.GetCellCenterWorld(position), PlayerController.Instance.transform.position);

                if (distance <= maxDistance && position != currentPlayerTilePosition)
                {
                    tilemap.SetTile(position, disabledTiles[position.x - bounds.xMin, position.y - bounds.yMin]);
                    disabledTiles[position.x - bounds.xMin, position.y - bounds.yMin] = null;
                }
            }
        }
    }
}
