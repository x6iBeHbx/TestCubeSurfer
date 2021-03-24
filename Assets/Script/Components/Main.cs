using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Core.Events;

public class Main : MonoBehaviour, IEventListener
{
    private GameObject _ui;
    private GameObject _track;
    private GameObject _player;
    void Start()
    {
        StartCoroutine(Init());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Init()
    {
        yield return null;

        _track = InitRound(0);
        _player = InitPlayer(_track);
        InitPopup();

        AddEventListener();
    }

    private GameObject InitRound(int roundID)
    {
        Debug.Log($"Prefabs/Track/Round_{roundID}");
        GameObject roundPrefab = Resources.Load<GameObject>($"Prefabs/Track/Round_{roundID}");
        GameObject round =  Instantiate(roundPrefab);
        round.name = "Track";
        round.transform.parent = transform;
        round.transform.position = new Vector3(0,0,0);

        return round;
    }

    private GameObject InitPlayer(GameObject track)
    {
        Debug.Log($"Prefabs/Player/Player");
        GameObject playerPrefab = Resources.Load<GameObject>($"Prefabs/Player/Player");
        GameObject player = Instantiate(playerPrefab);

        player.name = "Player";
        player.transform.parent = transform;

        player.transform.position = track.transform.Find("spawn_point").position;

        Transform pointsContainer = track.transform.Find("points");
        Transform[] points = new Transform[pointsContainer.childCount];

        for (int i = 0; i < pointsContainer.childCount; i++)
        {
            Debug.Log(pointsContainer.GetChild(i).name);
            Transform point = pointsContainer.Find("point_"+ i);
            points[i] = point;
        }

        MoveComponent moveComponent = player.GetComponent<MoveComponent>();

        moveComponent.InitPoints(points);
        moveComponent.AddEventListener();

        return player;
    }

    private void InitPopup()
    {
        _ui = GameObject.Find("UI");

        MainUIComponent uiComponent = _ui.GetComponent<MainUIComponent>();

        GameObject[] popups = Resources.LoadAll<GameObject>($"Prefabs/Popup");
        foreach (GameObject popup in popups)
        {
            uiComponent.AddPopup(popup.name, popup);
        }

        uiComponent.AddEventListener();

        EventManager.Instance.Dispatch("OpenPopup", "StartPopup");
    }

    private void ResetRound()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        GameObject roundTrack = InitRound(0);
        InitPlayer(roundTrack);

        EventManager.Instance.Dispatch("OpenPopup", "StartPopup");
    }

    public void AddEventListener()
    {
        EventManager.Instance.Add("LoadNextRound", this);
        EventManager.Instance.Add("ResetRound", this);
        EventManager.Instance.Add("StartPlay", this);
        EventManager.Instance.Add("StopPlay", this);
    }

    public void RemoveEventListener()
    {
        EventManager.Instance.Remove("LoadNextRound", this);
        EventManager.Instance.Remove("ResetRound", this);
        EventManager.Instance.Remove("StartPlay", this);
        EventManager.Instance.Remove("StopPlay", this);
    }

    public void Invoke<T>(string eventName, T e)
    {
        switch (eventName)
        {
            case "LoadNextRound":
                ResetRound();
                break;
            case "ResetRound":
                ResetRound();
                break;
            case "StartPlay":
                EventManager.Instance.Dispatch("OpenPopup", "GamplayUI");
                break;
            case "StopPlay":
                EventManager.Instance.Dispatch("ClosePopup", "GamplayUI");
                break;
        }
    }
}
