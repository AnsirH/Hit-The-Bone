using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ManagedComponents<T> where T : Component
{
    protected SortedList<int, T> idToComponent = new SortedList<int, T>();

    public SortedList<int, T> IdToComponent => idToComponent;

    /// <summary>
    /// ������Ʈ ���
    /// </summary>
    /// <param name="component">����� ������Ʈ</param>
    protected void AddComponent(T component)
    {
        int id = component.gameObject.GetInstanceID();
        if (idToComponent.ContainsKey(id))
        {
            Debug.LogError($"�̹� ��ϵ� {nameof(T)}�Դϴ�. �߰��� �� �����ϴ�.");
            return;
        }
        else
        {
            idToComponent.Add(id, component);
        }
    }

    /// <summary>
    /// ������Ʈ ����
    /// </summary>
    /// <param name="component">������ ������Ʈ</param>
    protected void RemoveComponent(T component)
    {
        int id = component.gameObject.GetInstanceID();

        if (!idToComponent.ContainsKey(id))
        {
            Debug.LogError($"��ϵ��� ���� {nameof(T)}�Դϴ�. ������ �� �����ϴ�.");
            return;
        }
        else
        {
            idToComponent.Remove(id);
        }
    }

    /// <summary>
    /// ���� �����ǰ� �ִ� ������Ʈ�� ���� ������Ʈ�� �����´�.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    protected T GetComponent(GameObject obj)
    {
        if (idToComponent.TryGetValue(obj.GetInstanceID(), out T result))
        {
            return result;
        }
        else
        {
            Debug.LogError($"{nameof(this.GetType)}�� �������� �ʴ� ��ü�Դϴ�.");
            return null;
        }
    }

    /// <summary>
    /// center�� ������ Transform ���� range ������ �ִ� ������Ʈ���� ����Ʈ�� ��ȯ�Ѵ�.
    /// </summary>
    /// <param name="center"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    protected List<T> GetAroundComponents(Transform center, float range)
    {
        float distance;
        List<T> result = new List<T>();
        foreach (T component in idToComponent.Values)
        {
            distance = Vector3.Distance(center.position, component.transform.position);
            if (distance <= range)
            {
                result.Add(component);
            }
        }

        if (result.Count > 0)
        {
            return result;
        }
        else
        {
            return null;
        }
    }
}

public class ComponentManager : Singleton<ComponentManager>
{
    ManagedEntities managedEntities = new ManagedEntities();
    ManagedTiles managedTiles = new ManagedTiles();

    public ManagedEntities ManagedEntities => managedEntities;
    public ManagedTiles ManagedTiles => managedTiles;
}
