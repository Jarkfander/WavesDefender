using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{

    private GameController _gameController = null;

    void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            Destroy(other.gameObject);
            _gameController.GameOver();
        }
    }

}
