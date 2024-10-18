using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleProjectileWeapon : WeaponBase
{
    [SerializeField]
    ProjectileWeapon[] projectileWeapons;

    public override void Fire(Monster target)
    {
        foreach (var weapon in projectileWeapons)
        {
            weapon.Fire(target);
        }
    }

    public override void SetData(Tower owner)
    {
        base.SetData(owner);
        foreach (var weapon in projectileWeapons)
        {
            weapon.SetData(owner);
        }
    }
}
