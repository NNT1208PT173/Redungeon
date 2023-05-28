using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallingDisco : MonoBehaviour
{
    [SerializeField] float fallingTime = 2f;
    [SerializeField] Tilemap tilemap;

    private void OnTriggerStay2D(Collider2D collision)
    {
        StartCoroutine(DestroyTileAfterDelay(collision.transform.position));
    }

    private IEnumerator DestroyTileAfterDelay(Vector3 position)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(position);
        yield return new WaitForSeconds(fallingTime);

        TileBase tile = tilemap.GetTile(cellPosition);
        if (tile != null)
        {
            tilemap.SetTile(cellPosition, null);
        }
    }
}