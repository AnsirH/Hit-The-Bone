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
/// ���Ϳ� Ÿ���� ��ӹ޴� ��ũ��Ʈ. ���� �� ��ƼƼ�� Ÿ���� ���͸� ��Ī�Ѵ�.
/// </summary>
public class EntityBase : MonoBehaviour
{
    // ��ü�� ����
    protected EntityStats stat;

    // ��ٿ� ����
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
            Debug.LogError("Range�� 0��. Ž�� �Ұ�");
            return null;
        }

        List<EntityBase> entities = EntityManager.Instance.GetEntitiesAround(this, range);

        return entities;
    }
}