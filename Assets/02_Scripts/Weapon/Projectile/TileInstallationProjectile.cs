using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInstallationProjectile : Projectile
{
    public float installationRange = 2f;
    [SerializeField]
    string spawnTileName;

    [SerializeField]
    LayerMask tileLayer;

    protected override void CollisionAction()
    {
        TileData[] collidedTiles = ComponentManager.Instance.ManagedTiles.GetAroundTiles(transform, installationRange, "WaypointTile");

        foreach (TileData tile in collidedTiles)
        {
            GameObject effectiveTileObj = ObjectPooler.Instance.SpawnFromPool(spawnTileName, tile.InteractionTargetPoint.position);
            EffectiveAreaTile effectiveAreaTile = ComponentManager.Instance.ManagedTiles.GetTileData(effectiveTileObj).EffectiveAreaTile;
            if (effectiveAreaTile != null)
            {
                effectiveAreaTile.StartEffect(sender, 5f);
            }
        }
    }
}
