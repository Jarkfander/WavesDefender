using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildFlagController : MonoBehaviour {

	public GameObject[] Buildings;

	public void build(int buildingNumber){
		Buildings [buildingNumber].SetActive(true);
		this.GetComponent<MeshRenderer> ().enabled = false;
	}
}
