using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBase : MonoBehaviour
{
    [SerializeField]
    protected Button button;

    public bool IsInteractable => button.IsInteractable();
    protected virtual void Awake()
    {
        button.onClick.AddListener(OnButtonClicked);
    }

    protected virtual void OnButtonClicked()
    {
        EventManager.Instance.SendButtonEventInfo(this);        
    }

    public void SetInteractable(bool active)
    {
        button.interactable = active;
    }
}
