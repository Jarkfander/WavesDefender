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
		Debug.Log ("rebuilding");
		BuildingType b = gc._gameState.getBuilding (cadran, ligne, emplacement);
		if (b == BuildingType.NONE) {
			Debug.Log ("unbuilding " + cadran + " " + ligne + " " + emplacement);
			Unbuild ();
		} else {
			Debug.Log ("building "+ b +" in " + cadran + " " + ligne + " " + emplacement);
			Build (b);
		}
	}


}
