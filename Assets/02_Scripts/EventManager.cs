using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
    public UnityEvent<TowerData> onTowerButtonClick = new UnityEvent<TowerData>();

    public void AddListener(GameObject listener, UnityAction<TowerData> action)
    {
        onTowerButtonClick.AddListener(action);
        Debug.Log($"{listener.name}�� onTowerButtonClick �̺�Ʈ�� {nameof(action)}�� ����Ͽ����ϴ�.");
    }

    public void SendButtonEventInfo(ButtonBase button)
    {
        Debug.Log($"{button.name} ��ư Ŭ�� ��");
        if (button is TowerSpawnButton)
        {
            TowerSpawnButton towerSpawnButton = (TowerSpawnButton)button;
            Debug.Log("Ÿ�� ��ư Ŭ�� ��");
            onTowerButtonClick.Invoke(towerSpawnButton.TowerData);
        }
        else
        {
            Debug.Log("�Ϲ� ��ư Ŭ�� ��");
        }
    }
}
