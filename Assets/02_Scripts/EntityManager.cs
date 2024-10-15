using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : Singleton<EntityManager>
{
    [SerializeField]
    List<EntityBase> activeEntities = new List<EntityBase>();

    public List<EntityBase> ActiveEntities => activeEntities;

    public void AddSpawnedEntity(EntityBase entity)
    {
        activeEntities.Add(entity);
        CheckList();
    }

    public void RemoveDisabledEntity(EntityBase entity)
    {
        activeEntities.Remove(entity);
        CheckList();
    }

    public List<Tower> GetTowersAround(EntityBase centerEntity, float range)
    {
        float distance;
        List<Tower> resultTowers = new List<Tower>();
        foreach (Tower tower in activeEntities)
        {
            if (tower.CompareTag("Tower"))
            {
                distance = Vector3.Distance(centerEntity.transform.position, tower.transform.position);
                if (distance <= range)
                {
                    resultTowers.Add(tower);
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

    public List<EntityBase> GetEntitiesAround(EntityBase centerEntity, float range)
    {
        float distance;
        List<EntityBase> resultEntities = new List<EntityBase>();
        foreach (EntityBase entity in activeEntities)
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

    void CheckList()
    {
        foreach (var entity in activeEntities)
        {
            if (!entity.gameObject.activeSelf)
            {
                activeEntities.Remove(entity);
                continue;
            }
        }
    }
}
