using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Core.Events;

public class MainUIComponent : MonoBehaviour, IEventListener
{

    private Dictionary<string, GameObject> _popups;
    public void AddEventListener()
    {
        EventManager.Instance.Add("OpenPopup", this);
        EventManager.Instance.Add("ClosePopup", this);
    }

    public void Invoke<T>(string eventName, T e)
    {
        switch (eventName)
        {
            case "OpenPopup":
                OpenPopup(e as string);
                break;
            case "ClosePopup":
                ClosePopup(e as string);
                break;
        }
    }

    public void RemoveEventListener()
    {
        EventManager.Instance.Remove("OpenPopup", this);
        EventManager.Instance.Remove("ClosePopup", this);
    }

    private void Awake()
    {
        _popups = new Dictionary<string, GameObject>();
    }

    public void AddPopup(string name, GameObject popup)
    {
        _popups.Add(name, popup);
    }

    public void RemovePopup(string name, GameObject popup)
    {
        _popups.Remove(name);
    }

    private void OpenPopup(string name)
    {
        if (_popups.TryGetValue(name, out GameObject prefab))
        {
            GameObject popup = Instantiate(prefab);
            popup.name = name;
            popup.transform.parent = transform;
            popup.transform.localPosition = new Vector3(0,0,0);
            EventManager.Instance.Dispatch("PopupOpened", name);
        }
    }

    private void ClosePopup(string name)
    {
        Destroy(transform.Find(name)?.gameObject);
        EventManager.Instance.Dispatch("PopupClosed", name);
    }
}
