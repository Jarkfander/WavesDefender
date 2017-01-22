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
                if (_currentPathNodeNumber == 4){
                    _pNR._pathNodeNumber = 3;
                    _pNR._pathNode = _mobSpawn.transform.GetChild(3);
                }
                if (_currentPathNodeNumber == 6){
                    _pNR._pathNodeNumber = 5;
                    _pNR._pathNode = _mobSpawn.transform.GetChild(5);
                }
                if (_currentPathNodeNumber == 8){
                    _pNR._pathNodeNumber = 7;
                    _pNR._pathNode = _mobSpawn.transform.GetChild(7);
                }

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