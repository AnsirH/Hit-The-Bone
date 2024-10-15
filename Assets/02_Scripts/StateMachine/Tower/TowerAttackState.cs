using StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackState : StateBase<Tower>
{
    public override void EnterState(Tower entity)
    {
        Debug.Log(entity.name + " 공격 개시");
        if (entity.Target != null)
        {
            entity.Target.Hit(entity);
            entity.StartCooldown(Tower.CooldownState.Attack);
        }

        entity.ChangeState(Tower.State.Attackable);
    }

    public override void Execute_FixedUpdate(Tower entity)
    {
        return;
    }

    public override void Execute_LateUpdate(Tower entity)
    {
        return;
    }

    public override void Execute_Update(Tower entity)
    {
        return;
    }

    public override void ExitState(Tower entity)
    {
        return;
    }
}
