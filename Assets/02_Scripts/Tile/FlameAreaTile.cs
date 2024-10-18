using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAreaTile : EffectiveAreaTile
{
    public float damageTerm = 0.75f;
    public float damageRatio = 0.05f;

    [SerializeField]
    LayerMask monsterLayer;

    protected override IEnumerator EffectCoroutine(EntityBase owner, float duration)
    {
        Tower ownerTower = owner as Tower;

        float timer = 0f;

        while (timer < duration)
        {
            timer += damageTerm;

            Collider[] monsterCollider = Physics.OverlapBox(transform.position + Vector3.up * 0.5f, Vector3.one * 0.5f, Quaternion.identity, monsterLayer);

            foreach (Collider collider in monsterCollider)
            {
                Monster monster = EntityManager.Instance.IdToEntity[collider.gameObject.GetInstanceID()] as Monster;
                monster.Hit(ownerTower, damageRatio);
            }

            yield return new WaitForSeconds(damageTerm);
        }

        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + Vector3.up * 0.5f, Vector3.one);
    }
}
