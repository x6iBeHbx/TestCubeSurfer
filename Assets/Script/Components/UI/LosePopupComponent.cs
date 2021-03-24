using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Script.Core.Events;

public class LosePopupComponent : MonoBehaviour
{
    private Button _resetBtn;
    void Start()
    {
        _resetBtn = transform.GetComponentInChildren<Button>();
        _resetBtn.onClick.AddListener(ResetRound);
    }

    private void ResetRound()
    {
        EventManager.Instance.Dispatch<string>("ResetRound", null);
        EventManager.Instance.Dispatch<string>("ClosePopup", this.gameObject.name);

    }
}
