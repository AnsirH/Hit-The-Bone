using System.Collections;
using System.Collections.Generic;
using UnityEditor.Events;
using UnityEngine;

public class TowerSpawnPanel : MonoBehaviour
{
    [SerializeField]
    TowerSpawnButton[] buttons;

    [SerializeField]
    TowerData[] towerDatas;

    [SerializeField]
    Sprite cancelImage;
    TowerSpawnButton currentButton;
    private bool towerSpawnButtonClicked;

    private void Awake()
    {
        EventManager.Instance.onTowerButtonClick.AddListener(OnTowerButtonClicked);
        DisplayTowerButtons();
    }

    private void OnTowerButtonClicked(TowerData towerData)
    {
        Debug.Log("타워 패널 버튼 이벤트 실행됨");


        // 현재 타워 생성 상태가 아닐 때 <- towerSpawnButtonClicked는 임시 변수임. 상태 확인은 따로 구현할 것.
        if (towerSpawnButtonClicked == false)
        {
            towerSpawnButtonClicked = true;
            DisplayCancleButton();
        }
        else
        {
            towerSpawnButtonClicked = false;
            DisplayTowerButtons();
        }
    }

    private void DisplayTowerButtons()
    {
        if (towerDatas.Length > buttons.Length)
        {
            Debug.LogError("타워 데이터의 정보가 버튼 개수보다 많아 정보를 할당할 수 없습니다.");
            return;
        }
        for (int i = 0; i < towerDatas.Length; i++)
        {
            buttons[i].SetButtonData(towerDatas[i]);
        }
    }

    private void DisplayCancleButton()
    {
        foreach (var button in buttons)
        {
            button.SetButtonData(null);
            button.SetInteractable(false);
        }
        buttons[0].SetButtonImage(cancelImage);
        buttons[0].SetInteractable(true);
    }
} 