using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    NONE = 0,
    BRASS = 1,
    PRISM = 2
}

public class TowerManager : MonoBehaviour
{
    private SortedList<int, MobController> _monstersToKill;
    private int _mobNumber = 0;
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
        _monstersToKill = new SortedList<int, MobController>();
        _gameController = GameObject.Find("PlayerPlatform").GetComponent<GameController>();
    }

    void Update()
    {
        if (_monstersToKill.Count > 0)
        {
            foreach (MobController mob in _monstersToKill.Values)
            {
                mob.TakeDamage(_towerDamages, _towerElement);
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            MobController mob = other.GetComponent<MobController>();
            if (!_monstersToKill.ContainsValue(mob))
            {
                _monstersToKill.Add(_mobNumber, mob);
                _mobNumber++;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            MobController mob = other.GetComponent<MobController>();
            if (_monstersToKill.ContainsValue(mob))
            {
                _monstersToKill.RemoveAt(_monstersToKill.IndexOfValue(mob));
            }
        }
    }



}
