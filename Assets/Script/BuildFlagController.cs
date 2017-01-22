using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildFlagController : MonoBehaviour {

	public GameObject[] Buildings;
	public GameObject flagMesh;

	public void build(int buildingNumber){
		Buildings [buildingNumber].SetActive(true);
		flagMesh.SetActive (false);
		this.GetComponent<Collider> ().enabled = false;
	}
}
