using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EffectiveAreaTile : MonoBehaviour 
{
    [SerializeField]
    TileData tileData;

    void OnEnable()
    {
        ComponentManager.Instance.ManagedTiles.AddTile(tileData);
    }

    protected virtual void OnDisable()
    {
        ObjectPooler.Instance.ReturnToPool(gameObject);
        CancelInvoke();

        ComponentManager.Instance.ManagedTiles.RemoveTile(tileData);
    }


    public void StartEffect(EntityBase owner, float duration)
    {
        StartCoroutine(EffectCoroutine(owner, duration));
    }

    protected virtual IEnumerator EffectCoroutine(EntityBase owner, float duration)
    {
        yield return null;
    }
}