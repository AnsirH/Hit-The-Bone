using StatePattern;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : EntityBase
{
    [Header("타워 데이터"), SerializeField]
    TowerData data;

    [Header("Base Rotation Transform"), SerializeField]
    Transform baseRotation;

    [Header("Weapon"), SerializeField]
    private WeaponBase weapon;

    public WeaponBase Weapon => weapon;

    public enum CooldownState
    {
        Attack = 0b00000001,
        Skill = 0b00000010
    }

    float attackCoolTimer = 0.0f;
    float skillCoolTimer = 0.0f;

    public enum State
    {
        Idle,
        Attackable,
        Attack
    }

    private TowerStateMachine stateMachine;
    private StateBase<Tower>[] states = new StateBase<Tower>[3];
    public Monster Target;    
    public bool IsAttackable { get { return (cooldownState & (byte)CooldownState.Attack) == 0; } }    

    // Start is called before the first frame update
    void Awake()
    {
        stat = new TowerStats(data.Attack, data.AttackSpeed, data.Range);
        stateMachine = new TowerStateMachine();
        states[(int)State.Idle] = new TowerIdleState();
        states[(int)State.Attackable] = new TowerAttackableState();
        states[(int)State.Attack] = new TowerAttackState();

        stateMachine.SetUp(this, states[(int)State.Idle]);

        weapon.SetData(this);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        CheckCooldown();

        stateMachine.Updated();
    }

    private void LateUpdate()
    {
        stateMachine.LateUpdated();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdated();
    }

    protected override List<EntityBase> GetAroundEntities(float range)
    {
        List<EntityBase> returnList = base.GetAroundEntities(range);
        if (returnList == null)
        {
            return null;
        }
        else
        {
            return returnList.FindAll(x => x.CompareTag("Monster"));
        }
    }

    public void ChangeState(State state)
    {
        stateMachine.ChangeState(states[(int)state]);
    }

    public Monster GetNearstMonster()
    {
        List<EntityBase> nearMonsters = GetAroundEntities(stat.GetStat("Range"));
        if (nearMonsters == null || nearMonsters.Count == 0)
        {
            return null;
        }
        Monster resultMonster = nearMonsters[0] as Monster;

        float minDistance = Vector3.Distance(transform.position, resultMonster.transform.position);

        for (int i = 1; i < nearMonsters.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, nearMonsters[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                resultMonster = nearMonsters[i] as Monster;
            }
        }

        return resultMonster;
    }

    public void StartCooldown(CooldownState cooldownState)
    {
        switch (cooldownState)
        {
            case CooldownState.Attack:
                attackCoolTimer = 0.0f;
                this.cooldownState |= (byte)CooldownState.Attack;
                break;
            case CooldownState.Skill:
                skillCoolTimer = 0.0f;
                this.cooldownState |= (byte)CooldownState.Skill;
                break;
        }
    }

    public void RotateHeadToTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0.0f;
        baseRotation.rotation = Quaternion.LookRotation(direction);
    }

    private void CheckCooldown()
    {
        if ((cooldownState & (byte)CooldownState.Attack) == 1)
        {
            attackCoolTimer += Time.deltaTime * stat.GetStat("AttackSpeed");

            if (attackCoolTimer >= 1.0f)
            {
                cooldownState ^= (byte)CooldownState.Attack;
                attackCoolTimer = 0.0f;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (stat != null)
        {
            Gizmos.DrawWireSphere(transform.position, stat.GetStat("Range"));
        }
    }
}
