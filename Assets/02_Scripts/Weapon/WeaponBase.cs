using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField]
    protected string projectileName;

    protected Tower owner;

    public abstract void Fire(Monster target);

    public virtual void SetData(Tower owner)
    {
        this.owner = owner;
    }
}
