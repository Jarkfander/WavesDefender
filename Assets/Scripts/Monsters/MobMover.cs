using UnityEngine;
using System.Collections;

public class MobMover : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    Rigidbody _rb= null;

    private GameObject _mainTower;

    void Start()
    {
        _mainTower = GameObject.Find("VoidTower");
        _rb = GetComponent<Rigidbody>();

        if (_mainTower != null)
        {
            transform.rotation = Quaternion.LookRotation(_mainTower.transform.position - gameObject.transform.position);
        }
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(_mainTower.transform.position - gameObject.transform.localPosition);
        transform.position = transform.position + transform.forward*_speed;
    }
}