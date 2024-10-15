using Stats;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public interface IDetectable
{
    EntityBase[] GetEntitiesAround();
}

/// <summary>
/// 몬스터와 타워가 상속받는 스크립트. 게임 내 엔티티는 타워와 몬스터를 지칭한다.
/// </summary>
public class EntityBase : MonoBehaviour
{
    // 개체의 스탯
    protected EntityStats stat;

    // 쿨다운 상태
    protected byte cooldownState = 0b00000000;

    public EntityStats Stat => stat;

    protected virtual void OnEnable()
    {
        Init();
        EntityManager.Instance.AddSpawnedEntity(this);
    }

    protected virtual void OnDisable()
    {
        ObjectPooler.Instance.ReturnToPool(gameObject);
        EntityManager.Instance.RemoveDisabledEntity(this);
        CancelInvoke();
    }

    protected virtual void Update()
    {
        stat.CheckBuffDuration();
    }

    public virtual void Init()
    {

    }
    
    protected virtual List<EntityBase> GetEntitiesAround(float range)
    {
        if (range <= 0)
        {
            Debug.LogError("Range가 0임. 탐지 불가");
            return null;
        }

        List<EntityBase> entities = EntityManager.Instance.GetEntitiesAround(this, range);

        return entities;
    }
}