using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour
{
    public enum TileType
    {
        None,
        Empty,
        Waypoint,
        Tower,
        Effective
    }

    [SerializeField]
    TileType tileType;

    [SerializeField]
    Transform interactionTargetPoint;

    [SerializeField]
    EffectiveAreaTile effectiveAreaTile;

    public Transform InteractionTargetPoint => interactionTargetPoint;

    public EffectiveAreaTile EffectiveAreaTile => effectiveAreaTile;

    private void Awake()
    {
        ComponentManager.Instance.ManagedTiles.AddTile(this);
    }
}
