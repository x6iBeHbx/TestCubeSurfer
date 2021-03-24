using UnityEngine;
using Assets.MyTestWithAnim.Script.Utils;
using Assets.Script.Core.Events;

public class MoveComponent : MonoBehaviour, IEventListener
{
    [SerializeField] private float _velocity = 5f;
    private Transform[] _pointList;

    private Vector3 _directionVector;
    private Vector3 _nextPoint;
    private float _delta;
    private bool IsMovable { get; set; }

    public int _rotationPointIndex;

    void Awake()
    {
        // Default vector move on Z
        _directionVector = new Vector3(0, 0, 1);

        _delta = 0;
        _rotationPointIndex = 0;
    }

    void FixedUpdate()
    {
        if (!IsMovable)
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            float axisH = Input.GetAxis("Mouse X") * 0.2f;

            float minX = _pointList[_rotationPointIndex].position.x - 2.5f;
            float maxX = _pointList[_rotationPointIndex].position.x + 2.5f;

            float minZ = _pointList[_rotationPointIndex].position.z - 2.5f;
            float maxZ = _pointList[_rotationPointIndex].position.z + 2.5f;


            _delta += axisH;
            _delta = Mathf.Clamp(_delta, -2.5f, 2.5f);

            if (_directionVector.z > 0)
            {
                float posX = base.transform.position.x + axisH;
                posX = Mathf.Clamp(posX, minX, maxX);
                base.transform.position = new Vector3(posX, base.transform.position.y, base.transform.position.z);
            }


            if (_directionVector.x > 0)
            {
                float posZ = base.transform.position.z - axisH;
                posZ = Mathf.Clamp(posZ, minZ, maxZ);
                base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, posZ);
                
            }
        }

        Vector3 nextRotationPoint =
            new Vector3(_pointList[_rotationPointIndex].position.x + _delta,
            transform.position.y, _pointList[_rotationPointIndex].position.z - _delta);

        transform.position = Vector3.MoveTowards(transform.position, nextRotationPoint, _velocity * Time.deltaTime);

        if (Vector3.Equals(nextRotationPoint, transform.position))
        {
            float angle = _pointList[_rotationPointIndex].eulerAngles.y;
            transform.position = nextRotationPoint;


            _directionVector = MathUtil.Vector3ByYRotation(_directionVector, angle);
            base.transform.Rotate(new Vector3(0, angle, 0));
           

            if (_rotationPointIndex < _pointList.Length - 1)
            {
                _rotationPointIndex++;
            } else
            {
                IsMovable = false;
            }
        }
    }

    public void InitPoints(Transform[] points)
    {
        Debug.Log(points.Length);
        _pointList = points;
    }

    public void AddEventListener()
    {
        EventManager.Instance.Add("StartPlay", this);
        EventManager.Instance.Add("StopPlay", this);
    }

    public void RemoveEventListener()
    {
        EventManager.Instance.Remove("StartPlay", this);
        EventManager.Instance.Remove("StopPlay", this);
    }

    public void Invoke<T>(string eventName, T e)
    {
        switch (eventName)
        {
            case "StartPlay":
                IsMovable = true;
                break;
            case "StopPlay":
                IsMovable = false;
                break;
        }
    }
}
