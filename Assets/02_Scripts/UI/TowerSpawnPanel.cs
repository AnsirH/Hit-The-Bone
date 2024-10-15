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
        Debug.Log("Ÿ�� �г� ��ư �̺�Ʈ �����");


        // ���� Ÿ�� ���� ���°� �ƴ� �� <- towerSpawnButtonClicked�� �ӽ� ������. ���� Ȯ���� ���� ������ ��.
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
            Debug.LogError("Ÿ�� �������� ������ ��ư �������� ���� ������ �Ҵ��� �� �����ϴ�.");
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