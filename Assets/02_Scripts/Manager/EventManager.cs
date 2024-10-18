using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
    /// <summary>
    /// Ÿ�� ��ư Ŭ�� �̺�Ʈ
    /// </summary>
    public UnityEvent<TowerData> onTowerButtonClick = new UnityEvent<TowerData>();

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

    public void SendResetTowerButton()
    {

    }
}
