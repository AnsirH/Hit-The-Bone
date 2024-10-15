using StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerIdleState : StateBase<Tower>
{
    public override void EnterState(Tower entity)
    {
        entity.Target = null;
    }

    public override void Execute_FixedUpdate(Tower entity)
    {
    }

    public override void Execute_LateUpdate(Tower entity)
    {
        if (entity.GetNearstMonster() != null)
        {
            entity.ChangeState(Tower.State.Attackable);
        }
    }

    public override void Execute_Update(Tower entity)
    {
    }

    public override void ExitState(Tower entity)
    {
    }
}
