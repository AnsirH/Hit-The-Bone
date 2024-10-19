using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagedEntities : ManagedComponents<EntityBase>
{
    /// <summary>
    /// 按眉 殿废
    /// </summary>
    /// <param name="entity"></param>
    public void AddEntity(EntityBase entity)
    {
        AddComponent(entity);
    }

    /// <summary>
    /// 按眉 昏力
    /// </summary>
    /// <param name="entity"></param>
    public void RemoveEntity(EntityBase entity)
    {
        RemoveComponent(entity);
    }

    /// <summary>
    /// 按眉 立辟
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public EntityBase GetEntity(GameObject obj)
    {
        return GetComponent(obj);
    }

    public Tower[] GetAroundTowers(Transform center, float range)
    {
        List<EntityBase> entities = GetAroundComponents(center, range);

        if (entities.Count == 0)
        {
            return null;
        }
        entities = entities.FindAll(x => x is Tower);
        
        if (entities.Count == 0)
        {
            return null;
        }

        Tower[] result = new Tower[entities.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = (Tower)entities[i];
        }

        return result;
    }

    public Monster[] GetAroundMonsters(Transform center, float range)
    {
        List<EntityBase> entities = GetAroundComponents(center, range);

        if (entities.Count == 0)
        {
            return null;
        }
        entities = entities.FindAll(x => x is Monster);

        if (entities.Count == 0)
        {
            return null;
        }

        Monster[] result = new Monster[entities.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = (Monster)entities[i];
        }

        return result;
    }

    public List<EntityBase> GetAroundEntities(EntityBase centerEntity, float range)
    {
        return GetAroundComponents(centerEntity.transform, range);
    }
}
