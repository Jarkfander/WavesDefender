using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildFlagController : MonoBehaviour {

	public GameObject[] Buildings;
	public GameObject flagMesh;
	public ParticleSystem particules;

	public void build(int buildingNumber){
		Buildings [buildingNumber].SetActive(true);
		flagMesh.SetActive (false);
		this.GetComponent<Collider> ().enabled = false;
		particules.Play ();
		Destroy (particules.gameObject, 2.0f);
	}
}
