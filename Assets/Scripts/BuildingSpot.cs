using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpot : MonoBehaviour {

	public int cadran;
	public int ligne;
	public int emplacement;

	public GameObject[] BuildingsPrefab;

	public GameObject currentBuilding = null;

	private GameController gc;

	void Start(){
		gc = FindObjectOfType<GameController> ();
	}

	void Update(){
		if (gc._mustRebuild) {
			Rebuild ();
		}
	}

	public void Build(BuildingType b){
		currentBuilding = Instantiate(BuildingsPrefab[b-(BuildingType)1],this.transform.localPosition,this.transform.rotation);
		GetComponent<Collider>().enabled = false;
	}

	public void Unbuild(){
		if (currentBuilding != null) {
			Destroy (currentBuilding);
			currentBuilding = null;
		}
		GetComponent<Collider>().enabled = true;
	}

	public void Rebuild(){
		BuildingType b = gc._gameState.getBuilding (cadran, ligne, emplacement);
		if (b == BuildingType.NONE) {
			Unbuild ();
		} else {
			Build (b);
		}
	}


}
