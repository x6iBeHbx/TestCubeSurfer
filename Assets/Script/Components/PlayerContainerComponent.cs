using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Core.Events;

public class PlayerContainerComponent : MonoBehaviour
{
    [SerializeField] GameObject _explosion;
    [SerializeField] GameObject _explosionContainer;

    List<Transform> _childsCube;
    List<ParticleSystem> _explosions;
    Transform _character;
    Transform _camera;
    Animator _animator;

    CustomEvent<int> _coinsEventObj;

    private float offsetY;

    private void Awake()
    {
        _explosionContainer = GameObject.Find("Explosions");
    }

    void Start()
    {
        // temp
        offsetY = 1f;
        _coinsEventObj = new CustomEvent<int>(0);
        _childsCube = new List<Transform>();
        _explosions = new List<ParticleSystem>();
        _character = transform.Find("Character");
        _camera = _character.Find("PlayerCamera");
        _animator = _character.GetComponentInChildren<Animator>();
        _animator.Play("IdleState");

        InitChilds();
        CalculatePostionForChilds();
        Debug.Log(_childsCube.Count);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (ParticleSystem p in _explosions)
        {
            if (p.gameObject && !p.IsAlive())
            {
                _explosions.Remove(p);
                Destroy(p.gameObject);
            }
        }
    }

    
    public void AddCube(Transform cube)
    {
        _childsCube.Add(cube);
        cube.tag = "PlayerItem";
        CollisionComponent collComp = cube.gameObject.AddComponent<CollisionComponent>();
        collComp.ContainerComponent = this;

        _camera.transform.Rotate(new Vector3(0.5f, 0, 0));
    }

    public void RemoveCube(Transform cube)
    {
        if (_childsCube.Contains(cube))
        {
            Debug.Log("Remove ITEM: " + cube.name);
            GameObject exp = Instantiate(_explosion);
            exp.transform.parent = _explosionContainer.transform;
            exp.transform.position = cube.position;
            _childsCube.Remove(cube);
            _camera.transform.Rotate(new Vector3(-0.5f, 0, 0));
            CheckForLose();
        }
    }

    public void CalculatePostionForChilds()
    {
        Debug.Log(_childsCube.Count);

        for (int i = _childsCube.Count - 1; i >= 0; i--)
        {
            float y = ((_childsCube.Count - 1) - i) * offsetY;
            _childsCube[i].localPosition = new Vector3(0, y, 0);
        }

        _character.localPosition = new Vector3(0, _childsCube.Count * offsetY, 0);

        
    }

    private void InitChilds()
    {
        foreach (Transform b in transform)
        {
            if (b.tag == "PlayerItem")
            {
                CollisionComponent collComp= b.gameObject.AddComponent<CollisionComponent>();
                Rigidbody rg = b.gameObject.GetComponent<Rigidbody>();

                collComp.ContainerComponent = this;

                _childsCube.Add(b);
            }
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Coins")
        {
            _coinsEventObj.body = 1;
            EventManager.Instance.Dispatch<IEvent<int>>("UpdateCoins" , _coinsEventObj);

            Destroy(coll.gameObject);
        }

        if (coll.name == "finish_zone")
        {
            _animator.SetTrigger("WinRound");
            EventManager.Instance.Dispatch<string>("StopPlay", null);
            EventManager.Instance.Dispatch<string>("OpenPopup", "WinPopup");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Collition Exit" + other.tag);
        if (other.tag == "ObstacleZone")
        {
            CalculatePostionForChilds();
        }
    }

    private void CheckForLose()
    {
        if (_childsCube.Count <= 0)
        {

            EventManager.Instance.Dispatch<string>("StopPlay", null);
            EventManager.Instance.Dispatch("OpenPopup", "LosePopup");
        }
    }
}
