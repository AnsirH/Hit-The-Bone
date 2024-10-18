using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpawnButton : ButtonBase
{
    [SerializeField]
    private TowerData towerData;
    [SerializeField]
    private Image buttonImage;

    public TowerData TowerData => towerData;

    public bool IsValid => towerData != null;

    protected override void Awake()
    {
        base.Awake();
        if (towerData != null)
        {
            SetButtonImage(towerData.Icon);
        }
    }

    /// <summary>
    /// 버튼의 타워 데이터를 설정한다.
    /// </summary>
    /// <param name="newTowerData"></param>
    public void SetButtonData(TowerData newTowerData)
    {
        if (newTowerData == null)
        {
            towerData = null;
            SetButtonImageEmpty();
            SetInteractable(false);
            return;
        }
        towerData = newTowerData;
        SetButtonImage(towerData.Icon);
        SetInteractable(true);
    }

    #region 버튼 이미지 설정
    public void SetButtonImage(Sprite newImage)
    {
        if (newImage == null)
        {
            SetButtonImageEmpty();
            return;
        }
        buttonImage.sprite = newImage;
        buttonImage.color = Color.white;
    }

    public void SetButtonImageEmpty()
    {
        buttonImage.sprite = null;
        buttonImage.color = Color.black;
    }
    #endregion

}
