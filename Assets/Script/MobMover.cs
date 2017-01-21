using UnityEngine;
using System.Collections;

public class MobMover : MonoBehaviour
{
    private Transform _mobSpawn = null;
    private int _currentPathNodeNumber;
    private int _totalPathNodeNumberInMobSpawn;
    private float _pathNodeReachDelta = 0.1f;

    Rigidbody _rb= null;
    [SerializeField]
    private float _speed;



    void Start()
    {
        _totalPathNodeNumberInMobSpawn = _mobSpawn.childCount;
        _rb = GetComponent<Rigidbody>();
        _currentPathNodeNumber = 0;
    }

    void Update(){
        if (_currentPathNodeNumber < _totalPathNodeNumberInMobSpawn) {
            Transform pathNodeToFollow = _mobSpawn.GetChild(_currentPathNodeNumber);
            if (pathNodeToFollow != null)
            {
                transform.rotation = Quaternion.LookRotation(pathNodeToFollow.transform.position - gameObject.transform.position);
                _rb.velocity = transform.forward * _speed;
                if (Mathf.Abs(pathNodeToFollow.position.x - gameObject.transform.position.x) < _pathNodeReachDelta
                        && Mathf.Abs(pathNodeToFollow.position.y - gameObject.transform.position.y) < _pathNodeReachDelta
                        && Mathf.Abs(pathNodeToFollow.position.z - gameObject.transform.position.z) < _pathNodeReachDelta)
                {
                    _currentPathNodeNumber++;
                }
            }
        }
    }

    public void InitializeMobSpawn(Transform mobSpawn) {
        _mobSpawn = mobSpawn;
    }
}