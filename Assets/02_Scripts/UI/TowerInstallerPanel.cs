using System.Collections;
using System.Collections.Generic;
using UnityEditor.Events;
using UnityEngine;

public class TowerInstallerPanel : MonoBehaviour
{
    [SerializeField]
    TowerSpawnButton[] buttons;

    [SerializeField]
    TowerData[] towerDatas;

    [SerializeField]
    Sprite cancelImage;
    TowerSpawnButton currentButton;

    [SerializeField]
    LayerMask tileLayer;
    [SerializeField]
    TowerData currentTowerData;
    [SerializeField]
    bool isInstallationMode = false;

    public bool IsInstallationMode => isInstallationMode;

    private void Awake()
    {
        EventManager.Instance.onTowerButtonClick.AddListener(OnTowerButtonClicked);
        DisplayTowerButtons();
    }
    private void Update()
    {
        if (isInstallationMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, tileLayer))
                {
                    if (hit.collider.CompareTag("TowerTile"))
                    {
                        InstallTower(hit.transform);
                        currentTowerData = null;
                        isInstallationMode = false;
                        DisplayTowerButtons();
                    }
                }
            }            
        }
    }

    private void InstallTower(Transform tile)
    {
        if (currentTowerData == null)
        {
            Debug.LogError("���� ���õ� Ÿ�� �����Ͱ� �����ϴ�.");
            return;
        }
        ObjectPooler.Instance.SpawnFromPool(currentTowerData.Name, tile.Find("Interaction Target Point").position, Quaternion.identity);
    }

    // Ÿ�� ��ư Ŭ�� �̺�Ʈ �Լ�
    private void OnTowerButtonClicked(TowerData towerData)
    {
        if (towerData == null)
        {
            isInstallationMode = false;
            currentTowerData = null;
            DisplayTowerButtons();
        }
        else
        {
            isInstallationMode = true;
            currentTowerData = towerData;
            DisplayCancleButton();
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