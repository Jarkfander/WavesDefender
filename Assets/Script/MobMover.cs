using UnityEngine;
using System.Collections;

public class MobMover : MonoBehaviour
{
    private GameObject _mobSpawn = null;
    private int _currentPathNodeNumber;
    private int _totalPathNodeNumberInMobSpawn;
    private float _pathNodeReachDelta = 0.1f;

    Rigidbody _rb = null;
    [SerializeField]
    private float _speed;

    private PathNodeReached _pNR = null;


    void Start()
    {
        _totalPathNodeNumberInMobSpawn = _mobSpawn.transform.childCount;
        _rb = GetComponent<Rigidbody>();
        _pNR = _mobSpawn.GetComponent<PathNodeReached>();
        _currentPathNodeNumber = _pNR._pathNodeNumber;
        
        if (_currentPathNodeNumber == 0) {
            _currentPathNodeNumber = 1;
        }
    }

    void Update()
    {
        if (_currentPathNodeNumber < _totalPathNodeNumberInMobSpawn)
        {
            Transform pathNodeToFollow = _mobSpawn.transform.GetChild(_currentPathNodeNumber);
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
        //gestion de l'avancement des spawns
        
        
    }

    public int GetCurrentPathNodeNumber()
    {
        return _currentPathNodeNumber;
    }

    public GameObject GetMobSpawn()
    {
        return _mobSpawn;
    }

    public void InitializeMobSpawn(GameObject mobSpawn)
    {
        _mobSpawn = mobSpawn;
    }
}