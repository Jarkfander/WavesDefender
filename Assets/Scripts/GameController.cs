using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour {

	public GameObject _buildingPanel;
	public EventSystem es;

	private GameState _gameState;

	private int _selectedSpotCadran;
	private int _selectedSpotLigne;
	private int _selectedSpotEmplacement;

	// Use this for initialization
	void Start () {
		_gameState = new GameState (5, 3);
		_selectedSpotCadran = -1;
		_selectedSpotLigne = -1;
		_selectedSpotEmplacement = -1;
		_buildingPanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				BuildingSpot bs = hit.collider.GetComponent<BuildingSpot> ();
				if (bs != null) {
					_selectedSpotCadran = bs.cadran;
					_selectedSpotLigne = bs.ligne;
					_selectedSpotEmplacement = bs.ligne;

					_buildingPanel.SetActive (true);
				} 
			}
		}

		if (Input.GetMouseButtonDown (1)) {
			_buildingPanel.SetActive (false);
		}
	}

	void BuildTheBuildingThatIsSelected(BuildingType b){
		_gameState.setBuilding (_selectedSpotCadran, _selectedSpotLigne, _selectedSpotEmplacement, b);
		//rebuild les buildings lololol
	}

	public void RotateClockwise(){
		_gameState.rotateBuildingsClockwise();
		//rebuild les buildings lololol
	}

	public void RotateAnticlockwise(){
		_gameState.rotateBuildingsAnticlockwise();
		//rebuild les buildings lololol
	}

	public void BuildPrism(){
		BuildTheBuildingThatIsSelected (BuildingType.PRISM);
		Debug.Log ("Bien!");
	}

	public void BuildBrass(){
		BuildTheBuildingThatIsSelected (BuildingType.BRASS);
		Debug.Log ("Oui messire");
	}

}
