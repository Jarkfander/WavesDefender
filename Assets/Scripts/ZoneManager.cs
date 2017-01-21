using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{

    [SerializeField]
    private int CadranNumber;
    [SerializeField]
    private int ZoneNumber;

    private GameController _gameController;

    void Start() {
        _gameController = GameObject.Find("Plateau").GetComponent<GameController>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Monster")) {
            if (ZoneNumber != (_gameController.getGameState().getNbZone()-1) && ZoneNumber >= 0) {
                Debug.Log("Blocking zone " + (ZoneNumber+1) + " in cadran " + CadranNumber);
                _gameController.getGameState().captureZone(CadranNumber, ZoneNumber+1);
            }
        }
    }

    public int GetCadranNumber()
    {
        return CadranNumber;
    }

    public int GetZoneNumber()
    {
        return ZoneNumber;
    }
}
