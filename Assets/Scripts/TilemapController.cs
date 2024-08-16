using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] Color highlightColor = Color.yellow;
    [SerializeField] Vector3Int previousTilePosition;
    

    private void Awake()
    {
        if (tilemap == null)
        {
            Debug.LogError("Tilemap component not assigned.");
        }
    }

    public Vector3Int FindNearestTile(Vector3 position)
    {
        float minDistance = Mathf.Infinity;
        Vector3Int nearestCell = Vector3Int.zero;

        foreach (Vector3Int tilePosition in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(tilePosition))
            {
                Vector3 tileWorldPosition = tilemap.GetCellCenterWorld(tilePosition);
                float distance = Vector3.Distance(position, tileWorldPosition);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestCell = tilePosition;
                }
            }
        }
        ChangeTileColor(nearestCell);
        return nearestCell;
    }

    public void ChangeTileColor(Vector3Int position)
    {
        Debug.Log("Attempting to change color at position " + position);

        if (tilemap.HasTile(position))
        {
            tilemap.SetTileFlags(position, TileFlags.None);
            tilemap.SetColor(position, highlightColor);
            Debug.Log("Color changed to " + highlightColor + " at position " + position);
        }
        else
        {
            Debug.LogWarning("No tile found at position " + position);
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.2f);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.2f);
        Gizmos.color = Color.blue;
        foreach (var collider in colliders)
        {
            Gizmos.DrawWireSphere(collider.transform.position, 1);
        }
    }
}
