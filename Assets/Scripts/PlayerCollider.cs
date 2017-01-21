using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {

    private GameController _gameController;

    void Start()
    {
        _gameController = GameObject.Find("Plateau").GetComponent<GameController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("Perdu");
            Destroy(other.gameObject);
        }
    }
}
