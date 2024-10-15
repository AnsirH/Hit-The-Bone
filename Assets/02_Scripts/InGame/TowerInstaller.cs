using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInstaller : MonoBehaviour
{
    public TowerData currentTowerData;

    private void Awake()
    {
        EventManager.Instance.AddListener(gameObject, OnTowerSpawnButtonClicked);
    }

    private void OnTowerSpawnButtonClicked(TowerSpawnButton clickedButton)
    {
        if (clickedButton.IsValid == false)
        {
            currentTowerData = null;
        }
        currentTowerData = clickedButton.TowerData;
    }
}
