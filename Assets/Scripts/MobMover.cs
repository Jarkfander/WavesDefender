using UnityEngine;
using System.Collections;

public class MobMover : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private GameObject _mainTower;

    void Start()
    {
        _mainTower = GameObject.Find("VoidTower");
    }

    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (_mainTower != null){
            gameObject.transform.rotation = Quaternion.LookRotation(_mainTower.transform.position - gameObject.transform.position);
            rb.velocity = transform.forward * _speed;
        }
    }
}