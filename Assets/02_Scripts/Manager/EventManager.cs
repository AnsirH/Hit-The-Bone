using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
    /// <summary>
    /// 타워 버튼 클릭 이벤트
    /// </summary>
    public UnityEvent<TowerData> onTowerButtonClick = new UnityEvent<TowerData>();

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

    public void SendResetTowerButton()
    {

    }
}
