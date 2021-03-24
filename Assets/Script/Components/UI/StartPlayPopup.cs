using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Core.Events;

public class StartPlayPopup : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            EventManager.Instance.Dispatch<string>("StartPlay", null);
            EventManager.Instance.Dispatch("ClosePopup", gameObject.name);
        }
    }
}
