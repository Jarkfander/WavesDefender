using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {
    private GameController _gameController;

    [SerializeField]
    private int _towerDamages = 0;
    [SerializeField]
    private BuildingType _towerElement = BuildingType.NONE;

    [SerializeField]
    private float _towerFireInterval;
    private float _damageTime;

    void Start()
    {
        _gameController = GameObject.Find("Plateau").GetComponent<GameController>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.CompareTag("Monster")){
            MobController mob = other.GetComponent<MobController>();
            if (Time.time > (_damageTime + _towerFireInterval)) {
                _damageTime = Time.time;
                mob.TakeDamage(_towerDamages, _towerElement);
            }
        }
    }
}
