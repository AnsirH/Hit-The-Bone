using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectile : Projectile
{
    public float explosionRange = 1f;

    protected override void CollisionAction()
    {
        base.CollisionAction();
        Monster[] monsters = ComponentManager.Instance.ManagedEntities.GetAroundMonsters(transform, explosionRange);
        foreach (Monster monster in monsters)
        {
            monster.Hit(sender);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
