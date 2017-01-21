using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject _Board;
	public float targetRotationY;
	public float speed;

    //Game over
    private bool _isGameOver;

    //Gestion des monstres
    [SerializeField]
    private GameObject _lightMonster = null;
    [SerializeField]
    private GameObject _soundMonster = null;

    //Gestion des phases de jeu (calme ou spawning)
    private bool _isACalmPhase;
    [SerializeField]
    private float _calmPhaseDuration = 5.0f;

    //Calibrage du spawn de monstres
    [SerializeField]
    private float _mobSpawnIntervalTime = 0.85f;
    [SerializeField]
    private float _mobsToSpawnNumber = 20;

    // Use this for initialization
    void Start () {
        _isACalmPhase = true;
        _isGameOver = false;
        StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    void Update () {
		    if (Mathf.Abs(targetRotationY - _Board.transform.rotation.eulerAngles.y)>0.01 ) {
			    _Board.transform.rotation = Quaternion.Lerp (_Board.transform.rotation, Quaternion.Euler (0.0f, targetRotationY, 0.0f), Time.deltaTime*speed);
		    }
	}

	public void RotateBoardClockwise(){
		targetRotationY = targetRotationY + 90;
	}

	public void RotateBoardAnticlockwise(){
		targetRotationY = targetRotationY - 90;
	}

    IEnumerator SpawnWaves()
    {
        if (!_isGameOver) {
            if (_isACalmPhase)
            {
                _isACalmPhase = false;
                yield return new WaitForSeconds(_calmPhaseDuration);
            }

            int mobsSpawnedNumber = 0;
            Transform mobSpawn = null;

            // a remplacer par un random
            GameObject monsterToSpawn = _lightMonster;
            MobMover spawnedMonsterMover = null;

            while (!_isACalmPhase)
            {
                mobSpawn = RandomMobSpawn();
                if (mobsSpawnedNumber < _mobsToSpawnNumber && !_isGameOver)
                {
                    GameObject spawnedMonster = (GameObject)Instantiate(monsterToSpawn, mobSpawn.transform.position, mobSpawn.transform.rotation);
                    spawnedMonsterMover = spawnedMonster.GetComponent<MobMover>();
                    spawnedMonsterMover.InitializeMobSpawn(mobSpawn);
                    mobsSpawnedNumber++;
                    yield return new WaitForSeconds(_mobSpawnIntervalTime);
                }
                else
                {
                    _isACalmPhase = true;
                    yield return new WaitForSeconds(0.0f);
                }
            }
        }
    }

    private Transform RandomMobSpawn() {
        return GameObject.Find("MobSpawn" + (Random.Range(0, 8)+1)).transform;
    }

    public void GameOver() {
        _isGameOver = true;
        DestroyAllExistingEnnemies();
    }

    public void DestroyAllExistingEnnemies() {
        foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster")) {
            Destroy(monster.gameObject);
        }
    }
}
