using Stats;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

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
        ComponentManager.Instance.ManagedEntities.AddEntity(this);
    }

    protected virtual void OnDisable()
    {
        ComponentManager.Instance.ManagedEntities.RemoveEntity(this);

        ObjectPooler.Instance.ReturnToPool(gameObject);        
        CancelInvoke();
    }

    protected virtual void Update()
    {
        stat.CheckBuffDuration();
    }

    public virtual void Init()
    {

    }
    
    protected virtual List<EntityBase> GetAroundEntities(float range)
    {
        if (range <= 0)
        {
            Debug.LogError("Range�� 0��. Ž�� �Ұ�");
            return null;
        }

        List<EntityBase> entities = ComponentManager.Instance.ManagedEntities.GetAroundEntities(this, range);

        return entities;
    }
}