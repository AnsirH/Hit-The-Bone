using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EffectiveAreaTile : MonoBehaviour
{
    protected virtual void OnDisable()
    {
        ObjectPooler.Instance.ReturnToPool(gameObject);
        CancelInvoke();
        StopAllCoroutines();
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