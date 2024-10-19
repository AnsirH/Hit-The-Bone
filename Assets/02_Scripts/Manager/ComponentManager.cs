using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ManagedComponents<T> where T : Component
{
    protected SortedList<int, T> idToComponent = new SortedList<int, T>();

    public SortedList<int, T> IdToComponent => idToComponent;

    /// <summary>
    /// 컴포넌트 등록
    /// </summary>
    /// <param name="component">등록할 컴포넌트</param>
    protected void AddComponent(T component)
    {
        int id = component.gameObject.GetInstanceID();
        if (idToComponent.ContainsKey(id))
        {
            Debug.LogError($"이미 등록된 {nameof(T)}입니다. 추가할 수 없습니다.");
            return;
        }
        else
        {
            idToComponent.Add(id, component);
        }
    }

    /// <summary>
    /// 컴포넌트 삭제
    /// </summary>
    /// <param name="component">제거할 컴포넌트</param>
    protected void RemoveComponent(T component)
    {
        int id = component.gameObject.GetInstanceID();

        if (!idToComponent.ContainsKey(id))
        {
            Debug.LogError($"등록되지 않은 {nameof(T)}입니다. 삭제할 수 없습니다.");
            return;
        }
        else
        {
            idToComponent.Remove(id);
        }
    }

    /// <summary>
    /// 현재 관리되고 있는 컴포넌트를 게임 오브젝트로 가져온다.
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
            Debug.LogError($"{nameof(this.GetType)}에 존재하지 않는 객체입니다.");
            return null;
        }
    }

    /// <summary>
    /// center로 지정한 Transform 기준 range 범위에 있는 컴포넌트들을 리스트로 반환한다.
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
