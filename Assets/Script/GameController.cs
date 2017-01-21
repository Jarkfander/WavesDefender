using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject _Board;
    public float targetRotationY;
    public float speed;
    public GameObject panelBuilding;
    private GameObject Flag;



    //Economie
    [SerializeField]
    private int _startingGold = 0;
    private int _currentGold =0;
    [SerializeField]
    private UnityEngine.UI.Text _goldText = null;

    //Tours
    [SerializeField]
    private GameObject _towerPrism = null;
    [SerializeField]
    private GameObject _towerBrass = null;

    //Game over
    private bool _isGameOver;

	[SerializeField]
	private GameObject gameoverScreen;

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

    //Audio
    [SerializeField]
    private AudioClip _gameOverAudio = null;

	public Button leftButton;
	public Button rightButton;

    // Use this for initialization
    void Start()
    {
        _isACalmPhase = true;
        _isGameOver = false;
        StartCoroutine(SpawnWaves());
        _currentGold = _startingGold;
        UpdateGoldText();
    }

    // Update is called once per frame
    void Update()
    {
		if (Mathf.Abs (targetRotationY - _Board.transform.rotation.eulerAngles.y) > 0.02) {
			_Board.transform.rotation = Quaternion.Lerp (_Board.transform.rotation, Quaternion.Euler (0.0f, targetRotationY, 0.0f), Time.deltaTime * speed);
		} else {
			leftButton.interactable = true;
			rightButton.interactable = true;
		}

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.transform.tag + " " + hit.transform.name);
                if (hit.transform.CompareTag("Flag"))
                {
                    panelBuilding.SetActive(true);
                    Flag = hit.transform.gameObject;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            panelBuilding.SetActive(false);
        }
    }

    public void BuildBrassOnFlag()
    {
        int price = _towerBrass.GetComponent<TowerManager>().GetTowerPrice();
        if (GetCurrentGold() >= price)
        {
            Flag.GetComponent<BuildFlagController>().build(1);
            panelBuilding.SetActive(false);
            RemoveGold(price);
        }
        else {
            Debug.Log("Not enough gold to build a Brass Tower");
        }
        
    }

    public void BuildPrismOnFlag()
    {
        int price = _towerPrism.GetComponent<TowerManager>().GetTowerPrice();
        if (GetCurrentGold() >= price)
        {
            Flag.GetComponent<BuildFlagController>().build(0);
            panelBuilding.SetActive(false);
            RemoveGold(price);
        }
        else
        {
            Debug.Log("Not enough gold to build a Prism Tower");
        }
    }

    IEnumerator SpawnWaves()
    {
        if (!_isGameOver)
        {
            if (_isACalmPhase)
            {
                _isACalmPhase = false;
                yield return new WaitForSeconds(_calmPhaseDuration);
            }

            int mobsSpawnedNumber = 0;
            Transform mobSpawn = null;

            // a remplacer par un random
            GameObject monsterToSpawn = null;

            MobMover spawnedMonsterMover = null;

            while (!_isACalmPhase)
            {
                Element mobElement = (Element)Random.Range(0, 2);
                if (mobElement.Equals(Element.Light))
                {
                    monsterToSpawn = _lightMonster;
                }
                else if (mobElement.Equals(Element.Sound))
                {
                    monsterToSpawn = _soundMonster;
                }
                mobSpawn = RandomMobSpawn();
                if (mobsSpawnedNumber < _mobsToSpawnNumber && !_isGameOver)
                {
                    GameObject spawnedMonster = null;
                    if (monsterToSpawn != null)
                    {
                        spawnedMonster = (GameObject)Instantiate(monsterToSpawn, mobSpawn.transform.position, mobSpawn.transform.rotation);
                        spawnedMonsterMover = spawnedMonster.GetComponent<MobMover>();
                        spawnedMonsterMover.InitializeMobSpawn(mobSpawn);

                    }
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

    private Transform RandomMobSpawn()
    {
        return GameObject.Find("MobSpawn" + (Random.Range(0, 8) + 1)).transform;
    }

    public void GameOver()
    {
        _isGameOver = true;
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = _gameOverAudio;
        audioSource.Play();
        
        DestroyAllExistingEnnemies();

		gameoverScreen.SetActive (true);
    }

    public void DestroyAllExistingEnnemies()
    {
        foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))
        {
            Destroy(monster.gameObject);
        }
    }

    public void RotateBoardClockwise()
    {
        targetRotationY = targetRotationY + 90;
		leftButton.interactable = false;
		rightButton.interactable = false;
    }

    public void RotateBoardAnticlockwise()
    {
        targetRotationY = targetRotationY - 90;
		leftButton.interactable = false;
		rightButton.interactable = false;
    }

    public int GetCurrentGold() {
        return _currentGold;
    }

    public void AddGold(int gold) {
        _currentGold += gold;
        UpdateGoldText();
    }

    public void RemoveGold(int price) {
        _currentGold -= price;
        if (_currentGold < 0) {
            _currentGold = 0;
        }
        UpdateGoldText();
    }

    public void UpdateGoldText() {
        _goldText.text = GetCurrentGold() + " minerals";
    }
}
