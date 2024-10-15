using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
    public UnityEvent<TowerSpawnButton> onTowerButtonClick;

    public void AddListener(GameObject listener, UnityAction<TowerSpawnButton> action)
    {
        onTowerButtonClick.AddListener(action);
        Debug.Log($"{listener.name}�� onTowerButtonClick �̺�Ʈ�� {nameof(action)}�� ����Ͽ����ϴ�.");
    }

    public void SendButtonEventInfo(ButtonBase button)
    {
        Debug.Log($"{button.name} ��ư Ŭ�� ��");
        if (button is TowerSpawnButton)
        {
            onTowerButtonClick.Invoke(button as TowerSpawnButton);
        }
    }
}
