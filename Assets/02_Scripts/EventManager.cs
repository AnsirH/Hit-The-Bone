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
        Debug.Log($"{listener.name}가 onTowerButtonClick 이벤트에 {nameof(action)}을 등록하였습니다.");
    }

    public void SendButtonEventInfo(ButtonBase button)
    {
        Debug.Log($"{button.name} 버튼 클릭 됨");
        if (button is TowerSpawnButton)
        {
            TowerSpawnButton towerSpawnButton = (TowerSpawnButton)button;
            Debug.Log("타워 버튼 클릭 됨");
            onTowerButtonClick.Invoke(towerSpawnButton.TowerData);
        }
        else
        {
            Debug.Log("일반 버튼 클릭 됨");
        }
    }
}
