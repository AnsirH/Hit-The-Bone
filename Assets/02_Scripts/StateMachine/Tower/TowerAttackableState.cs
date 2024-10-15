using StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackableState : StateBase<Tower>
{

    public override void EnterState(Tower entity)
    {
        Debug.Log(entity.name + " 공격 가능 상태 돌입.");
    }

    public override void Execute_Update(Tower entity)
    {
        

        //if ((cooldownState & (byte)CooldownState.Skill) == 1)
        //{
        //    skillCoolTimer += Time.deltaTime;

        //    if (skillCoolTimer)
        //}
    }

    public override void Execute_LateUpdate(Tower entity)
    {
        Monster target = entity.GetNearstMonster();
        if (target == null)
        {
            entity.ChangeState(Tower.State.Idle);
        }
        else
        {
            entity.Target = target;
            if (entity.IsAttackable)
            {
                entity.ChangeState(Tower.State.Attack);
            }
        }

        if (entity.Target != null)
        {
            entity.RotateHeadToTarget(entity.Target.transform);
        }
    }

    public override void Execute_FixedUpdate(Tower entity)
    {
    }

    public override void ExitState(Tower entity)
    {
    }
}
