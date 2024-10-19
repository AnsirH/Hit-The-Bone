using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagedTiles : ManagedComponents<TileData>
{
    public void AddTile(TileData tile)
    {
        AddComponent(tile);
    }

    public void RemoveTile(TileData tile)
    {
        RemoveComponent(tile);
    }

    public TileData GetTileData(GameObject obj)
    {
        return GetComponent(obj);
    }

    public TileData[] GetAroundTiles(Transform center, float range, string tag)
    {
        List<TileData> tiles = GetAroundComponents(center, range);

        if (tiles.Count == 0)
        {
            return null;
        }

        tiles = tiles.FindAll(x => x.CompareTag(tag));

        if (tiles.Count == 0)
        {
            return null;
        }

        TileData[] result = new TileData[tiles.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = tiles[i];
        }

        return result;
    }
}