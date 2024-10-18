using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>
{
    private SortedList<int, Projectile> idToProjectile = new SortedList<int, Projectile>();

    public SortedList<int, Projectile> IdToProjectil => idToProjectile;

    /// <summary>
    /// Projectile 등록
    /// </summary>
    /// <param name="projectile"></param>
    public void AddEntity(Projectile projectile)
    {
        int id = projectile.gameObject.GetInstanceID();
        if (idToProjectile.ContainsKey(id))
        {
            Debug.LogError("이미 등록된 개체입니다. 추가할 수 없습니다.");
            return;
        }
        else
        {
            idToProjectile.Add(id, projectile);
        }
    }

    /// <summary>
    /// Projectile 삭제
    /// </summary>
    /// <param name="projectile"></param>
    public void RemoveEntity(Projectile projectile)
    {
        int id = projectile.gameObject.GetInstanceID();

        if (!idToProjectile.ContainsKey(id))
        {
            Debug.LogError("등록되지 않은 개체입니다. 삭제할 수 없습니다.");
            return;
        }
        else
        {
            idToProjectile.Remove(id);
        }
    }

    /// <summary>
    /// Projectile 접근
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public Projectile GetProjectile(GameObject obj)
    {
        if (idToProjectile.TryGetValue(obj.GetInstanceID(), out Projectile projectile))
        {
            return projectile;
        }
        else
        {
            Debug.LogError("ProjectileManager에 존재하지 않는 객체입니다.");
            return null;
        }
    }
}
