using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Script.Core.Events;

public class GamePlayUIComponent : MonoBehaviour, IEventListener
{
    private Text _coinsTxt;
    private int _coins;

    public void AddEventListener()
    {
        EventManager.Instance.Add("UpdateCoins", this);
    }

    public void Invoke<T>(string name, T e)
    {
        switch (name)
        {
            case "UpdateCoins":

                Debug.Log("UpdateCoins EVENT");
                UpdateCoins((e as IEvent<int>).body);
                break;
        }
    }

    public void RemoveEventListener()
    {
        EventManager.Instance.Remove("UpdateCoins", this);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!_coinsTxt) _coinsTxt = transform.Find("CoinsCountTxt").GetComponent<Text>();
        _coins = 0;
        AddEventListener();
    }

    private void OnDestroy()
    {
        RemoveEventListener();
    }

    private void UpdateCoins(int value)
    {
        _coins += value;
        _coinsTxt.text = _coins.ToString();
    }
}
