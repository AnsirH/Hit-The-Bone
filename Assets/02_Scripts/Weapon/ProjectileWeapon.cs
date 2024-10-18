using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : WeaponBase
{    
    public override void Fire(Monster target)
    {
        if (target == null) return;

        GameObject projectileObj = ObjectPooler.Instance.SpawnFromPool(projectileName, transform.position);
        Projectile projectile = ProjectileManager.Instance.GetProjectile(projectileObj);
        projectile.SetData(owner, target);
    }
}
