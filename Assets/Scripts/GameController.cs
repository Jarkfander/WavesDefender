using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour {

	private int _NOMBRE_SPAWNS_PAR_ZONE = 5;
	private int _NOMBRE_ZONES_PAR_CADRAN;
	private int _NOMBRE_DE_CADRANS;

	public GameState _gameState;

	private GameObject _cadran;

	private Transform[] _mobSpawns;

	//int = numero de cadran
	//sortedList : int = numero de zone -- GameObject = mobSpawns pour la zone
	private SortedList<int,SortedList<int, Transform>> _mobSpawnsParCadran = null;
	public GameObject _buildingPanel;
	public EventSystem es;

	private BuildingSpot _selectedSpot;

	// Use this for initialization
	void Start () {
		_selectedSpot = null;
		_buildingPanel.SetActive (false);
		_gameState = new GameState(5,4);
		_NOMBRE_DE_CADRANS = _gameState.getNbCadran();
		_NOMBRE_ZONES_PAR_CADRAN = _gameState.getNbZone();
		initializeMobSpawns();
	}
	
	// Update is called once per frame
	void Update () {
        Transform mobSpawnGroupDeLaZone = null;
        SortedList<int, Transform> mobSpawns = new SortedList<int, Transform>();
        int spawnCount = 0;

        //Pour chaque cadran on met à jour la liste des mobspawns actifs
        for (int numeroCadran = 0; numeroCadran < _NOMBRE_DE_CADRANS; ++numeroCadran)
        {
            _cadran = GameObject.Find("Cadran" + numeroCadran);
            if (_cadran != null)
            {
                //pour chaque zone on remplit une liste temporaire de mobSpawns
                for (int numeroZone = _NOMBRE_ZONES_PAR_CADRAN; numeroZone > 0; numeroZone--)
                {
                    if (!_gameState.isZoneActive(numeroCadran, numeroZone-1))
                    {
                        mobSpawnGroupDeLaZone = _cadran.GetComponent<Transform>().GetChild(numeroZone - 1).GetChild(0);
                        if (mobSpawnGroupDeLaZone != null)
                        {
                            for (int numeroSpawnZone = 0; numeroSpawnZone < _NOMBRE_SPAWNS_PAR_ZONE; numeroSpawnZone++)
                            {
                                //On ajoute dans une liste temporaire tous les mobspawns pour une zone en particulier
                                Transform spawn = mobSpawnGroupDeLaZone.GetChild(numeroSpawnZone);
                                mobSpawns.Add(spawnCount, spawn);
                                spawnCount++;
                            }
                        }
                        else
                        {
                            Debug.Log("Erreur : impossible de trouver le gameObject contenant les mobSpawns de départ du cadran " + numeroCadran);
                        }
                    }
                }
                //On supprime la liste de MobSpawns active pour le cadran en cours de traitement
                _mobSpawnsParCadran.Remove(numeroCadran);

                //on remet la liste globale de mobSpawns à jour avec la liste temporaire pour le cadran en cours de traitement
                _mobSpawnsParCadran.Add(numeroCadran, mobSpawns);
            }
            else{
                Debug.Log("Erreur : cadran" + numeroCadran + " introuvable");
            }

        }

        //Check click selection building spot
        if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				BuildingSpot bs = hit.collider.GetComponent<BuildingSpot> ();
				if (bs != null) {
					_selectedSpot = bs;

					_buildingPanel.SetActive (true);
				} 
			}
		}

		if (Input.GetMouseButtonDown (1)) {
			_buildingPanel.SetActive (false);
		}
	}

	void BuildTheBuildingThatIsSelected(BuildingType b){
		_gameState.setBuilding (_selectedSpot.cadran, _selectedSpot.ligne, _selectedSpot.emplacement, b);
		_selectedSpot.Build (b);
	}

	public void RotateClockwise(){
		_gameState.rotateBuildingsClockwise();
		Rebuild ();
	}

	public void RotateAnticlockwise(){
		_gameState.rotateBuildingsAnticlockwise();
		Rebuild ();
	}

	public void BuildPrism(){
		BuildTheBuildingThatIsSelected (BuildingType.PRISM);
		_buildingPanel.SetActive (false);
	}

	public void BuildBrass(){
		BuildTheBuildingThatIsSelected (BuildingType.BRASS);
		_buildingPanel.SetActive (false);
	}

	void Rebuild(){
		BuildingSpot[] buildingSpots = FindObjectsOfType<BuildingSpot> ();

		foreach (BuildingSpot buildingSpot in buildingSpots) {
			
			buildingSpot.Rebuild ();
		}
	}

    /// <summary>
    /// Cette méthode initialise la liste des mobSpawns actifs pour tous les cadrans
    /// </summary>
    private void initializeMobSpawns() {
        Transform mobSpawnGroupDeLaZone = null;
        SortedList<int, Transform> mobSpawns = new SortedList<int, Transform>();
        _mobSpawnsParCadran = new SortedList<int, SortedList<int, Transform>>();
        int spawnCount = 0;

        //Pour chaque cadran on veut set les mobspawns actifs
        for (int numeroCadran = 0; numeroCadran < _NOMBRE_DE_CADRANS; ++numeroCadran)
        {
            _cadran = GameObject.Find("Cadran" + numeroCadran);
            if (_cadran != null) {
                mobSpawnGroupDeLaZone = _cadran.GetComponent<Transform>().GetChild(_NOMBRE_ZONES_PAR_CADRAN - 1).GetChild(0);

                if (mobSpawnGroupDeLaZone != null)
                {
                    for (int numeroSpawnZone = 0; numeroSpawnZone < _NOMBRE_SPAWNS_PAR_ZONE; numeroSpawnZone++)
                    {
                        //On ajoute dans une liste temporaire tous les mobspawns pour une zone en particulier
                        Transform spawn = mobSpawnGroupDeLaZone.GetChild(numeroSpawnZone);
                        mobSpawns.Add(spawnCount, spawn);
                        spawnCount++;
                    }
                    //on set la liste globale de mobSpawns avec la liste temporaire pour le cadran en cours de traitement
                    _mobSpawnsParCadran.Add(numeroCadran, mobSpawns);
                }else{
                    Debug.Log("Erreur : impossible de trouver le gameObject contenant les mobSpawns de départ du cadran "+numeroCadran);
                }
                
            }else{
                Debug.Log("Erreur : cadran" + numeroCadran+" introuvable");
            }
        }
    }



    public GameState getGameState() {
        return _gameState;
    }
}
