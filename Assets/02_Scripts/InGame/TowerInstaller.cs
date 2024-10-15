using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInstaller : MonoBehaviour
{
    public TowerData currentTowerData;

    private void Awake()
    {
        EventManager.Instance.onTowerButtonClick.AddListener(OnTowerSpawnButtonClicked);
    }

    private void OnTowerSpawnButtonClicked(TowerData towerData)
    {
        currentTowerData = towerData;
    }
}
