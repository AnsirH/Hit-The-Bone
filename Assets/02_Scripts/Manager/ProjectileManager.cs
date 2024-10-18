using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>
{
    private SortedList<int, Projectile> idToProjectile = new SortedList<int, Projectile>();

    public SortedList<int, Projectile> IdToProjectil => idToProjectile;

    /// <summary>
    /// Projectile ���
    /// </summary>
    /// <param name="projectile"></param>
    public void AddEntity(Projectile projectile)
    {
        int id = projectile.gameObject.GetInstanceID();
        if (idToProjectile.ContainsKey(id))
        {
            Debug.LogError("�̹� ��ϵ� ��ü�Դϴ�. �߰��� �� �����ϴ�.");
            return;
        }
        else
        {
            idToProjectile.Add(id, projectile);
        }
    }

    /// <summary>
    /// Projectile ����
    /// </summary>
    /// <param name="projectile"></param>
    public void RemoveEntity(Projectile projectile)
    {
        int id = projectile.gameObject.GetInstanceID();

        if (!idToProjectile.ContainsKey(id))
        {
            Debug.LogError("��ϵ��� ���� ��ü�Դϴ�. ������ �� �����ϴ�.");
            return;
        }
        else
        {
            idToProjectile.Remove(id);
        }
    }

    /// <summary>
    /// Projectile ����
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
            Debug.LogError("ProjectileManager�� �������� �ʴ� ��ü�Դϴ�.");
            return null;
        }
    }
}
