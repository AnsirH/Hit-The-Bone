using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : Singleton<EntityManager>
{
    // game object�� id ������ ��ü�� ����
    SortedList<int, EntityBase> idToEntity = new SortedList<int, EntityBase>();

    public SortedList<int, EntityBase> IdToEntity => idToEntity;

    /// <summary>
    /// ��ü ���
    /// </summary>
    /// <param name="entity"></param>
    public void AddEntity(EntityBase entity)
    {
        int id = entity.gameObject.GetInstanceID();
        if (idToEntity.ContainsKey(id))
        {
            Debug.LogError("�̹� ��ϵ� ��ü�Դϴ�. �߰��� �� �����ϴ�.");
            return;
        }
        else
        {
            idToEntity.Add(id, entity);
        }
    }

    /// <summary>
    /// ��ü ����
    /// </summary>
    /// <param name="entity"></param>
    public void RemoveEntity(EntityBase entity)
    {
        int id = entity.gameObject.GetInstanceID();

        if (!idToEntity.ContainsKey(id))
        {
            Debug.LogError("��ϵ��� ���� ��ü�Դϴ�. ������ �� �����ϴ�.");
            return;
        }
        else
        {
            idToEntity.Remove(id);
        }
    }

    /// <summary>
    /// ��ü ����
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public EntityBase GetEntity(GameObject obj)
    {
        if (idToEntity.TryGetValue(obj.GetInstanceID(), out EntityBase entity))
        {
            return entity;
        }
        else
        {
            Debug.LogError("EntityManager�� �������� �ʴ� ��ü�Դϴ�.");
            return null;
        }
    }

    public List<Tower> GetAroundTowers(EntityBase centerEntity, float range)
    {
        float distance;
        List<Tower> resultTowers = new List<Tower>();
        foreach (EntityBase tower in idToEntity.Values)
        {
            if (tower is Tower)
            {
                distance = Vector3.Distance(centerEntity.transform.position, tower.transform.position);
                if (distance <= range)
                {
                    resultTowers.Add(tower as Tower);
                }
            }            
        }

        if (resultTowers.Count > 0)
        {
            return resultTowers;
        }
        else
        {
            return null;
        }
    }

    public Monster[] GetAroundMonsters(EntityBase centerEntity, float range)
    {
        Monster[] result;

        Collider[] colliders = Physics.OverlapSphere(centerEntity.transform.position, range, LayerMask.GetMask("Monster"));
        result = new Monster[colliders.Length];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = idToEntity[colliders[i].gameObject.GetInstanceID()] as Monster;
        }

        return result;
    }

    public List<EntityBase> GetAroundEntities(EntityBase centerEntity, float range)
    {
        float distance;
        List<EntityBase> resultEntities = new List<EntityBase>();
        foreach (EntityBase entity in idToEntity.Values)
        {
            distance = Vector3.Distance(centerEntity.transform.position, entity.transform.position);
            if (distance <= range)
            {
                resultEntities.Add(entity);
            }
        }

        if (resultEntities.Count > 0)
        {
            return resultEntities;
        }
        else
        {
            return null;
        }
    }
}
